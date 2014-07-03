using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryItem
	{
		#region Properties

		IDictionary<string, object> Properties { get; }
		IDirectoryUri Url { get; }

		#endregion
	}
}