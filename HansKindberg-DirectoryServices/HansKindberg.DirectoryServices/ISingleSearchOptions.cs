namespace HansKindberg.DirectoryServices
{
	public interface ISingleSearchOptions : IGeneralSearchOptions
	{
		#region Methods

		ISingleSearchOptions Copy();
		ISingleSearchOptions Copy(ISingleSearchOptions singleSearchOptionsToOverrideWith);

		#endregion
	}
}