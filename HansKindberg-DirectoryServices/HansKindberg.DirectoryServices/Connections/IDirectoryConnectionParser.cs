namespace HansKindberg.DirectoryServices.Connections
{
	public interface IDirectoryConnectionParser
	{
		#region Methods

		IDirectoryConnection Parse(string connectionString);

		#endregion
	}
}