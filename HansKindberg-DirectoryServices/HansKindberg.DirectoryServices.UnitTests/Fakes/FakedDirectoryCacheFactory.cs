namespace HansKindberg.DirectoryServices.UnitTests.Fakes
{
	public class FakedDirectoryCacheFactory : IDirectoryCacheFactory
	{
		#region Methods

		public virtual IDirectoryCache Create()
		{
			return new FakedDirectoryCache(null);
		}

		public virtual IDirectoryCache Create(string keyPrefix)
		{
			return new FakedDirectoryCache(keyPrefix);
		}

		#endregion
	}
}