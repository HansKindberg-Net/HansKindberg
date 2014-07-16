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
		private readonly ISingleSearchOptions _singleSearchOptions;

		#endregion

		#region Constructors

		public Directory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser) {}
		public Directory(IDirectoryConnection connection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser) : this(connection, directoryUriParser, distinguishedNameParser, new SearchOptions(), new SingleSearchOptions()) {}
		public Directory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions, ISingleSearchOptions singleSearchOptions) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser, searchOptions, singleSearchOptions) {}

		public Directory(IDirectoryConnection connection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions, ISingleSearchOptions singleSearchOptions)
		{
			if(connection == null)
				throw new ArgumentNullException("connection");

			if(directoryUriParser == null)
				throw new ArgumentNullException("directoryUriParser");

			if(distinguishedNameParser == null)
				throw new ArgumentNullException("distinguishedNameParser");

			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			if(singleSearchOptions == null)
				throw new ArgumentNullException("singleSearchOptions");

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
			this._singleSearchOptions = singleSearchOptions;
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

		public virtual ISingleSearchOptions SingleSearchOptions
		{
			get { return this._singleSearchOptions; }
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

		protected internal virtual DirectorySearcher CreateDirectorySearcher(DirectoryEntry searchRoot, ISearchOptions searchOptions)
		{
			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			var directorySearcher = this.CreateGeneralDirectorySearcher(searchRoot, searchOptions);

			if(searchOptions.PageSize != null)
				directorySearcher.PageSize = searchOptions.PageSize.Value;

			if(searchOptions.SearchScope != null)
				directorySearcher.SearchScope = searchOptions.SearchScope.Value;

			return directorySearcher;
		}

		protected internal virtual DirectorySearcher CreateDirectorySingleSearcher(DirectoryEntry searchRoot, ISingleSearchOptions singleSearchOptions)
		{
			var directorySingleSearcher = this.CreateGeneralDirectorySearcher(searchRoot, singleSearchOptions);

			directorySingleSearcher.SearchScope = SearchScope.Base;

			return directorySingleSearcher;
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

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller.")]
		protected internal virtual DirectorySearcher CreateGeneralDirectorySearcher(DirectoryEntry searchRoot, IGeneralSearchOptions generalSearchOptions)
		{
			if(generalSearchOptions == null)
				throw new ArgumentNullException("generalSearchOptions");

			var generalDirectorySearcher = new DirectorySearcher(searchRoot, generalSearchOptions.Filter, generalSearchOptions.PropertiesToLoad == null ? null : generalSearchOptions.PropertiesToLoad.ToArray());

			if(generalSearchOptions.Asynchronous != null)
				generalDirectorySearcher.Asynchronous = generalSearchOptions.Asynchronous.Value;

			if(generalSearchOptions.AttributeScopeQuery != null)
				generalDirectorySearcher.AttributeScopeQuery = generalSearchOptions.AttributeScopeQuery;

			if(generalSearchOptions.CacheResults != null)
				generalDirectorySearcher.CacheResults = generalSearchOptions.CacheResults.Value;

			if(generalSearchOptions.ClientTimeout != null)
				generalDirectorySearcher.ClientTimeout = generalSearchOptions.ClientTimeout.Value;

			if(generalSearchOptions.DereferenceAlias != null)
				generalDirectorySearcher.DerefAlias = generalSearchOptions.DereferenceAlias.Value;

			if(generalSearchOptions.DirectorySynchronization != null)
				generalDirectorySearcher.DirectorySynchronization = generalSearchOptions.DirectorySynchronization;

			if(generalSearchOptions.ExtendedDistinguishedName != null)
				generalDirectorySearcher.ExtendedDN = generalSearchOptions.ExtendedDistinguishedName.Value;

			if(generalSearchOptions.PropertyNamesOnly != null)
				generalDirectorySearcher.PropertyNamesOnly = generalSearchOptions.PropertyNamesOnly.Value;

			if(generalSearchOptions.ReferralChasing != null)
				generalDirectorySearcher.ReferralChasing = generalSearchOptions.ReferralChasing.Value;

			if(generalSearchOptions.SecurityMasks != null)
				generalDirectorySearcher.SecurityMasks = generalSearchOptions.SecurityMasks.Value;

			if(generalSearchOptions.ServerPageTimeLimit != null)
				generalDirectorySearcher.ServerPageTimeLimit = generalSearchOptions.ServerPageTimeLimit.Value;

			if(generalSearchOptions.ServerTimeLimit != null)
				generalDirectorySearcher.ServerTimeLimit = generalSearchOptions.ServerTimeLimit.Value;

			if(generalSearchOptions.SizeLimit != null)
				generalDirectorySearcher.SizeLimit = generalSearchOptions.SizeLimit.Value;

			if(generalSearchOptions.Sort != null)
				generalDirectorySearcher.Sort = generalSearchOptions.Sort;

			if(generalSearchOptions.Tombstone != null)
				generalDirectorySearcher.Tombstone = generalSearchOptions.Tombstone.Value;

			if(generalSearchOptions.VirtualListView != null)
				generalDirectorySearcher.VirtualListView = generalSearchOptions.VirtualListView;

			return generalDirectorySearcher;
		}

		bool IDirectory.Exists(string distinguishedName)
		{
			return this.Exists(this.DistinguishedNameParser.Parse(distinguishedName));
		}

		public virtual bool Exists(IDistinguishedName distinguishedName)
		{
			return this.Exists(this.CreateDirectoryUri(distinguishedName), this.Authentication);
		}

		bool IGlobalDirectory.Exists(string path)
		{
			return this.Exists(path, null);
		}

		public virtual bool Exists(IDirectoryUri url)
		{
			return this.Exists(url, null);
		}

		public virtual bool Exists(IDirectoryUri url, IDirectoryAuthentication authentication)
		{
			if(url == null)
				throw new ArgumentNullException("url");

			return this.Exists(url.ToString(), authentication);
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

			try
			{
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
			}
			catch(DirectoryServicesCOMException directoryServicesComException)
			{
				throw new DirectoryServicesException(directoryServicesComException);
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
			return this.Get(distinguishedName, this.SingleSearchOptions);
		}

		public virtual IDirectoryItem Get(IDistinguishedName distinguishedName)
		{
			return this.Get(distinguishedName, this.SingleSearchOptions);
		}

		IDirectoryItem IGlobalDirectory.Get(string path)
		{
			return this.Get(path, null, null);
		}

		public virtual IDirectoryItem Get(IDirectoryUri url)
		{
			return this.Get(url, null, null);
		}

		IDirectoryItem IDirectory.Get(string distinguishedName, ISingleSearchOptions singleSearchOptions)
		{
			return this.Get(this.DistinguishedNameParser.Parse(distinguishedName), singleSearchOptions);
		}

		public virtual IDirectoryItem Get(IDistinguishedName distinguishedName, ISingleSearchOptions singleSearchOptions)
		{
			return this.Get(this.CreateDirectoryUri(distinguishedName), singleSearchOptions, this.Authentication);
		}

		public virtual IDirectoryItem Get(string path, ISingleSearchOptions singleSearchOptions)
		{
			return this.Get(path, singleSearchOptions, null);
		}

		public virtual IDirectoryItem Get(IDirectoryUri url, ISingleSearchOptions singleSearchOptions)
		{
			return this.Get(url, singleSearchOptions, null);
		}

		public virtual IDirectoryItem Get(string path, IDirectoryAuthentication authentication)
		{
			return this.Get(path, null, authentication);
		}

		public virtual IDirectoryItem Get(IDirectoryUri url, IDirectoryAuthentication authentication)
		{
			return this.Get(url, null, authentication);
		}

		public virtual IDirectoryItem Get(string path, ISingleSearchOptions singleSearchOptions, IDirectoryAuthentication authentication)
		{
			try
			{
				using(var searchRoot = this.GetDirectoryEntry(path, authentication))
				{
					using(var directorySingleSearcher = this.CreateDirectorySingleSearcher(searchRoot, singleSearchOptions))
					{
						return this.CreateDirectoryItem(directorySingleSearcher.FindOne());
					}
				}
			}
			catch(DirectoryServicesCOMException directoryServicesComException)
			{
				throw new DirectoryServicesException(directoryServicesComException);
			}
		}

		public virtual IDirectoryItem Get(IDirectoryUri url, ISingleSearchOptions singleSearchOptions, IDirectoryAuthentication authentication)
		{
			if(url == null)
				throw new ArgumentNullException("url");

			return this.Get(url.ToString(), singleSearchOptions, authentication);
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