﻿using System;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryEntry : IDisposable
	{
		#region Properties

		IDirectoryEntries Children { get; }
		IDirectoryEntry Parent { get; }
		string Path { get; set; }
		IPropertyCollection Properties { get; }

		#endregion
	}
}