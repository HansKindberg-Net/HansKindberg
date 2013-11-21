using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;

namespace HansKindberg.DirectoryServices
{
	public class Directory : IDirectory
	{
		#region Fields

		private readonly IConnectionSettings _connectionSettings;
		private readonly IDirectorySearcherOptions _directorySearcherOptions;
		private readonly string _hostUrl;
		private readonly string _rootPath;

		#endregion

		#region Constructors

		public Directory(IConnectionSettings connectionSettings) : this(connectionSettings, null) {}

		public Directory(IConnectionSettings connectionSettings, IDirectorySearcherOptions directorySearcherOptions)
		{
			if(connectionSettings == null)
				throw new ArgumentNullException("connectionSettings");

			this._connectionSettings = connectionSettings;
			this._directorySearcherOptions = directorySearcherOptions;

			string hostUrl = connectionSettings.Scheme.ToString() + "://" + connectionSettings.Host;
			if(connectionSettings.Port != null)
				hostUrl += ":" + connectionSettings.Port.Value.ToString(CultureInfo.InvariantCulture);
			if(!hostUrl.EndsWith("/", StringComparison.Ordinal))
				hostUrl += "/";

			this._hostUrl = hostUrl;
			this._rootPath = hostUrl + connectionSettings.DistinguishedName;
		}

		#endregion

		#region Properties

		public virtual string HostUrl
		{
			get { return this._hostUrl; }
		}

		protected internal virtual string RootPath
		{
			get { return this._rootPath; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected internal virtual DirectorySearcher CreateDirectorySearcher(ValueContainer<IDirectoryEntry> searchRoot, ValueContainer<string> filter, ValueContainer<IEnumerable<string>> propertiesToLoad, ValueContainer<SearchScope> scope, ValueContainer<IDirectorySearcherOptions> directorySearcherOptions)
		{
			DirectorySearcher directorySearcher = new DirectorySearcher();

			if(searchRoot != null)
				directorySearcher.SearchRoot = searchRoot.Value == null ? null : this.GetConcreteDirectoryEntry(searchRoot.Value.Path);

			if(filter != null)
				directorySearcher.Filter = filter.Value;

			if(propertiesToLoad != null)
			{
				directorySearcher.PropertiesToLoad.Clear();

				foreach(string propertyToLoad in propertiesToLoad.Value ?? new string[0])
				{
					directorySearcher.PropertiesToLoad.Add(propertyToLoad);
				}
			}

			if(scope != null)
				directorySearcher.SearchScope = scope.Value;

			IDirectorySearcherOptions actualDirectorySearcherOptions = directorySearcherOptions != null ? directorySearcherOptions.Value : this._directorySearcherOptions;

			this.SetDirectorySearcherOptions(directorySearcher, actualDirectorySearcherOptions, filter, propertiesToLoad, scope);

			return directorySearcher;
		}

		public virtual IEnumerable<ISearchResult> FindAll()
		{
			return this.FindAll(null, null, null, null, null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(null, null, null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), null, null, null, null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(string filter)
		{
			return this.FindAll(null, new ValueContainer<string>(filter), null, null, null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), null, null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual IEnumerable<ISearchResult> FindAll(string filter, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(null, new ValueContainer<string>(filter), null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), null, null, null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad)
		{
			return this.FindAll(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope)
		{
			return this.FindAll(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), null);
		}

		public virtual IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindAll(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual IEnumerable<ISearchResult> FindAll(ValueContainer<IDirectoryEntry> searchRoot, ValueContainer<string> filter, ValueContainer<IEnumerable<string>> propertiesToLoad, ValueContainer<SearchScope> scope, ValueContainer<IDirectorySearcherOptions> directorySearcherOptions)
		{
			List<ISearchResult> searchResultList = new List<ISearchResult>();

			using(DirectorySearcher directorySearcher = this.CreateDirectorySearcher(searchRoot, filter, propertiesToLoad, scope, directorySearcherOptions))
			{
				searchResultList.AddRange((from SearchResult searchResult in directorySearcher.FindAll() select (SearchResultWrapper) searchResult).Cast<ISearchResult>());
			}

			return searchResultList.ToArray();
		}

		public virtual ISearchResult FindOne()
		{
			return this.FindOne(null, null, null, null, null);
		}

		public virtual ISearchResult FindOne(IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(null, null, null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), null, null, null, null);
		}

		public virtual ISearchResult FindOne(string filter)
		{
			return this.FindOne(null, new ValueContainer<string>(filter), null, null, null);
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), null, null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual ISearchResult FindOne(string filter, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(null, new ValueContainer<string>(filter), null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot, string filter)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), null, null, null);
		}

		public virtual ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad)
		{
			return this.FindOne(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, null);
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), null, null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, null);
		}

