using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		#region Properties

		TValue this[TKey key] { get; }
		IEnumerable<TKey> Keys { get; }
		IEnumerable<TValue> Values { get; }

		#endregion

		#region Methods

		bool ContainsKey(TKey key);
		bool TryGetValue(TKey key, out TValue value);

		#endregion
	}
}