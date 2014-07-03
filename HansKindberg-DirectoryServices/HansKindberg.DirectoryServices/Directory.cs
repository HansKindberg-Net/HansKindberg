using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;

namespace HansKindberg.DirectoryServices
{
	public class Directory : IDirectory, IGlobalDirectory
	{
		#region Fields

		private AuthenticationTypes _authenticationTypes;
		private const Scheme _defaultScheme = Scheme.LDAP;
		private readonly string _defaultSearchFilter;
		private readonly SearchScope _defaultSearchScope;
		private readonly IDirectoryUriParser _directoryUriParser;
		private IDistinguishedName _distinguishedName;
		private readonly IDistinguishedNameParser _distinguishedNameParser;
		private string _host;
		private string _password;
		private int? _port;
		private Scheme _scheme;
		private readonly ISearchOptions _searchOptions;
		private string _userName;

		#endregion

		//public Directory() : this(new DirectoryConnection()) { }
		//public Directory(IDirectoryConnection directoryConnection) : this(directoryConnection, new SearchOptions(), new DirectoryUriParser()) { }
		//public Directory(ISearchOptions searchOptions) : this(new DirectoryConnection(), searchOptions, new DirectoryUriParser()) { }
		//public Directory(IDirectoryUriParser directoryUriParser) : this(new DirectoryConnection(), new SearchOptions(), directoryUriParser) { }
		//public Directory(IDirectoryConnection directoryConnection, ISearchOptions searchOptions) : this(directoryConnection, searchOptions, new DirectoryUriParser()) { }
		//public Directory(IDirectoryConnection directoryConnection, IDirectoryUriParser directoryUriParser) : this(directoryConnection, new SearchOptions(), directoryUriParser) { }
		//public Directory(ISearchOptions searchOptions, IDirectoryUriParser directoryUriParser) : this(new DirectoryConnection(), searchOptions, directoryUriParser) { }

		#region Constructors

