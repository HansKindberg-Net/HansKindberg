namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryCacheFactory
	{
		#region Methods

		IDirectoryCache Create();
		IDirectoryCache Create(string keyPrefix);

		#endregion
	}
}