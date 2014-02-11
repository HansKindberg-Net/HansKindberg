using System.DirectoryServices;
using HansKindberg.Connections;

namespace HansKindberg.DirectoryServices.Connections
{
	public interface IConnectionSettings : ISecureConnectionSettings
	{
		#region Properties

		AuthenticationTypes? AuthenticationTypes { get; }
		string DistinguishedName { get; }
		string Host { get; }
		int? Port { get; }
		Scheme Scheme { get; }

		#endregion
	}
}