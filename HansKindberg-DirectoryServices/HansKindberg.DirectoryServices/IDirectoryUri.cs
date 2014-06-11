namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryUri
	{
		#region Properties

		string DistinguishedName { get; set; }
		string Host { get; set; }
		int? Port { get; set; }
		Scheme Scheme { get; set; }

		#endregion

		#region Methods

		string ToString();

		#endregion
	}
}