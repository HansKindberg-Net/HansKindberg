using System.DirectoryServices;
using HansKindberg.Connections;

namespace HansKindberg.DirectoryServices.Connections
{
	public interface ILdapConnectionSettings : ISecureConnectionSettings
	{
		#region Properties

		AuthenticationTypes? AuthenticationTypes { get; }
		string Path { get; }

		#endregion
	}
}