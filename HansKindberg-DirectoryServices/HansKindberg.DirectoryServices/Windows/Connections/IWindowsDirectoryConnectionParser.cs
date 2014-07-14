namespace HansKindberg.DirectoryServices.Windows.Connections
{
	public interface IWindowsDirectoryConnectionParser
	{
		#region Methods

		IWindowsDirectoryConnection Parse(string connectionString);

		#endregion
	}
}