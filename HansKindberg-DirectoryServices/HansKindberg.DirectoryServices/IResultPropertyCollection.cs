using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public interface IResultPropertyCollection
	{
		#region Properties

		ICollection<object> this[string name] { get; }
		ICollection<string> PropertyNames { get; }
		ICollection<object> Values { get; }

		#endregion
	}
}