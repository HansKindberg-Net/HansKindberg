using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;

namespace HansKindberg.DirectoryServices
{
	public class Directory : GeneralDirectory, IDirectory, IGlobalDirectory
	{
		#region Fields

		private readonly DirectoryConnection _connection;
		private readonly IDirectoryUriParser _directoryUriParser;
		private readonly IDistinguishedNameParser _distinguishedNameParser;
		private readonly ISearchOptions _searchOptions;

		#endregion

		#region Constructors

		public Directory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser) {}
		public Directory(IDirectoryConnection connection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser) : this(connection, directoryUriParser, distinguishedNameParser, new SearchOptions()) {}
		public Directory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser, searchOptions) {}

		public Directory(IDirectoryConnection connection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions)
		{
			if(connection == null)
				throw new ArgumentNullException("connection");

			if(directoryUriParser == null)
				throw new ArgumentNullException("directoryUriParser");

			if(distinguishedNameParser == null)
				throw new ArgumentNullException("distinguishedNameParser");

			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			this._connection = new DirectoryConnection();
			this._connection.Authentication.AuthenticationTypes = connection.Authentication.AuthenticationTypes;
			this._connection.Authentication.Password = connection.Authentication.Password;
			this._connection.Authentication.UserName = connection.Authentication.UserName;

			if(connection.Url.DistinguishedName != null)
				this._connection.Url.DistinguishedName = distinguishedNameParser.Parse(connection.Url.DistinguishedName.ToString());

			this._connection.Url.Host = connection.Url.Host;
			this._connection.Url.Port = connection.Url.Port;
			this._connection.Url.Scheme = connection.Url.Scheme;
			this._directoryUriParser = directoryUriParser;
			this._distinguishedNameParser = distinguishedNameParser;
			this._searchOptions = searchOptions;
		}

		#endregion

		#region Properties

		protected internal virtual IDirectoryAuthentication Authentication
		{
			get { return this.Connection.Authentication; }
		}

		public virtual AuthenticationTypes? AuthenticationTypes
		{
			get { return this.Connection.Authentication.AuthenticationTypes; }
			set { this.Connection.Authentication.AuthenticationTypes = value; }
		}

		protected internal virtual DirectoryConnection Connection
		{
			get { return this._connection; }
		}

		protected internal virtual IDirectoryUriParser DirectoryUriParser
		{
			get { return this._directoryUriParser; }
		}

		public virtual IDistinguishedName DistinguishedName
		{
			get { return this.Connection.Url.DistinguishedName; }
			set { this.Connection.Url.DistinguishedName = value; }
		}

		protected internal virtual IDistinguishedNameParser DistinguishedNameParser
		{
			get { return this._distinguishedNameParser; }
		}

		public virtual string Host
		{
			get { return this.Connection.Url.Host; }
			set { this.Connection.Url.Host = value; }
		}

		public virtual string Password
		{
			get { return this.Connection.Authentication.Password; }
			set { this.Connection.Authentication.Password = value; }
		}

		public virtual int? Port
		{
			get { return this.Connection.Url.Port; }
			set { this.Connection.Url.Port = value; }
		}

		public virtual IDirectoryItem Root
		{
			get { return this.Get(this.Url, this.Authentication); }
		}

		public virtual Scheme Scheme
		{
			get { return this.Connection.Url.Scheme; }
			set { this.Connection.Url.Scheme = value; }
		}

		public virtual ISearchOptions SearchOptions
		{
			get { return this._searchOptions; }
		}

		protected internal virtual IDirectoryUri Url
		{
			get { return this.Connection.Url; }
		}

		public virtual string UserName
		{
			get { return this.Connection.Authentication.UserName; }
			set { this.Connection.Authentication.UserName = value; }
		}

		#endregion

		#region Methods

		protected internal virtual IDirectoryItem CreateDirectoryItem(DirectoryEntry directoryEntry)
		{
			if(directoryEntry == null)
				return null;

			try
			{
				using(var directorySearcher = new DirectorySearcher(directoryEntry))
				{
					directorySearcher.SearchScope = SearchScope.Base;
					return this.CreateDirectoryItem(directorySearcher.FindOne());
				}
			}
			catch(NotSupportedException)
			{
				var directoryItem = new DirectoryItem
				{
					Url = this.DirectoryUriParser.Parse(directoryEntry.Path)
				};

				foreach(string propertyName in directoryEntry.Properties.PropertyNames)
				{
					directoryItem.Properties.Add(propertyName, directoryEntry.Properties[propertyName].Value);
				}

				return directoryItem;
			}
		}

		protected internal virtual IDirectoryItem CreateDirectoryItem(SearchResult searchResult)
		{
			if(searchResult == null)
				return null;

			var directoryItem = new DirectoryItem
			{
				Url = this.CreateDirectoryUri(searchResult.Path)
			};

			if(searchResult.Properties != null && searchResult.Properties.PropertyNames != null)
			{
				foreach(string propertyName in searchResult.Properties.PropertyNames)
				{
					var resultPropertyValueCollection = searchResult.Properties[propertyName];

					var propertyValue = resultPropertyValueCollection.Count == 0 ? null : (resultPropertyValueCollection.Count == 1 ? resultPropertyValueCollection[0] : resultPropertyValueCollection.Cast<object>().ToArray());

					directoryItem.Properties.Add(propertyName, propertyValue);
				}
			}

			return directoryItem;
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller.")]
		protected internal virtual DirectorySearcher CreateDirectorySearcher(DirectoryEntry searchRoot, ISearchOptions searchOptions)
		{
			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			var directorySearcher = new DirectorySearcher(searchRoot, searchOptions.Filter, searchOptions.PropertiesToLoad == null ? null : searchOptions.PropertiesToLoad.ToArray());

			if(searchOptions.Asynchronous != null)
				directorySearcher.Asynchronous = searchOptions.Asynchronous.Value;

			if(searchOptions.AttributeScopeQuery != null)
				directorySearcher.AttributeScopeQuery = searchOptions.AttributeScopeQuery;

			if(searchOptions.CacheResults != null)
				directorySearcher.CacheResults = searchOptions.CacheResults.Value;

			if(searchOptions.ClientTimeout != null)
				directorySearcher.ClientTimeout = searchOptions.ClientTimeout.Value;

			if(searchOptions.DereferenceAlias != null)
				directorySearcher.DerefAlias = searchOptions.DereferenceAlias.Value;

			if(searchOptions.DirectorySynchronization != null)
				directorySearcher.DirectorySynchronization = searchOptions.DirectorySynchronization;

			if(searchOptions.ExtendedDistinguishedName != null)
				directorySearcher.ExtendedDN = searchOptions.ExtendedDistinguishedName.Value;

			if(searchOptions.PageSize != null)
				directorySearcher.PageSize = searchOptions.PageSize.Value;

			if(searchOptions.PropertyNamesOnly != null)
				directorySearcher.PropertyNamesOnly = searchOptions.PropertyNamesOnly.Value;

			if(searchOptions.ReferralChasing != null)
				directorySearcher.ReferralChasing = searchOptions.ReferralChasing.Value;

			if(searchOptions.SearchScope != null)
				directorySearcher.SearchScope = searchOptions.SearchScope.Value;

			if(searchOptions.SecurityMasks != null)
				directorySearcher.SecurityMasks = searchOptions.SecurityMasks.Value;

			if(searchOptions.ServerPageTimeLimit != null)
				directorySearcher.ServerPageTimeLimit = searchOptions.ServerPageTimeLimit.Value;

			if(searchOptions.ServerTimeLimit != null)
				directorySearcher.ServerTimeLimit = searchOptions.ServerTimeLimit.Value;

			if(searchOptions.SizeLimit != null)
				directorySearcher.SizeLimit = searchOptions.SizeLimit.Value;

			if(searchOptions.Sort != null)
				directorySearcher.Sort = searchOptions.Sort;

			if(searchOptions.Tombstone != null)
				directorySearcher.Tombstone = searchOptions.Tombstone.Value;

			if(searchOptions.VirtualListView != null)
				directorySearcher.VirtualListView = searchOptions.VirtualListView;

			return directorySearcher;
		}

		protected internal virtual IDirectoryUri CreateDirectoryUri(IDistinguishedName distinguishedName)
		{
			return new DirectoryUri
			{
				DistinguishedName = distinguishedName,
				Host = this.Host,
				Port = this.Port,
				Scheme = this.Scheme
			};
		}

		protected internal virtual IDirectoryUri CreateDirectoryUri(string path)
		{
			return this.DirectoryUriParser.Parse(path);
		}

		public virtual IEnumerable<IDirectoryItem> Find()
		{
			return this.Find(this.Url, this.SearchOptions, this.Authentication);
		}

		IEnumerable<IDirectoryItem> IDirectory.Find(string searchRootDistinguishedName)
		{
			return ((IDirectory) this).Find(searchRootDistinguishedName, this.SearchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName)
		{
			return this.Find(searchRootDistinguishedName, this.SearchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(ISearchOptions searchOptions)
		{
			return this.Find(this.Url, searchOptions, this.Authentication);
		}

		IEnumerable<IDirectoryItem> IGlobalDirectory.Find(string searchRootPath)
		{
			return this.Find(searchRootPath, null, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl)
		{
			return this.Find(searchRootUrl, null, null);
		}

		IEnumerable<IDirectoryItem> IDirectory.Find(string searchRootDistinguishedName, ISearchOptions searchOptions)
		{
			return this.Find(this.DistinguishedNameParser.Parse(searchRootDistinguishedName), searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, ISearchOptions searchOptions)
		{
			return this.Find(this.CreateDirectoryUri(searchRootDistinguishedName), searchOptions, this.Authentication);
		}

		IEnumerable<IDirectoryItem> IGlobalDirectory.Find(string searchRootPath, ISearchOptions searchOptions)
		{
			return this.Find(searchRootPath, searchOptions, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, ISearchOptions searchOptions)
		{
			return this.Find(searchRootUrl, searchOptions, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootPath, IDirectoryAuthentication authentication)
		{
			return this.Find(searchRootPath, null, authentication);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, IDirectoryAuthentication authentication)
		{
			return this.Find(searchRootUrl, null, authentication);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootPath, ISearchOptions searchOptions, IDirectoryAuthentication authentication)
		{
			var searchResultList = new List<IDirectoryItem>();

			using(var searchRoot = this.GetDirectoryEntry(searchRootPath, authentication))
			{
				using(var directorySearcher = this.CreateDirectorySearcher(searchRoot, searchOptions))
				{
					using(var searchResults = directorySearcher.FindAll())
					{
						searchResultList.AddRange(from SearchResult searchResult in searchResults select this.CreateDirectoryItem(searchResult));
					}
				}
			}

			return searchResultList.ToArray();
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, ISearchOptions searchOptions, IDirectoryAuthentication authentication)
		{
			if(searchRootUrl == null)
				throw new ArgumentNullException("searchRootUrl");

			return this.Find(searchRootUrl.ToString(), searchOptions, authentication);
		}

		IDirectoryItem IDirectory.Get(string distinguishedName)
		{
			return this.Get(this.DistinguishedNameParser.Parse(distinguishedName));
		}

		public virtual IDirectoryItem Get(IDistinguishedName distinguishedName)
		{
			return this.Get(this.CreateDirectoryUri(distinguishedName), this.Authentication);
		}

		IDirectoryItem IGlobalDirectory.Get(string path)
		{
			return this.Get(path, null);
		}

		public virtual IDirectoryItem Get(IDirectoryUri url)
		{
			return this.Get(url, null);
		}

		public virtual IDirectoryItem Get(string path, IDirectoryAuthentication authentication)
		{
			using(var directoryEntry = this.GetDirectoryEntry(path, authentication))
			{
				return this.CreateDirectoryItem(directoryEntry);
			}
		}

		public virtual IDirectoryItem Get(IDirectoryUri url, IDirectoryAuthentication authentication)
		{
			if(url == null)
				throw new ArgumentNullException("url");

			return this.Get(url.ToString(), authentication);
		}

		#endregion

		//public virtual IDirectoryItem Create(IDirectoryItem parent, string name, string schemaClassName)
		//{
		//	throw new NotImplementedException();
		//}

		//public virtual bool Delete(string distinguishedName)
		//{
		//	using (var directoryEntry = this.GetDirectoryEntry(distinguishedName))
		//	{
		//		directoryEntry.DeleteTree();
		//		return true;
		//	}
		//}

		//public virtual IEnumerable<IDirectoryItem> GetChildren(string distinguishedName)
		//{
		//	var children = new List<IDirectoryItem>();

		//	using (var directoryEntry = this.GetDirectoryEntry(distinguishedName))
		//	{
		//		foreach (DirectoryEntry child in directoryEntry.Children)
		//		{
		//			using (child)
		//			{
		//				children.Add(this.CreateDirectoryItem(directoryEntry));
		//			}
		//		}
		//	}

		//	return children.ToArray();
		//}

		//public virtual object Invoke(IDirectoryItem DirectoryItem, string methodName, params object[] arguments)
		//{
		//	throw new NotImplementedException();
		//}

		//public virtual void Move(IDirectoryItem DirectoryItem, IDirectoryItem destination)
		//{
		//	throw new NotImplementedException();
		//}

		//public virtual void Rename(IDirectoryItem DirectoryItem, string name)
		//{
		//	throw new NotImplementedException();
		//}

		//public virtual void Save(IDirectoryItem DirectoryItem)
		//{
		//	throw new NotImplementedException();
		//}

		//#endregion
	}
}