		public Directory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser) {}
		public Directory(IDirectoryConnection directoryConnection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser) : this(directoryConnection, directoryUriParser, distinguishedNameParser, new SearchOptions()) {}
		public Directory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser, searchOptions) {}

		public Directory(IDirectoryConnection directoryConnection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions)
		{
			if(directoryConnection == null)
				throw new ArgumentNullException("directoryConnection");

			if(directoryUriParser == null)
				throw new ArgumentNullException("directoryUriParser");

			if(distinguishedNameParser == null)
				throw new ArgumentNullException("distinguishedNameParser");

			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			if(directoryConnection.AuthenticationTypes.HasValue)
			{
				this._authenticationTypes = directoryConnection.AuthenticationTypes.Value;
			}
			else
			{
				using(DirectoryEntry directoryEntry = new DirectoryEntry())
				{
					this._authenticationTypes = directoryEntry.AuthenticationType;
				}
			}

			this._directoryUriParser = directoryUriParser;
			this._distinguishedName = directoryConnection.DistinguishedName;
			this._distinguishedNameParser = distinguishedNameParser;
			this._host = directoryConnection.Host;
			this._password = directoryConnection.Password;
			this._port = directoryConnection.Port;
			this._scheme = directoryConnection.Scheme.HasValue ? directoryConnection.Scheme.Value : _defaultScheme;
			this._searchOptions = searchOptions;
			this._userName = directoryConnection.UserName;

			using(var directorySearcher = new DirectorySearcher())
			{
				this._defaultSearchFilter = directorySearcher.Filter;
				this._defaultSearchScope = directorySearcher.SearchScope;
			}
		}

		#endregion

		#region Properties

		public virtual AuthenticationTypes AuthenticationTypes
		{
			get { return this._authenticationTypes; }
			set { this._authenticationTypes = value; }
		}

		protected internal virtual string DefaultSearchFilter
		{
			get { return this._defaultSearchFilter; }
		}

		protected internal virtual SearchScope DefaultSearchScope
		{
			get { return this._defaultSearchScope; }
		}

		protected internal virtual IDirectoryUriParser DirectoryUriParser
		{
			get { return this._directoryUriParser; }
		}

		public virtual IDistinguishedName DistinguishedName
		{
			get { return this._distinguishedName; }
			set { this._distinguishedName = value; }
		}

		protected internal virtual IDistinguishedNameParser DistinguishedNameParser
		{
			get { return this._distinguishedNameParser; }
		}

		public virtual string Host
		{
			get { return this._host; }
			set { this._host = value; }
		}

		public virtual string Password
		{
			get { return this._password; }
			set { this._password = value; }
		}

		public virtual int? Port
		{
			get { return this._port; }
			set { this._port = value; }
		}

		public virtual IDirectoryItem Root
		{
			get { return this.Get(this.Url); }
		}

		public virtual Scheme Scheme
		{
			get { return this._scheme; }
			set { this._scheme = value; }
		}

		protected internal virtual ISearchOptions SearchOptions
		{
			get { return this._searchOptions; }
		}

		public virtual IDirectoryUri Url
		{
			get { return this.CreateDirectoryUri(this.DistinguishedName); }
		}

		public virtual string UserName
		{
			get { return this._userName; }
			set { this._userName = value; }
		}

		#endregion

		#region Methods

		protected internal virtual IDirectoryItem CreateDirectoryNode(DirectoryEntry directoryEntry)
		{
			if(directoryEntry == null)
				return null;

			try
			{
				using(var directorySearcher = new DirectorySearcher(directoryEntry))
				{
					directorySearcher.SearchScope = SearchScope.Base;
					return this.CreateDirectoryNode(directorySearcher.FindOne());
				}
			}
			catch(NotSupportedException)
			{
				var directoryNode = new DirectoryItem
				{
					Url = this.DirectoryUriParser.Parse(directoryEntry.Path)
				};

				foreach(string propertyName in directoryEntry.Properties.PropertyNames)
				{
					directoryNode.Properties.Add(propertyName, directoryEntry.Properties[propertyName].Value);
				}

				return directoryNode;
			}
		}

		protected internal virtual IDirectoryItem CreateDirectoryNode(SearchResult searchResult)
		{
			if(searchResult == null)
				return null;

			var directoryNode = new DirectoryItem
			{
				Url = this.CreateDirectoryUri(searchResult.Path)
			};

			if(searchResult.Properties != null && searchResult.Properties.PropertyNames != null)
			{
				foreach(string propertyName in searchResult.Properties.PropertyNames)
				{
					var resultPropertyValueCollection = searchResult.Properties[propertyName];

					var propertyValue = resultPropertyValueCollection.Count == 0 ? null : (resultPropertyValueCollection.Count == 1 ? resultPropertyValueCollection[0] : resultPropertyValueCollection.Cast<object>().ToArray());

					directoryNode.Properties.Add(propertyName, propertyValue);
				}
			}

			return directoryNode;
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller.")]
		protected internal virtual DirectorySearcher CreateDirectorySearcher(DirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			searchOptions = searchOptions ?? this.SearchOptions;

			filter = this.GetSearchFilter(filter, searchOptions);

			propertiesToLoad = this.GetPropertiesToLoad(propertiesToLoad, searchOptions);

			searchScope = this.GetSearchScope(searchScope, searchOptions);

			// ReSharper disable PossibleMultipleEnumeration
			var directorySearcher = new DirectorySearcher(searchRoot, filter, propertiesToLoad != null ? propertiesToLoad.ToArray() : null, searchScope.Value);
			// ReSharper restore PossibleMultipleEnumeration

			if(searchOptions != null)
			{
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
			}

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
			return this.Find((string) null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string filter)
		{
			return this.Find(filter, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(ISearchOptions searchOptions)
		{
			return this.Find(null, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string filter, ISearchOptions searchOptions)
		{
			return this.Find(this.DistinguishedName, filter, null, null, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad)
		{
			return this.Find(searchRootDistinguishedName, filter, propertiesToLoad, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad)
		{
			return this.Find(searchRootDistinguishedName, filter, propertiesToLoad, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, SearchScope? searchScope)
		{
			return this.Find(searchRootDistinguishedName, filter, null, searchScope);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, SearchScope? searchScope)
		{
			return this.Find(searchRootDistinguishedName, filter, null, searchScope);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, ISearchOptions searchOptions)
		{
			return this.Find(searchRootDistinguishedName, filter, null, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, ISearchOptions searchOptions)
		{
			return this.Find(searchRootDistinguishedName, filter, null, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope)
		{
			return this.Find(searchRootDistinguishedName, filter, propertiesToLoad, searchScope, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope)
		{
			return this.Find(searchRootDistinguishedName, filter, propertiesToLoad, searchScope, null);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			return this.Find(searchRootDistinguishedName, filter, null, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			return this.Find(searchRootDistinguishedName, filter, null, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			return this.Find(this.DistinguishedNameParser.Parse(searchRootDistinguishedName), filter, propertiesToLoad, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			if(searchRootDistinguishedName == null)
				throw new ArgumentNullException("searchRootDistinguishedName");

			return this.Find(this.CreateDirectoryUri(searchRootDistinguishedName), filter, propertiesToLoad, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			if(searchRootUrl == null)
				throw new ArgumentNullException("searchRootUrl");

			return ((IGlobalDirectory) this).Find(searchRootUrl.ToString(), filter, propertiesToLoad, searchScope, searchOptions);
		}

		IEnumerable<IDirectoryItem> IGlobalDirectory.Find(string searchRootPath, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			return this.Find(searchRootPath, this.UserName, this.Password, filter, propertiesToLoad, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, string userName, string password, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			if(searchRootUrl == null)
				throw new ArgumentNullException("searchRootUrl");

			return this.Find(searchRootUrl.ToString(), userName, password, filter, propertiesToLoad, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootPath, string userName, string password, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			return this.Find(searchRootPath, userName, password, this.AuthenticationTypes, filter, propertiesToLoad, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, string userName, string password, AuthenticationTypes authenticationTypes, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			if(searchRootUrl == null)
				throw new ArgumentNullException("searchRootUrl");

			return this.Find(searchRootUrl.ToString(), userName, password, authenticationTypes, filter, propertiesToLoad, searchScope, searchOptions);
		}

		public virtual IEnumerable<IDirectoryItem> Find(string searchRootPath, string userName, string password, AuthenticationTypes authenticationTypes, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions)
		{
			var searchResultList = new List<IDirectoryItem>();

			using(var searchRoot = this.GetDirectoryEntry(searchRootPath, userName, password, authenticationTypes))
			{
				using(var directorySearcher = this.CreateDirectorySearcher(searchRoot, filter, propertiesToLoad, searchScope, searchOptions))
				{
					using(var searchResults = directorySearcher.FindAll())
					{
						searchResultList.AddRange(from SearchResult searchResult in searchResults select this.CreateDirectoryNode(searchResult));
					}
				}
			}

			return searchResultList.ToArray();
		}

		public virtual IDirectoryItem Get(string distinguishedName)
		{
			return this.Get(this.DistinguishedNameParser.Parse(distinguishedName));
		}

		public virtual IDirectoryItem Get(IDistinguishedName distinguishedName)
		{
			return this.Get(this.CreateDirectoryUri(distinguishedName), this.UserName, this.Password, this.AuthenticationTypes);
		}

		IDirectoryItem IGlobalDirectory.Get(string path)
		{
			return this.Get(path, this.UserName, this.Password, this.AuthenticationTypes);
		}

		public virtual IDirectoryItem Get(IDirectoryUri url)
		{
			return this.Get(url, this.UserName, this.Password, this.AuthenticationTypes);
		}

		public virtual IDirectoryItem Get(string path, string userName, string password)
		{
			return this.Get(path, userName, password, this.AuthenticationTypes);
		}

		public virtual IDirectoryItem Get(IDirectoryUri url, string userName, string password)
		{
			return this.Get(url, userName, password, this.AuthenticationTypes);
		}

		public virtual IDirectoryItem Get(string path, string userName, string password, AuthenticationTypes authenticationTypes)
		{
			using(var directoryEntry = this.GetDirectoryEntry(path, userName, password, authenticationTypes))
			{
				return this.CreateDirectoryNode(directoryEntry);
			}
		}

		public virtual IDirectoryItem Get(IDirectoryUri url, string userName, string password, AuthenticationTypes authenticationTypes)
		{
			if(url == null)
				throw new ArgumentNullException("url");

			return this.Get(url.ToString(), userName, password, authenticationTypes);
		}

		protected internal virtual DirectoryEntry GetDirectoryEntry(string path, string userName, string password, AuthenticationTypes authenticationTypes)
		{
			return new DirectoryEntry(path, userName, password, authenticationTypes);
		}

		protected internal virtual IEnumerable<string> GetPropertiesToLoad(IEnumerable<string> propertiesToLoad, ISearchOptions searchOptions)
		{
			if(propertiesToLoad != null)
				return propertiesToLoad;

			if(searchOptions != null)
				propertiesToLoad = searchOptions.PropertiesToLoad;

			return propertiesToLoad;
		}

		protected internal virtual string GetSearchFilter(string filter, ISearchOptions searchOptions)
		{
			if(filter != null)
				return filter;

			if(searchOptions != null)
				filter = searchOptions.Filter;

			if(filter != null)
				return filter;

			return this.DefaultSearchFilter;
		}

		protected internal virtual SearchScope GetSearchScope(SearchScope? searchScope, ISearchOptions searchOptions)
		{
			if(searchScope != null)
				return searchScope.Value;

			if(searchOptions != null)
				searchScope = searchOptions.SearchScope;

			if(searchScope != null)
				return searchScope.Value;

			return this.DefaultSearchScope;
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

		//public virtual IEnumerable<IReadOnlyDirectoryNode> Find(string filter, IEnumerable<string> propertiesToLoad, SearchScope? scope, ISearchOptions searchOptions)
		//{
		//	throw new NotImplementedException();
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
		//				children.Add(this.CreateDirectoryNode(directoryEntry));
		//			}
		//		}
		//	}

		//	return children.ToArray();
		//}

		//protected internal virtual DirectoryEntry GetDirectoryEntry(IDirectoryUri directoryUri)
		//{
		//	if (directoryUri == null)
		//		throw new ArgumentNullException("directoryUri");

		//	return new DirectoryEntry(directoryUri.ToString(), this.UserName, this.Password, this.AuthenticationTypes);
		//}

		//protected internal virtual DirectoryEntry GetDirectoryEntry(string distinguishedName)
		//{
		//	return this.GetDirectoryEntry(this.CreateDirectoryUri(distinguishedName));
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

		////public virtual IDisposableEnumerable<ISearchResult> Find()
		////{
		////	return this.Find(null, null, null, null, null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(null, null, null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), null, null, null, null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(string filter)
		////{
		////	return this.Find(null, new ValueContainer<string>(filter), null, null, null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot, IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), null, null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(string filter, IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(null, new ValueContainer<string>(filter), null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot, string filter)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), null, null, null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(string filter, IEnumerable<string> propertiesToLoad)
		////{
		////	return this.Find(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot, string filter, IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope)
		////{
		////	return this.Find(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), null);
		////}

		////public virtual IDisposableEnumerable<ISearchResult> Find(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions)
		////{
		////	return this.Find(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		////}

		////[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		////protected internal virtual IDisposableEnumerable<ISearchResult> Find(ValueContainer<IDirectoryEntry> searchRoot, ValueContainer<string> filter, ValueContainer<IEnumerable<string>> propertiesToLoad, ValueContainer<SearchScope> scope, ValueContainer<IDirectorySearcherOptions> directorySearcherOptions)
		////{
		////	return null;

		////	//List<ISearchResult> searchResultList = new List<ISearchResult>();

		////	//using(DirectorySearcher directorySearcher = this.CreateDirectorySearcher(searchRoot, filter, propertiesToLoad, scope, directorySearcherOptions))
		////	//{
		////	//	searchResultList.AddRange((from SearchResult searchResult in directorySearcher.FindAll() select (SearchResultWrapper) searchResult).Cast<ISearchResult>());
		////	//}

		////	//return searchResultList.ToArray();
		////}

		////protected internal virtual DirectoryEntry GetConcreteDirectoryEntry(string distinguishedName)
		////{
		////	return this.GetConcreteDirectoryEntry(this.CreateDirectoryUri(this.Scheme, this.Host, this.Port, distinguishedName));
		////}

		////protected internal virtual DirectoryEntry GetConcreteDirectoryEntry(IDirectoryUri url)
		////{
		////	return this.GetConcreteDirectoryEntry(url, this.UserName, this.Password, this.AuthenticationTypes);
		////}

		////protected internal virtual DirectoryEntry GetConcreteDirectoryEntry(IDirectoryUri url, string userName, string password, AuthenticationTypes authenticationTypes)
		////{
		////	if(url == null)
		////		throw new ArgumentNullException("url");

		////	return new DirectoryEntry(url.ToString(), userName, password, authenticationTypes);
		////}

		////protected internal virtual DirectoryEntry GetConcreteDirectoryEntry(Scheme scheme, string host, int? port, string distinguishedName, string userName, string password, AuthenticationTypes authenticationTypes)
		////{
		////	return new DirectoryEntry(this.CreateDirectoryUri(scheme, host, port, distinguishedName).ToString(), userName, password, authenticationTypes);
		////}

		//////[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		////protected internal virtual DirectoryEntry GetConcreteRoot()
		////{
		////	return this.GetConcreteDirectoryEntry(this.Url);
		////}

		//////[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by caller, IDirectoryEntry.Dispose().")]
		////public virtual IDirectoryEntry GetDirectoryEntry(string path)
		////{
		////	return new DirectoryEntryWrapper(this.GetConcreteDirectoryEntry(path));
		////}

		////public virtual IDirectoryEntry GetDirectoryEntry(IDirectoryUri url)
		////{
		////	throw new NotImplementedException();
		////}

		////public virtual IDirectoryEntry GetDirectoryEntry(Scheme scheme, string host, int? port, string distinguishedName)
		////{
		////	throw new NotImplementedException();
		////}

		////public virtual IDirectoryEntry GetDirectoryEntry(IDirectoryUri url, AuthenticationTypes authenticationTypes, string userName, string password)
		////{
		////	throw new NotImplementedException();
		////}

		////public virtual IDirectoryEntry GetDirectoryEntry(Scheme scheme, string host, int? port, string distinguishedName, AuthenticationTypes authenticationTypes, string userName, string password)
		////{
		////	throw new NotImplementedException();
		////}

		//////public virtual string GetPath(string distinguishedName)
		//////{
		//////	return this.HostUrl + distinguishedName;
		//////}
		////[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by caller, IDirectoryEntry.Dispose().")]
		////public virtual IDirectoryEntry GetRoot()
		////{
		////	return this.GetDirectoryEntry(this.RootPath);
		////}
	}
}