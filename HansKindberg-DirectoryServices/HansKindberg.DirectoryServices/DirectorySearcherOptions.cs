using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public class DirectorySearcherOptions : IDirectorySearcherOptions
	{
		#region Properties

		public virtual bool? Asynchronous { get; set; }
		public virtual ValueContainer<string> AttributeScopeQuery { get; set; }
		public virtual bool? CacheResults { get; set; }
		public virtual TimeSpan? ClientTimeout { get; set; }
		public virtual DereferenceAlias? DereferenceAlias { get; set; }
		public virtual ValueContainer<DirectorySynchronization> DirectorySynchronization { get; set; }
		public virtual ExtendedDN? ExtendedDistinguishedName { get; set; }
		public virtual ValueContainer<string> Filter { get; set; }
		public virtual int? PageSize { get; set; }
		public virtual ValueContainer<IEnumerable<string>> PropertiesToLoad { get; set; }
		public virtual bool? PropertyNamesOnly { get; set; }
		public virtual ReferralChasingOption? ReferralChasing { get; set; }
		public virtual SearchScope? SearchScope { get; set; }
		public virtual SecurityMasks? SecurityMasks { get; set; }
		public virtual TimeSpan? ServerPageTimeLimit { get; set; }
		public virtual TimeSpan? ServerTimeLimit { get; set; }
		public virtual int? SizeLimit { get; set; }
		public virtual ValueContainer<SortOption> Sort { get; set; }
		public virtual bool? Tombstone { get; set; }
		public virtual ValueContainer<DirectoryVirtualListView> VirtualListView { get; set; }

		#endregion
	}
}