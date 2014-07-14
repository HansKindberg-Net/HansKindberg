using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public abstract class GeneralDirectory
	{
		#region Methods

		protected internal virtual DirectoryEntry GetDirectoryEntry(string path, IDirectoryAuthentication authentication)
		{
			if(authentication == null)
				return new DirectoryEntry(path);

			if(authentication.AuthenticationTypes == null)
				return new DirectoryEntry(path, authentication.UserName, authentication.Password);

			return new DirectoryEntry(path, authentication.UserName, authentication.Password, authentication.AuthenticationTypes.Value);
		}

		#endregion
	}
}