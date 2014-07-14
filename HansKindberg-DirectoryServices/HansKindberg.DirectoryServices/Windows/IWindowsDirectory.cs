using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices.Windows
{
	public interface IWindowsDirectory
	{
		#region Properties

		IWindowsDirectoryItem Root { get; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IWindowsDirectoryItem Get(string localPath);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IWindowsDirectoryItem Get(IList<string> localPath);

		IEnumerable<IWindowsDirectoryItem> GetChildren();
		IEnumerable<IWindowsDirectoryItem> GetChildren(string localPath);
		IEnumerable<IWindowsDirectoryItem> GetChildren(IList<string> localPath);

		#endregion
	}
}