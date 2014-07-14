using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public class SearchOptions : ISearchOptions
	{
		#region Properties

		public virtual bool? Asynchronous { get; set; }
		public virtual string AttributeScopeQuery { get; set; }
		public virtual bool? CacheResults { get; set; }
		public virtual TimeSpan? ClientTimeout { get; set; }
		public virtual DereferenceAlias? DereferenceAlias { get; set; }
		public virtual DirectorySynchronization DirectorySynchronization { get; set; }
		public virtual ExtendedDN? ExtendedDistinguishedName { get; set; }
		public virtual string Filter { get; set; }
		public virtual int? PageSize { get; set; }
		public virtual IEnumerable<string> PropertiesToLoad { get; set; }
		public virtual bool? PropertyNamesOnly { get; set; }
		public virtual ReferralChasingOption? ReferralChasing { get; set; }
		public virtual SearchScope? SearchScope { get; set; }
		public virtual SecurityMasks? SecurityMasks { get; set; }
		public virtual TimeSpan? ServerPageTimeLimit { get; set; }
		public virtual TimeSpan? ServerTimeLimit { get; set; }
		public virtual int? SizeLimit { get; set; }
		public virtual SortOption Sort { get; set; }
		public virtual bool? Tombstone { get; set; }
		public virtual DirectoryVirtualListView VirtualListView { get; set; }

		#endregion

		#region Methods

		ISearchOptions ISearchOptions.Copy()
		{
			return this.Copy();
		}

		public virtual SearchOptions Copy()
		{
			var searchOptions = new SearchOptions();

			searchOptions.OverrideWith(this);

			return searchOptions;
		}

		ISearchOptions ISearchOptions.Copy(ISearchOptions searchOptionsToOverrideWith)
		{
			return this.Copy(searchOptionsToOverrideWith);
		}

		public virtual SearchOptions Copy(ISearchOptions searchOptionsToOverrideWith)
		{
			var searchOptions = this.Copy();

			searchOptions.OverrideWith(searchOptionsToOverrideWith);

			return searchOptions;
		}

		protected internal virtual void OverrideWith(ISearchOptions searchOptions)
		{
			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			if(searchOptions.Asynchronous != null)
				this.Asynchronous = searchOptions.Asynchronous;

			if(searchOptions.AttributeScopeQuery != null)
				this.AttributeScopeQuery = searchOptions.AttributeScopeQuery;

			if(searchOptions.CacheResults != null)
				this.CacheResults = searchOptions.CacheResults;

			if(searchOptions.ClientTimeout != null)
				this.ClientTimeout = searchOptions.ClientTimeout;

			if(searchOptions.DereferenceAlias != null)
				this.DereferenceAlias = searchOptions.DereferenceAlias;

			if(searchOptions.DirectorySynchronization != null)
				this.DirectorySynchronization = searchOptions.DirectorySynchronization;

			if(searchOptions.ExtendedDistinguishedName != null)
				this.ExtendedDistinguishedName = searchOptions.ExtendedDistinguishedName;

			if(searchOptions.Filter != null)
				this.Filter = searchOptions.Filter;

			if(searchOptions.PageSize != null)
				this.PageSize = searchOptions.PageSize;

			if(searchOptions.PropertiesToLoad != null)
				this.PropertiesToLoad = searchOptions.PropertiesToLoad;

			if(searchOptions.PropertyNamesOnly != null)
				this.PropertyNamesOnly = searchOptions.PropertyNamesOnly;

			if(searchOptions.ReferralChasing != null)
				this.ReferralChasing = searchOptions.ReferralChasing;

			if(searchOptions.SearchScope != null)
				this.SearchScope = searchOptions.SearchScope;

			if(searchOptions.SecurityMasks != null)
				this.SecurityMasks = searchOptions.SecurityMasks;

			if(searchOptions.ServerPageTimeLimit != null)
				this.ServerPageTimeLimit = searchOptions.ServerPageTimeLimit;

			if(searchOptions.ServerTimeLimit != null)
				this.ServerTimeLimit = searchOptions.ServerTimeLimit;

			if(searchOptions.SizeLimit != null)
				this.SizeLimit = searchOptions.SizeLimit;

			if(searchOptions.Sort != null)
				this.Sort = searchOptions.Sort;

			if(searchOptions.Tombstone != null)
				this.Tombstone = searchOptions.Tombstone;

			if(searchOptions.VirtualListView != null)
				this.VirtualListView = searchOptions.VirtualListView;
		}

		#endregion
	}
}