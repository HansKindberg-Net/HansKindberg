using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Methods

		IEnumerable<IDirectoryNode> Find();
		IEnumerable<IDirectoryNode> Find(string filter);
		IEnumerable<IDirectoryNode> Find(ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(string filter, ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad);
		IEnumerable<IDirectoryNode> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad);
		IEnumerable<IDirectoryNode> Find(string searchRootDistinguishedName, string filter, SearchScope? searchScope);
		IEnumerable<IDirectoryNode> Find(IDistinguishedName searchRootDistinguishedName, string filter, SearchScope? searchScope);
		IEnumerable<IDirectoryNode> Find(string searchRootDistinguishedName, string filter, ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(IDistinguishedName searchRootDistinguishedName, string filter, ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope);
		IEnumerable<IDirectoryNode> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope);
		IEnumerable<IDirectoryNode> Find(string searchRootDistinguishedName, string filter, SearchScope? searchScope, ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(IDistinguishedName searchRootDistinguishedName, string filter, SearchScope? searchScope, ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryNode Get(string distinguishedName);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryNode Get(IDistinguishedName distinguishedName);

		#endregion
	}
}