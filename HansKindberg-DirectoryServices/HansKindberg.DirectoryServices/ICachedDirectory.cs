namespace HansKindberg.DirectoryServices
{
	public interface ICachedDirectory
	{
		#region Properties

		IDirectoryCache Cache { get; }

		#endregion

		#region Methods

		void ClearCache();

		#endregion
	}
}