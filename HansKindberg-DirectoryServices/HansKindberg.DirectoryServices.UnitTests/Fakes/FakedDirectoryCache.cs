using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.UnitTests.Fakes
{
	public class FakedDirectoryCache : IDirectoryCache
	{
		#region Fields

		private readonly IDictionary<string, object> _items = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		private readonly string _keyPrefix;

		#endregion

		#region Constructors

		public FakedDirectoryCache(string keyPrefix)
		{
			this._keyPrefix = keyPrefix;
		}

		#endregion

		#region Properties

		public virtual IDictionary<string, object> Items
		{
			get { return this._items; }
		}

		public virtual string KeyPrefix
		{
			get { return this._keyPrefix; }
		}

		#endregion

		#region Methods

		public virtual void Clear()
		{
			this.Items.Clear();
		}

		public virtual object Get(string key)
		{
			if(this.Items.ContainsKey(key))
				return this.Items[key];

			return null;
		}

		public virtual bool Remove(string key)
		{
			if(this.Items.ContainsKey(key))
			{
				this.Items.Remove(key);
				return true;
			}

			return false;
		}

		public virtual void Set(string key, object value)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(this.Items.ContainsKey(key))
				this.Items[key] = value;

			this.Items.Add(key, value);
		}

		#endregion
	}
}