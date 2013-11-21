using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectorySearcherOptions
	{
		#region Properties

		bool? Asynchronous { get; }
		LazyInitialization<string> AttributeScopeQuery { get; }
		bool? CacheResults { get; }
		TimeSpan? ClientTimeout { get; }
		DereferenceAlias? DereferenceAlias { get; }
		LazyInitialization<DirectorySynchronization> DirectorySynchronization { get; }
		ExtendedDN? ExtendedDistinguishedName { get; }
		LazyInitialization<string> Filter { get; }
		int? PageSize { get; }

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		LazyInitialization<IEnumerable<string>> PropertiesToLoad { get; }

		bool? PropertyNamesOnly { get; }
		ReferralChasingOption? ReferralChasing { get; }
		SearchScope? SearchScope { get; }
		SecurityMasks? SecurityMasks { get; }
		TimeSpan? ServerPageTimeLimit { get; }
		TimeSpan? ServerTimeLimit { get; }
		int? SizeLimit { get; }
		LazyInitialization<SortOption> Sort { get; }
		bool? Tombstone { get; }
		LazyInitialization<DirectoryVirtualListView> VirtualListView { get; }

		#endregion
	}
}