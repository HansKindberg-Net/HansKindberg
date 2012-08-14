namespace HansKindberg.Connections
{
	public interface ISecureConnectionSettings
	{
		#region Properties

		AuthenticationMethod? AuthenticationMethod { get; }
		string Password { get; }
		string UserName { get; }

		#endregion
	}
}