using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Properties

		IDirectoryItem Root { get; }

		#endregion

		#region Methods

		IEnumerable<IDirectoryItem> Find();
		IEnumerable<IDirectoryItem> Find(string filter);
		IEnumerable<IDirectoryItem> Find(ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(string filter, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad);
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, SearchScope? searchScope);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, SearchScope? searchScope);
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope);
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, SearchScope? searchScope, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, SearchScope? searchScope, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, string filter, IEnumerable<string> propertiesToLoad, SearchScope? searchScope, ISearchOptions searchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string distinguishedName);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDistinguishedName distinguishedName);

		#endregion
	}
}