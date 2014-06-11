using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Properties

		IDirectoryNode Root { get; }

		#endregion

		#region Methods

		IDirectoryNode Create(IDirectoryNode parent, string name, string schemaClassName);
		bool Delete(string distinguishedName);
		//IEnumerable<IReadOnlyDirectoryNode> Find();
		//IEnumerable<IReadOnlyDirectoryNode> Find(string filter);
		//IEnumerable<IReadOnlyDirectoryNode> Find(IEnumerable<string> propertiesToLoad);
		//IEnumerable<IReadOnlyDirectoryNode> Find(ISearchOptions searchOptions);
		//IEnumerable<IReadOnlyDirectoryNode> Find(string filter, ISearchOptions searchOptions);
		//IEnumerable<IReadOnlyDirectoryNode> Find(string filter, IEnumerable<string> propertiesToLoad);
		//IEnumerable<IReadOnlyDirectoryNode> Find(string filter, IEnumerable<string> propertiesToLoad, ISearchOptions searchOptions);
		//IEnumerable<IReadOnlyDirectoryNode> Find(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope);
		IEnumerable<IReadOnlyDirectoryNode> Find(string filter, IEnumerable<string> propertiesToLoad, SearchScope? scope, ISearchOptions searchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryNode Get(string distinguishedName);

		IEnumerable<IDirectoryNode> GetChildren(string distinguishedName);
		object Invoke(IDirectoryNode directoryNode, string methodName, params object[] arguments);
		void Move(IDirectoryNode directoryNode, IDirectoryNode destination);
		void Rename(IDirectoryNode directoryNode, string name);
		void Save(IDirectoryNode directoryNode);

		#endregion
	}
}