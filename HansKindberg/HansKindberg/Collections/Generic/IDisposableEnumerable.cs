using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public interface IDisposableEnumerable<T> : IDisposableEnumerable, IEnumerable<T> {}
}