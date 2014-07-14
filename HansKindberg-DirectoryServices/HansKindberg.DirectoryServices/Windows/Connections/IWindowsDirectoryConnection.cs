namespace HansKindberg.DirectoryServices.Windows.Connections
{
	public interface IWindowsDirectoryConnection
	{
		#region Properties

		IDirectoryAuthentication Authentication { get; }
		IWindowsDirectoryUri Url { get; }

		#endregion
	}
}