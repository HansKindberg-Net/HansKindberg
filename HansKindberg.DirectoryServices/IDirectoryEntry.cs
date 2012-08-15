using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryEntry : IDisposable
	{
		#region Properties

		IDirectoryEntries Children { get; }
		IDirectoryEntry Parent { get; }
		IPropertyCollection Properties { get; }

		#endregion
	}
}