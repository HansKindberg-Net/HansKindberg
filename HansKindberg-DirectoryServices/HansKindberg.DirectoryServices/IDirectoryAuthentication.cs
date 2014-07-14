using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryAuthentication
	{
		#region Properties

		AuthenticationTypes? AuthenticationTypes { get; set; }
		string Password { get; set; }
		string UserName { get; set; }

		#endregion
	}
}