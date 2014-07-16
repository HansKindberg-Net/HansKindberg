using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public abstract class GeneralSearchOptions : IGeneralSearchOptions
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
		public virtual IEnumerable<string> PropertiesToLoad { get; set; }
		public virtual bool? PropertyNamesOnly { get; set; }
		public virtual ReferralChasingOption? ReferralChasing { get; set; }
		public virtual SecurityMasks? SecurityMasks { get; set; }
		public virtual TimeSpan? ServerPageTimeLimit { get; set; }
		public virtual TimeSpan? ServerTimeLimit { get; set; }
		public virtual int? SizeLimit { get; set; }
		public virtual SortOption Sort { get; set; }
		public virtual bool? Tombstone { get; set; }
		public virtual DirectoryVirtualListView VirtualListView { get; set; }

		#endregion

		#region Methods

		protected internal virtual void OverrideWith(IGeneralSearchOptions generalSearchOptions)
		{
			if(generalSearchOptions == null)
				throw new ArgumentNullException("generalSearchOptions");

			if(generalSearchOptions.Asynchronous != null)
				this.Asynchronous = generalSearchOptions.Asynchronous;

			if(generalSearchOptions.AttributeScopeQuery != null)
				this.AttributeScopeQuery = generalSearchOptions.AttributeScopeQuery;

			if(generalSearchOptions.CacheResults != null)
				this.CacheResults = generalSearchOptions.CacheResults;

			if(generalSearchOptions.ClientTimeout != null)
				this.ClientTimeout = generalSearchOptions.ClientTimeout;

			if(generalSearchOptions.DereferenceAlias != null)
				this.DereferenceAlias = generalSearchOptions.DereferenceAlias;

			if(generalSearchOptions.DirectorySynchronization != null)
				this.DirectorySynchronization = generalSearchOptions.DirectorySynchronization;

			if(generalSearchOptions.ExtendedDistinguishedName != null)
				this.ExtendedDistinguishedName = generalSearchOptions.ExtendedDistinguishedName;

			if(generalSearchOptions.Filter != null)
				this.Filter = generalSearchOptions.Filter;

			if(generalSearchOptions.PropertiesToLoad != null)
				this.PropertiesToLoad = generalSearchOptions.PropertiesToLoad;

			if(generalSearchOptions.PropertyNamesOnly != null)
				this.PropertyNamesOnly = generalSearchOptions.PropertyNamesOnly;

			if(generalSearchOptions.ReferralChasing != null)
				this.ReferralChasing = generalSearchOptions.ReferralChasing;

			if(generalSearchOptions.SecurityMasks != null)
				this.SecurityMasks = generalSearchOptions.SecurityMasks;

			if(generalSearchOptions.ServerPageTimeLimit != null)
				this.ServerPageTimeLimit = generalSearchOptions.ServerPageTimeLimit;

			if(generalSearchOptions.ServerTimeLimit != null)
				this.ServerTimeLimit = generalSearchOptions.ServerTimeLimit;

			if(generalSearchOptions.SizeLimit != null)
				this.SizeLimit = generalSearchOptions.SizeLimit;

			if(generalSearchOptions.Sort != null)
				this.Sort = generalSearchOptions.Sort;

			if(generalSearchOptions.Tombstone != null)
				this.Tombstone = generalSearchOptions.Tombstone;

			if(generalSearchOptions.VirtualListView != null)
				this.VirtualListView = generalSearchOptions.VirtualListView;
		}

		#endregion
	}
}