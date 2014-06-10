using System.DirectoryServices;

namespace HansKindberg.DirectoryServices.Connections
{
	public interface IDirectoryConnection
	{
		#region Properties

		AuthenticationTypes? AuthenticationTypes { get; }
		string DistinguishedName { get; }
		string Host { get; }
		string Password { get; }
		int? Port { get; }
		Scheme? Scheme { get; }
		string UserName { get; }

		#endregion
	}
}