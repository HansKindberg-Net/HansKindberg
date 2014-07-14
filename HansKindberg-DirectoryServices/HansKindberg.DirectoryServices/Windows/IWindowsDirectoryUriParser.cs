namespace HansKindberg.DirectoryServices.Windows
{
	public interface IWindowsDirectoryUriParser
	{
		#region Methods

		IWindowsDirectoryUri Parse(string value);
		bool TryParse(string value, out IWindowsDirectoryUri windowsDirectoryUri);

		#endregion
	}
}