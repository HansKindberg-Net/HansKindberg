using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryNode
	{
		#region Properties

		Guid? Guid { get; }
		string Name { get; }
		string NativeGuid { get; }
		IDirectoryUri ParentPath { get; }
		IDirectoryUri Path { get; }

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IDictionary<string, IEnumerable<object>> Properties { get; }

		string SchemaClassName { get; }

		#endregion
	}
}