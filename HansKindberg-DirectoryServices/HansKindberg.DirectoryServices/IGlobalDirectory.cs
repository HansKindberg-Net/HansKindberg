using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface IGlobalDirectory
	{
		#region Methods

		IEnumerable<IDirectoryItem> Find(string searchRootPath);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl);
		IEnumerable<IDirectoryItem> Find(string searchRootPath, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(string searchRootPath, IDirectoryAuthentication authentication);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, IDirectoryAuthentication authentication);
		IEnumerable<IDirectoryItem> Find(string searchRootPath, ISearchOptions searchOptions, IDirectoryAuthentication authentication);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, ISearchOptions searchOptions, IDirectoryAuthentication authentication);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path, IDirectoryAuthentication authentication);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url, IDirectoryAuthentication authentication);

		#endregion
	}
}