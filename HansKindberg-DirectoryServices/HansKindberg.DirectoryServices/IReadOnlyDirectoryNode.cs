using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public interface IReadOnlyDirectoryNode
	{
		#region Properties

		IDirectoryUri Path { get; }

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IReadOnlyDictionary<string, IEnumerable<object>> Properties { get; }

		#endregion
	}
}