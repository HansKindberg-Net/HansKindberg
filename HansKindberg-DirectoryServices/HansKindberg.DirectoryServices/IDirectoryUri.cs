namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryUri {
		string DistinguishedName { get; set; }
		string Host { get; set; }
		int? Port { get; set; }
		Scheme Scheme { get; set; }
		string ToString();
	}
}