using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;

namespace HansKindberg.DirectoryServices
{
	public class CachedDirectory : Directory, ICachedDirectory
	{
		#region Fields

		private const char _cacheKeyComponentDelimiter = '&';
		private static readonly string _cacheKeyPrefix = typeof(CachedDirectory).FullName;
		private const char _cacheKeyValueDelimiter = '=';
		private IDirectoryCache _directoryCache;
		private readonly IDirectoryCacheFactory _directoryCacheFactory;

		#endregion

		#region Constructors

		public CachedDirectory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, IDirectoryCacheFactory directoryCacheFactory) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser, directoryCacheFactory) {}
		public CachedDirectory(IDirectoryConnection connection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, IDirectoryCacheFactory directoryCacheFactory) : this(connection, directoryUriParser, distinguishedNameParser, new SearchOptions(), new SingleSearchOptions(), directoryCacheFactory) {}
		public CachedDirectory(IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions, ISingleSearchOptions singleSearchOptions, IDirectoryCacheFactory directoryCacheFactory) : this(new DirectoryConnection(), directoryUriParser, distinguishedNameParser, searchOptions, singleSearchOptions, directoryCacheFactory) {}

		public CachedDirectory(IDirectoryConnection connection, IDirectoryUriParser directoryUriParser, IDistinguishedNameParser distinguishedNameParser, ISearchOptions searchOptions, ISingleSearchOptions singleSearchOptions, IDirectoryCacheFactory directoryCacheFactory) : base(connection, directoryUriParser, distinguishedNameParser, searchOptions, singleSearchOptions)
		{
			if(directoryCacheFactory == null)
				throw new ArgumentNullException("directoryCacheFactory");

			this._directoryCacheFactory = directoryCacheFactory;
		}

		#endregion

		#region Properties

		public virtual IDirectoryCache Cache
		{
			get { return this._directoryCache ?? (this._directoryCache = this.DirectoryCacheFactory.Create(this.CacheKeyPrefix)); }
		}

		protected internal virtual char CacheKeyComponentDelimiter
		{
			get { return _cacheKeyComponentDelimiter; }
		}

		protected internal virtual string CacheKeyPrefix
		{
			get { return _cacheKeyPrefix; }
		}

		protected internal virtual char CacheKeyValueDelimiter
		{
			get { return _cacheKeyValueDelimiter; }
		}

		protected internal virtual IDirectoryCacheFactory DirectoryCacheFactory
		{
			get { return this._directoryCacheFactory; }
		}

		#endregion

		#region Methods

		public virtual void ClearCache()
		{
			this.Cache.Clear();
		}

		protected internal virtual string CreateCacheKey(string method, string path)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}{6}", new object[] {this.CacheKeyPrefix, ".", method, ":", "Path", this.CacheKeyValueDelimiter, (path ?? string.Empty).ToUpperInvariant()});
		}

		protected internal virtual string CreateCacheKey(string method, string path, ISearchOptions searchOptions)
		{
			return this.CreateCacheKey(new List<string> {{this.CreateCacheKey(method, path)}, {this.CreateSearchOptionsCacheKey(searchOptions)}});
		}

		protected internal virtual string CreateCacheKey(string method, string path, ISingleSearchOptions singleSearchOptions)
		{
			return this.CreateCacheKey(new List<string> {{this.CreateCacheKey(method, path)}, {this.CreateSingleSearchOptionsCacheKey(singleSearchOptions)}});
		}

		protected internal virtual string CreateCacheKey(IEnumerable<string> cacheKeyComponents)
		{
			return string.Join(this.CacheKeyComponentDelimiter.ToString(CultureInfo.InvariantCulture), cacheKeyComponents.ToArray());
		}

		/// <summary>
		/// This may not be correct. You have to override this if you want to use it.
		/// </summary>
		protected internal virtual object CreateDirectorySynchronizationCacheKeyValue(DirectorySynchronization directorySynchronization)
		{
			if(directorySynchronization == null)
				return null;

			return directorySynchronization.Option;
		}

		/// <summary>
		/// This may not be correct. You have to override this if you want to use it.
		/// </summary>
		protected internal virtual object CreateDirectoryVirtualListViewCacheKeyValue(DirectoryVirtualListView directoryVirtualListView)
		{
			if(directoryVirtualListView == null)
				return null;

			return directoryVirtualListView.AfterCount + "," + directoryVirtualListView.ApproximateTotal + "," + directoryVirtualListView.BeforeCount + "," + directoryVirtualListView.Offset + "," + directoryVirtualListView.Target + "," + directoryVirtualListView.TargetPercentage;
		}

		protected internal virtual string CreateGeneralSearchOptionsCacheKey(IGeneralSearchOptions generalSearchOptions, SearchScope? searchScope, int? pageSize)
		{
			if(generalSearchOptions == null)
				throw new ArgumentNullException("generalSearchOptions");

			var cacheKeyDictionary = new Dictionary<string, object>();

			if(generalSearchOptions.Asynchronous != null)
				cacheKeyDictionary.Add("Asynchronous", generalSearchOptions.Asynchronous.Value);

			if(generalSearchOptions.AttributeScopeQuery != null)
				cacheKeyDictionary.Add("AttributeScopeQuery", generalSearchOptions.AttributeScopeQuery.ToUpperInvariant());

			if(generalSearchOptions.CacheResults != null)
				cacheKeyDictionary.Add("CacheResults", generalSearchOptions.CacheResults.Value);

			if(generalSearchOptions.ClientTimeout != null)
				cacheKeyDictionary.Add("ClientTimeout", generalSearchOptions.ClientTimeout.Value);

			if(generalSearchOptions.DereferenceAlias != null)
				cacheKeyDictionary.Add("DereferenceAlias", generalSearchOptions.DereferenceAlias.Value);

			if(generalSearchOptions.DirectorySynchronization != null)
				cacheKeyDictionary.Add("DirectorySynchronization", this.CreateDirectorySynchronizationCacheKeyValue(generalSearchOptions.DirectorySynchronization));

			if(generalSearchOptions.ExtendedDistinguishedName != null)
				cacheKeyDictionary.Add("ExtendedDistinguishedName", generalSearchOptions.ExtendedDistinguishedName.Value);

			if(generalSearchOptions.Filter != null)
				cacheKeyDictionary.Add("Filter", generalSearchOptions.Filter.ToUpperInvariant());

			if(pageSize != null)
				cacheKeyDictionary.Add("PageSize", pageSize.Value);

			if(generalSearchOptions.PropertiesToLoad != null)
				cacheKeyDictionary.Add("PropertiesToLoad", string.Join(",", generalSearchOptions.PropertiesToLoad.Select(propertyToLoad => propertyToLoad.ToUpperInvariant()).ToArray()));

			if(generalSearchOptions.PropertyNamesOnly != null)
				cacheKeyDictionary.Add("PropertyNamesOnly", generalSearchOptions.PropertyNamesOnly.Value);

			if(generalSearchOptions.ReferralChasing != null)
				cacheKeyDictionary.Add("ReferralChasing", generalSearchOptions.ReferralChasing.Value);

			if(searchScope != null)
				cacheKeyDictionary.Add("SearchScope", searchScope.Value);

			if(generalSearchOptions.SecurityMasks != null)
				cacheKeyDictionary.Add("SecurityMasks", generalSearchOptions.SecurityMasks.Value);

			if(generalSearchOptions.ServerPageTimeLimit != null)
				cacheKeyDictionary.Add("ServerPageTimeLimit", generalSearchOptions.ServerPageTimeLimit.Value);

			if(generalSearchOptions.ServerTimeLimit != null)
				cacheKeyDictionary.Add("ServerTimeLimit", generalSearchOptions.ServerTimeLimit.Value);

			if(generalSearchOptions.SizeLimit != null)
				cacheKeyDictionary.Add("SizeLimit", generalSearchOptions.SizeLimit.Value);

			if(generalSearchOptions.Sort != null)
				cacheKeyDictionary.Add("Sort", generalSearchOptions.Sort.PropertyName + "," + generalSearchOptions.Sort.Direction);

			if(generalSearchOptions.Tombstone != null)
				cacheKeyDictionary.Add("Tombstone", generalSearchOptions.Tombstone.Value);

			if(generalSearchOptions.VirtualListView != null)
				cacheKeyDictionary.Add("VirtualListView", this.CreateDirectoryVirtualListViewCacheKeyValue(generalSearchOptions.VirtualListView));

			return string.Join(this.CacheKeyComponentDelimiter.ToString(CultureInfo.InvariantCulture), cacheKeyDictionary.Select(item => string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", item.Key, this.CacheKeyValueDelimiter, item.Value)).ToArray());
		}

		protected internal virtual string CreateSearchOptionsCacheKey(ISearchOptions searchOptions)
		{
			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			return this.CreateGeneralSearchOptionsCacheKey(searchOptions, searchOptions.SearchScope, searchOptions.PageSize);
		}

		protected internal virtual string CreateSingleSearchOptionsCacheKey(ISingleSearchOptions singleSearchOptions)
		{
			if(singleSearchOptions == null)
				throw new ArgumentNullException("singleSearchOptions");

			return this.CreateGeneralSearchOptionsCacheKey(singleSearchOptions, SearchScope.Base, null);
		}

		public override bool Exists(string path, IDirectoryAuthentication authentication)
		{
			return (bool) this.Get(this.CreateCacheKey("Exists", path), () => (object) this.ExistsInternal(path, authentication));
		}

		protected internal virtual bool ExistsInternal(string path, IDirectoryAuthentication authentication)
		{
			return base.Exists(path, authentication);
		}

		public override IEnumerable<IDirectoryItem> Find(string searchRootPath, ISearchOptions searchOptions, IDirectoryAuthentication authentication)
		{
			return this.Get(this.CreateCacheKey("Find", searchRootPath, searchOptions), () => this.FindInternal(searchRootPath, searchOptions, authentication));
		}

		protected internal virtual IEnumerable<IDirectoryItem> FindInternal(string searchRootPath, ISearchOptions searchOptions, IDirectoryAuthentication authentication)
		{
			return base.Find(searchRootPath, searchOptions, authentication);
		}

		public override IDirectoryItem Get(string path, ISingleSearchOptions singleSearchOptions, IDirectoryAuthentication authentication)
		{
			return this.Get(this.CreateCacheKey("Get", path, singleSearchOptions), () => this.GetInternal(path, singleSearchOptions, authentication));
		}

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		protected internal virtual T Get<T>(string cacheKey, Func<T> populateDelegate) where T : class
		{
			if(cacheKey == null)
				throw new ArgumentNullException("cacheKey");

			if(populateDelegate == null)
				throw new ArgumentNullException("populateDelegate");

			var value = this.Cache.Get(cacheKey) as T;

			if(value == null)
			{
				value = populateDelegate();

				if(value != null)
					this.Cache.Set(cacheKey, value);
			}

			return value;
		}

		protected internal virtual IDirectoryItem GetInternal(string path, ISingleSearchOptions singleSearchOptions, IDirectoryAuthentication authentication)
		{
			return base.Get(path, singleSearchOptions, authentication);
		}

		#endregion
	}
}