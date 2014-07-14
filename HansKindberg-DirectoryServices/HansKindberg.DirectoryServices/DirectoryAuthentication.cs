using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public class DirectoryAuthentication : IDirectoryAuthentication
	{
		#region Properties

		public virtual AuthenticationTypes? AuthenticationTypes { get; set; }
		public virtual string Password { get; set; }
		public virtual string UserName { get; set; }

		#endregion
	}
}