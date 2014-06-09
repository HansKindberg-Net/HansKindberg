using System.Collections.Generic;

namespace HansKindberg.Collections.Generic
{
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