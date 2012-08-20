using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Properties

		string Path { get; }

		#endregion

		#region Methods

		IEnumerable<ISearchResult> FindAll();
		IEnumerable<ISearchResult> FindAll(IDirectorySearcherOptions directorySearcherOptions);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot);
		IEnumerable<ISearchResult> FindAll(string filter);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, IDirectorySearcherOptions directorySearcherOptions);
		IEnumerable<ISearchResult> FindAll(string filter, IDirectorySearcherOptions directorySearcherOptions);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter);
		IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IDirectorySearcherOptions directorySearcherOptions);
		IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad);
		IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions);
		IEnumerable<ISearchResult> FindAll(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope);
		IEnumerable<ISearchResult> FindAll(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne();
		ISearchResult FindOne(IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne(IDirectoryEntry searchRoot);
		ISearchResult FindOne(string filter);
		ISearchResult FindOne(IDirectoryEntry searchRoot, IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne(string filter, IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne(IDirectoryEntry searchRoot, string filter);
		ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad);
		ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad);
		ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope);
		ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne(string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions);
		ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope);
		ISearchResult FindOne(IDirectoryEntry searchRoot, string filter, IEnumerable<string> propertiesToLoad, SearchScope scope, IDirectorySearcherOptions directorySearcherOptions);
		IDirectoryEntry GetDirectoryEntry(string path);

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		IDirectoryEntry GetRoot();

		#endregion
	}
}