namespace HansKindberg.Connections
{
	public interface ISecureConnectionSettings
	{
		#region Properties

		string Password { get; }
		string UserName { get; }

		#endregion
	}
}