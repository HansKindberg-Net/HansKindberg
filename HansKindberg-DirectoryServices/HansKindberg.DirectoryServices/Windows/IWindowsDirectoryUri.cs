using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.Windows
{
	public interface IWindowsDirectoryUri
	{
		#region Properties

		string Host { get; set; }
		IList<string> LocalPath { get; }
		int? Port { get; set; }
		WindowsScheme Scheme { get; set; }

		#endregion
	}
}