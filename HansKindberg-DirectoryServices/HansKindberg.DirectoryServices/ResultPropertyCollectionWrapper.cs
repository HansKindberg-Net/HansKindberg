using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public class ResultPropertyCollectionWrapper : IResultPropertyCollection
	{
		#region Fields

		private readonly ResultPropertyCollection _resultPropertyCollection;

		#endregion

		#region Constructors

		public ResultPropertyCollectionWrapper(ResultPropertyCollection resultPropertyCollection)
		{
			if(resultPropertyCollection == null)
				throw new ArgumentNullException("resultPropertyCollection");

			this._resultPropertyCollection = resultPropertyCollection;
		}

		#endregion

		#region Properties

		public virtual ICollection<object> this[string name]
		{
			get
			{
				List<object> valueList = new List<object>();

				foreach(object value in this._resultPropertyCollection[name])
				{
					valueList.Add(value);
				}

				return valueList;
			}
		}

		public virtual ICollection<string> PropertyNames
		{
			get
			{
				List<string> propertyNameList = new List<string>();

				foreach(string propertyName in this._resultPropertyCollection.PropertyNames ?? new string[0])
				{
					propertyNameList.Add(propertyName);
				}

				return propertyNameList;
			}
		}

		public virtual ICollection<object> Values
		{
			get
			{
				List<object> valueList = new List<object>();

				foreach(object value in this._resultPropertyCollection.Values ?? new object[0])
				{
					valueList.Add(value);
				}

				return valueList;
			}
		}

		#endregion

		#region Methods

		public static ResultPropertyCollectionWrapper FromResultPropertyCollection(ResultPropertyCollection resultPropertyCollection)
		{
			return resultPropertyCollection;
		}

		#endregion

		#region Implicit operator

		public static implicit operator ResultPropertyCollectionWrapper(ResultPropertyCollection resultPropertyCollection)
		{
			return resultPropertyCollection == null ? null : new ResultPropertyCollectionWrapper(resultPropertyCollection);
		}

		#endregion
	}
}