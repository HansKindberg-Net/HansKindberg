namespace HansKindberg.DirectoryServices.Windows
{
	public interface IWindowsDirectoryItem : IGeneralDirectoryItem
	{
		#region Properties

		IWindowsDirectoryUri Url { get; }

		#endregion
	}
}