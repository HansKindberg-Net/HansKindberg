using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Methods

		IEnumerable<IDirectoryNode> Find();
		IEnumerable<IDirectoryNode> Find(string filter);
		IEnumerable<IDirectoryNode> Find(ISearchOptions searchOptions);
		IEnumerable<IDirectoryNode> Find(string filter, ISearchOptions searchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryNode Get(string distinguishedName);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryNode Get(IDistinguishedName distinguishedName);

		#endregion
	}
}