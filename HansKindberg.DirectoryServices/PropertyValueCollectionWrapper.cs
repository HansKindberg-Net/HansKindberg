using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	[SuppressMessage("Microsoft.Design", "CA1035:ICollectionImplementationsHaveStronglyTypedMembers", Justification = "This is a wrapper.")]
	[SuppressMessage("Microsoft.Design", "CA1039:ListsAreStronglyTyped", Justification = "This is a wrapper.")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class PropertyValueCollectionWrapper : IPropertyValueCollection
	{
		#region Fields

		private readonly PropertyValueCollection _propertyValueCollection;

		#endregion

		#region Constructors

		public PropertyValueCollectionWrapper(PropertyValueCollection propertyValueCollection)
		{
			if(propertyValueCollection == null)
				throw new ArgumentNullException("propertyValueCollection");

			this._propertyValueCollection = propertyValueCollection;
		}

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this._propertyValueCollection.Count; }
		}

		public virtual bool IsFixedSize
		{
			get { return ((IList) this._propertyValueCollection).IsFixedSize; }
		}

		bool IList.IsFixedSize
		{
			get { return this.IsFixedSize; }
		}

		public virtual bool IsReadOnly
		{
			get { return ((IList) this._propertyValueCollection).IsReadOnly; }
		}

		bool IList.IsReadOnly
		{
			get { return this.IsReadOnly; }
		}

		public virtual bool IsSynchronized
		{
			get { return ((ICollection) this._propertyValueCollection).IsSynchronized; }
		}

		bool ICollection.IsSynchronized
		{
			get { return this.IsSynchronized; }
		}

		public virtual object this[int index]
		{
			get { return ((IList) this._propertyValueCollection)[index]; }
			set { ((IList) this._propertyValueCollection)[index] = value; }
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { this[index] = value; }
		}

		public virtual string PropertyName
		{
			get { return this._propertyValueCollection.PropertyName; }
		}

		public virtual object SyncRoot
		{
			get { return ((ICollection) this._propertyValueCollection).SyncRoot; }
		}

		object ICollection.SyncRoot
		{
			get { return this.SyncRoot; }
		}

		public virtual object Value
		{
			get { return this._propertyValueCollection.Value; }
			set { this._propertyValueCollection.Value = value; }
		}

		#endregion

		#region Methods

		public virtual int Add(object value)
		{
			return ((IList) this._propertyValueCollection).Add(value);
		}

		int IList.Add(object value)
		{
			return this.Add(value);
		}

		public virtual void Clear()
		{
			this._propertyValueCollection.Clear();
		}

		public virtual bool Contains(object value)
		{
			return ((IList) this._propertyValueCollection).Contains(value);
		}

		bool IList.Contains(object value)
		{
			return this.Contains(value);
		}

		public void CopyTo(Array array, int index)
		{
			((ICollection) this._propertyValueCollection).CopyTo(array, index);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo(array, index);
		}

		public static PropertyValueCollectionWrapper FromPropertyValueCollection(PropertyValueCollection propertyValueCollection)
		{
			return propertyValueCollection;
		}

		public virtual IEnumerator GetEnumerator()
		{
			return this._propertyValueCollection.GetEnumerator();
		}

		public virtual int IndexOf(object value)
		{
			return ((IList) this._propertyValueCollection).IndexOf(value);
		}

		int IList.IndexOf(object value)
		{
			return this.IndexOf(value);
		}

		public virtual void Insert(int index, object value)
		{
			((IList) this._propertyValueCollection).Insert(index, value);
		}

		void IList.Insert(int index, object value)
		{
			this.Insert(index, value);
		}

		public virtual void Remove(object value)
		{
			((IList) this._propertyValueCollection).Remove(value);
		}

		void IList.Remove(object value)
		{
			this.Remove(value);
		}

		public virtual void RemoveAt(int index)
		{
			this._propertyValueCollection.RemoveAt(index);
		}

		#endregion

		#region Implicit operator

		public static implicit operator PropertyValueCollectionWrapper(PropertyValueCollection propertyValueCollection)
		{
			return propertyValueCollection == null ? null : new PropertyValueCollectionWrapper(propertyValueCollection);
		}

		#endregion
	}
}