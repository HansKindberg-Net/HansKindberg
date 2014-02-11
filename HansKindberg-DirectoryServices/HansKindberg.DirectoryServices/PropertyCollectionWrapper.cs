using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace HansKindberg.DirectoryServices
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class PropertyCollectionWrapper : IPropertyCollection
	{
		#region Fields

		private readonly PropertyCollection _propertyCollection;

		#endregion

		#region Constructors

		public PropertyCollectionWrapper(PropertyCollection propertyCollection)
		{
			if(propertyCollection == null)
				throw new ArgumentNullException("propertyCollection");

			this._propertyCollection = propertyCollection;
		}

		#endregion

		#region Properties

		public virtual IPropertyValueCollection this[string propertyName]
		{
			get { return (PropertyValueCollectionWrapper) this._propertyCollection[propertyName]; }
		}

		#endregion

		#region Methods

		public static PropertyCollectionWrapper FromPropertyCollection(PropertyCollection propertyCollection)
		{
			return propertyCollection;
		}

		public virtual IEnumerator<IPropertyValueCollection> GetEnumerator()
		{
			return this.ToList().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		protected internal virtual IList<IPropertyValueCollection> ToList()
		{
			return (from PropertyValueCollection propertyValueCollection in this._propertyCollection select (PropertyValueCollectionWrapper) propertyValueCollection).Cast<IPropertyValueCollection>().ToList();
		}

		#endregion

		#region Implicit operator

		public static implicit operator PropertyCollectionWrapper(PropertyCollection propertyCollection)
		{
			return propertyCollection == null ? null : new PropertyCollectionWrapper(propertyCollection);
		}

		#endregion
	}
}