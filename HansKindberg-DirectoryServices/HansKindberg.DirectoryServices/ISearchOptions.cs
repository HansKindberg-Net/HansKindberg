using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface ISearchOptions : IGeneralSearchOptions
	{
		#region Properties

		int? PageSize { get; }
		SearchScope? SearchScope { get; }

		#endregion

		#region Methods

		ISearchOptions Copy();
		ISearchOptions Copy(ISearchOptions searchOptionsToOverrideWith);

		#endregion
	}
}