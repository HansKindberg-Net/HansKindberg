using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface ISearchResult
	{
		#region Properties

		string Path { get; }
		IResultPropertyCollection Properties { get; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		IDirectoryEntry GetDirectoryEntry();

		#endregion
	}
}