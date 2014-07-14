using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices.Windows
{
	public interface IGlobalWindowsDirectory
	{
		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IWindowsDirectoryItem Get(string path);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IWindowsDirectoryItem Get(IWindowsDirectoryUri url);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IWindowsDirectoryItem Get(string path, IDirectoryAuthentication authentication);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IWindowsDirectoryItem Get(IWindowsDirectoryUri url, IDirectoryAuthentication authentication);

		IEnumerable<IWindowsDirectoryItem> GetChildren(string path);
		IEnumerable<IWindowsDirectoryItem> GetChildren(IWindowsDirectoryUri url);
		IEnumerable<IWindowsDirectoryItem> GetChildren(string path, IDirectoryAuthentication authentication);
		IEnumerable<IWindowsDirectoryItem> GetChildren(IWindowsDirectoryUri url, IDirectoryAuthentication authentication);

		#endregion
	}
}