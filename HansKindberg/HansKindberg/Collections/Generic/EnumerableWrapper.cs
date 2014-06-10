using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Abstractions;

namespace HansKindberg.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a wrapper.")]
	public class EnumerableWrapper<T> : Wrapper<IEnumerable>, IEnumerable<T>
	{
		#region Fields

		private readonly Func<object, T> _elementWrapper;

		#endregion

		#region Constructors

		public EnumerableWrapper(IEnumerable enumerable, Func<object, T> elementWrapper) : base(enumerable, "enumerable")
		{
			if(elementWrapper == null)
				throw new ArgumentNullException("elementWrapper");

			this._elementWrapper = elementWrapper;
		}

		#endregion

		#region Properties

		protected internal virtual Func<object, T> ElementWrapper
		{
			get { return this._elementWrapper; }
		}

		#endregion

		#region Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return new EnumeratorWrapper<T>(this.WrappedInstance.GetEnumerator(), this.ElementWrapper);
		}

		#endregion
	}
}