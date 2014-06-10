using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Abstractions;

namespace HansKindberg.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a wrapper.")]
	public class DictionaryWrapper<TKey, TValue> : Wrapper<IDictionary<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>
	{
		#region Constructors

		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary) : this(dictionary, "dictionary") {}
		protected DictionaryWrapper(IDictionary<TKey, TValue> dictionary, string dictionaryParameterName) : base(dictionary, dictionaryParameterName) {}

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this.WrappedInstance.Count; }
		}

		public virtual TValue this[TKey key]
		{
			get { return this.WrappedInstance[key]; }
		}

		public virtual IEnumerable<TKey> Keys
		{
			get { return this.WrappedInstance.Keys; }
		}

		public virtual IEnumerable<TValue> Values
		{
			get { return this.WrappedInstance.Values; }
		}

		#endregion

		#region Methods

		public virtual bool ContainsKey(TKey key)
		{
			return this.WrappedInstance.ContainsKey(key);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.WrappedInstance.GetEnumerator();
		}

		public virtual bool TryGetValue(TKey key, out TValue value)
		{
			return this.WrappedInstance.TryGetValue(key, out value);
		}

		#endregion
	}
}