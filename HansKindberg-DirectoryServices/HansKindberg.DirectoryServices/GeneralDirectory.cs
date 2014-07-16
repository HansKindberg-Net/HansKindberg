using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace HansKindberg.DirectoryServices
{
	public abstract class GeneralDirectory
	{
		#region Methods

		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "name")]
		public virtual bool Exists(string path, IDirectoryAuthentication authentication)
		{
			using(var directoryEntry = this.GetDirectoryEntry(path, authentication))
			{
				try
				{
					var name = directoryEntry.Name;
					return true;
				}
				catch(DirectoryServicesCOMException directoryServicesComException)
				{
					if(directoryServicesComException.ExtendedError == 10 || directoryServicesComException.ExtendedError == 3564) // Invalid credentials (10) or "Decoding LDAP credentials failed" (3564)
						throw new DirectoryServicesException(directoryServicesComException);

					return false;
				}
				catch(COMException)
				{
					return false;
				}
			}
		}

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