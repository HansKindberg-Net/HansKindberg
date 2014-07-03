using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface IGlobalDirectory
	{
		#region Methods

		IEnumerable<IDirectoryItem> Find(string searchRootPath, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path, string userName, string password);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url, string userName, string password);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path, string userName, string password, AuthenticationTypes authenticationTypes);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url, string userName, string password, AuthenticationTypes authenticationTypes);

		#endregion
	}
}