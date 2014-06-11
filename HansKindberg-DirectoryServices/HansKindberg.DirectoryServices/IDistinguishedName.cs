using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public interface IDistinguishedName : IEquatable<IDistinguishedName>
	{
		#region Properties

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IList<KeyValuePair<string, string>> Components { get; }

		#endregion
	}
}