		public virtual ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope)
		{
			return this.FindOne(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), null);
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), null, new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(null, new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), null);
		}

		public virtual ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions)
		{
			return this.FindOne(new ValueContainer<IDirectoryEntry>(searchRoot), new ValueContainer<string>(filter), new ValueContainer<IEnumerable<string>>(propertiesToLoad), new ValueContainer<SearchScope>(scope), new ValueContainer<IDirectorySearcherOptions>(directorySearcherOptions));
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual ISearchResult FindOne(ValueContainer<IDirectoryEntry> searchRoot, ValueContainer<string> filter, ValueContainer<IEnumerable<string>> propertiesToLoad, ValueContainer<SearchScope> scope, ValueContainer<IDirectorySearcherOptions> directorySearcherOptions)
		{
			using(DirectorySearcher directorySearcher = this.CreateDirectorySearcher(searchRoot, filter, propertiesToLoad, scope, directorySearcherOptions))
			{
				return (SearchResultWrapper) directorySearcher.FindOne();
			}
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by caller, IDirectoryEntry.Dispose().")]
		protected internal virtual DirectoryEntry GetConcreteDirectoryEntry(string path)
		{
			DirectoryEntry directoryEntry = new DirectoryEntry(path, this._connectionSettings.UserName, this._connectionSettings.Password);

			if(this._connectionSettings.AuthenticationTypes.HasValue)
				directoryEntry.AuthenticationType = this._connectionSettings.AuthenticationTypes.Value;

			return directoryEntry;
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected internal virtual DirectoryEntry GetConcreteRoot()
		{
			return this.GetConcreteDirectoryEntry(this.RootPath);
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by caller, IDirectoryEntry.Dispose().")]
		public virtual IDirectoryEntry GetDirectoryEntry(string path)
		{
			return new DirectoryEntryWrapper(this.GetConcreteDirectoryEntry(path));
		}

		public virtual string GetPath(string distinguishedName)
		{
			return this.HostUrl + distinguishedName;
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose must be handled by caller, IDirectoryEntry.Dispose().")]
		public virtual IDirectoryEntry GetRoot()
		{
			return this.GetDirectoryEntry(this.RootPath);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		protected internal virtual void SetDirectorySearcherOptions(DirectorySearcher directorySearcher, IDirectorySearcherOptions directorySearcherOptions, ValueContainer<string> filter, ValueContainer<IEnumerable<string>> propertiesToLoad, ValueContainer<SearchScope> scope)
		{
			if(directorySearcher == null)
				throw new ArgumentNullException("directorySearcher");

			if(directorySearcherOptions == null)
				return;

			if(filter == null && directorySearcherOptions.Filter != null)
				directorySearcher.Filter = directorySearcherOptions.Filter.Value;

			if(propertiesToLoad == null && directorySearcherOptions.PropertiesToLoad != null)
			{
				directorySearcher.PropertiesToLoad.Clear();

				foreach(string propertyToLoad in directorySearcherOptions.PropertiesToLoad.Value ?? new string[0])
				{
					directorySearcher.PropertiesToLoad.Add(propertyToLoad);
				}
			}

			if(scope == null && directorySearcherOptions.SearchScope.HasValue)
				directorySearcher.SearchScope = directorySearcherOptions.SearchScope.Value;

			if(directorySearcherOptions.Asynchronous.HasValue)
				directorySearcher.Asynchronous = directorySearcherOptions.Asynchronous.Value;

			if(directorySearcherOptions.AttributeScopeQuery != null)
				directorySearcher.AttributeScopeQuery = directorySearcherOptions.AttributeScopeQuery.Value;

			if(directorySearcherOptions.CacheResults.HasValue)
				directorySearcher.CacheResults = directorySearcherOptions.CacheResults.Value;

			if(directorySearcherOptions.ClientTimeout.HasValue)
				directorySearcher.ClientTimeout = directorySearcherOptions.ClientTimeout.Value;

			if(directorySearcherOptions.DereferenceAlias.HasValue)
				directorySearcher.DerefAlias = directorySearcherOptions.DereferenceAlias.Value;

			if(directorySearcherOptions.DirectorySynchronization != null)
				directorySearcher.DirectorySynchronization = directorySearcherOptions.DirectorySynchronization.Value;

			if(directorySearcherOptions.ExtendedDistinguishedName.HasValue)
				directorySearcher.ExtendedDN = directorySearcherOptions.ExtendedDistinguishedName.Value;

			if(directorySearcherOptions.PageSize.HasValue)
				directorySearcher.PageSize = directorySearcherOptions.PageSize.Value;

			if(directorySearcherOptions.PropertyNamesOnly.HasValue)
				directorySearcher.PropertyNamesOnly = directorySearcherOptions.PropertyNamesOnly.Value;

			if(directorySearcherOptions.ReferralChasing.HasValue)
				directorySearcher.ReferralChasing = directorySearcherOptions.ReferralChasing.Value;

			if(directorySearcherOptions.SecurityMasks.HasValue)
				directorySearcher.SecurityMasks = directorySearcherOptions.SecurityMasks.Value;

			if(directorySearcherOptions.ServerPageTimeLimit.HasValue)
				directorySearcher.ServerPageTimeLimit = directorySearcherOptions.ServerPageTimeLimit.Value;

			if(directorySearcherOptions.ServerTimeLimit.HasValue)
				directorySearcher.ServerTimeLimit = directorySearcherOptions.ServerTimeLimit.Value;

			if(directorySearcherOptions.SizeLimit.HasValue)
				directorySearcher.SizeLimit = directorySearcherOptions.SizeLimit.Value;

			if(directorySearcherOptions.Sort != null)
				directorySearcher.Sort = directorySearcherOptions.Sort.Value;

			if(directorySearcherOptions.Tombstone.HasValue)
				directorySearcher.Tombstone = directorySearcherOptions.Tombstone.Value;

			if(directorySearcherOptions.VirtualListView != null)
				directorySearcher.VirtualListView = directorySearcherOptions.VirtualListView.Value;
		}

		#endregion
	}
}