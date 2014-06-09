using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HansKindberg.Abstractions;

namespace HansKindberg.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a wrapper.")]
	public class DictionaryWrapper<TKey, TValue> : Wrapper<IDictionary>, IReadOnlyDictionary<TKey, TValue>
	{
		#region Constructors

		public DictionaryWrapper(IDictionary dictionary) : base(dictionary, "dictionary") {}

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this.WrappedInstance.Count; }
		}

		public virtual TValue this[TKey key]
		{
			get { return (TValue) this.WrappedInstance[key]; }
		}

		public virtual IEnumerable<TKey> Keys
		{
			get { return this.WrappedInstance.Keys.Cast<TKey>(); }
		}

		public virtual IEnumerable<TValue> Values
		{
			get { return this.WrappedInstance.Values.Cast<TValue>(); }
		}

		#endregion

		#region Methods

		public virtual bool ContainsKey(TKey key)
		{
			return this.WrappedInstance.Contains(key);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.WrappedInstance.Cast<DictionaryEntry>().ToDictionary(keyValuePair => (TKey) keyValuePair.Key, keyValuePair => (TValue) keyValuePair.Value).GetEnumerator();
		}

		public virtual bool TryGetValue(TKey key, out TValue value)
		{
			if(!this.ContainsKey(key))
			{
				value = default(TValue);
				return false;
			}

			value = this[key];
			return true;
		}

		#endregion
	}
}