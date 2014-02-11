using System;
using System.Collections.Generic;

namespace HansKindberg.Collections.Generic.Extensions
{
	public static class DictionaryExtension
	{
		#region Methods

		public static bool TryGetValueAndRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
		{
			if(dictionary == null)
				throw new ArgumentNullException("dictionary");

			bool tryGetValue = dictionary.TryGetValue(key, out value);

			if(tryGetValue)
				dictionary.Remove(key);

			return tryGetValue;
		}

		#endregion
	}
}