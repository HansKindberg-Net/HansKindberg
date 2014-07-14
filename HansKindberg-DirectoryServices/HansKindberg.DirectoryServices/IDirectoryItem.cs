namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryItem : IGeneralDirectoryItem
	{
		#region Properties

		IDirectoryUri Url { get; }

		#endregion
	}
}