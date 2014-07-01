namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryUri
	{
		#region Properties

		IDistinguishedName DistinguishedName { get; set; }
		string Host { get; set; }
		int? Port { get; set; }
		Scheme Scheme { get; set; }

		#endregion
	}
}