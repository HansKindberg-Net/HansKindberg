namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryUriParser
	{
		#region Methods

		IDirectoryUri Parse(string value);
		bool TryParse(string value, out IDirectoryUri directoryUri);

		#endregion
	}
}