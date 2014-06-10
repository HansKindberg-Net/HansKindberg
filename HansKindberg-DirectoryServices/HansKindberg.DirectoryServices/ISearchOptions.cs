using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface ISearchOptions
	{
		#region Properties

		bool? Asynchronous { get; }
		string AttributeScopeQuery { get; }
		bool? CacheResults { get; }
		TimeSpan? ClientTimeout { get; }
		DereferenceAlias? DereferenceAlias { get; }
		DirectorySynchronization DirectorySynchronization { get; }
		ExtendedDN? ExtendedDistinguishedName { get; }
		string Filter { get; }
		int? PageSize { get; }
		IEnumerable<string> PropertiesToLoad { get; }
		bool? PropertyNamesOnly { get; }
		ReferralChasingOption? ReferralChasing { get; }
		SearchScope? SearchScope { get; }
		SecurityMasks? SecurityMasks { get; }
		TimeSpan? ServerPageTimeLimit { get; }
		TimeSpan? ServerTimeLimit { get; }
		int? SizeLimit { get; }
		SortOption Sort { get; }
		bool? Tombstone { get; }
		DirectoryVirtualListView VirtualListView { get; }

		#endregion
	}
}