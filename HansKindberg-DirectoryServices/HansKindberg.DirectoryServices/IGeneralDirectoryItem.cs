using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public interface IGeneralDirectoryItem
	{
		#region Properties

		string Path { get; }
		IDictionary<string, object> Properties { get; }

		#endregion
	}
}