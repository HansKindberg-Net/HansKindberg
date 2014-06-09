using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Collections
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public interface IDisposableEnumerable : IDisposable, IEnumerable {}
}