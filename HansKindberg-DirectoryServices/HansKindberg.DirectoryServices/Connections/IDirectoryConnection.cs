namespace HansKindberg.DirectoryServices.Connections
{
	public interface IDirectoryConnection
	{
		#region Properties

		IDirectoryAuthentication Authentication { get; }
		IDirectoryUri Url { get; }

		#endregion
	}
}