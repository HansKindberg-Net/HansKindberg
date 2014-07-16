using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Properties

		IDirectoryItem Root { get; }
		ISearchOptions SearchOptions { get; }
		ISingleSearchOptions SingleSearchOptions { get; }

		#endregion

		#region Methods

		bool Exists(string distinguishedName);
		bool Exists(IDistinguishedName distinguishedName);
		IEnumerable<IDirectoryItem> Find();
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName);
		IEnumerable<IDirectoryItem> Find(ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(string searchRootDistinguishedName, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(IDistinguishedName searchRootDistinguishedName, ISearchOptions searchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string distinguishedName);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDistinguishedName distinguishedName);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string distinguishedName, ISingleSearchOptions singleSearchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDistinguishedName distinguishedName, ISingleSearchOptions singleSearchOptions);

		#endregion
	}
}