namespace HansKindberg.DirectoryServices
{
	public interface IDirectory
	{
		#region Properties

		IDirectoryEntry Root { get; }

		#endregion
	}
}