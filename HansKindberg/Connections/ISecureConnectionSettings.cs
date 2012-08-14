namespace HansKindberg.Connections
{
	public interface ISecureConnectionSettings
	{
		#region Properties

		AuthenticationMethod? AuthenticationMethod { get; set; }
		string Password { get; set; }
		string UserName { get; set; }

		#endregion
	}
}