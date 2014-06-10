using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Collections.Generic
{
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a wrapper.")]
	public class DisposableEnumerableWrapper<T, TElement> : DisposableWrapper<T>, IDisposableEnumerable<TElement> where T : IDisposable, IEnumerable
	{
		#region Fields

		private readonly EnumerableWrapper<TElement> _enumerableWrapper;

		#endregion

		#region Constructors

		public DisposableEnumerableWrapper(T disposableEnumerable, Func<object, TElement> elementWrapper) : this(disposableEnumerable, "disposableEnumerable", elementWrapper) {}

		protected DisposableEnumerableWrapper(T disposableEnumerable, string disposableEnumerableParameterName, Func<object, TElement> elementWrapper) : base(disposableEnumerable, disposableEnumerableParameterName)
		{
			this._enumerableWrapper = new EnumerableWrapper<TElement>(disposableEnumerable, elementWrapper);
		}

		#endregion

		#region Properties

		protected internal virtual EnumerableWrapper<TElement> EnumerableWrapper
		{
			get { return this._enumerableWrapper; }
		}

		#endregion

		#region Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual IEnumerator<TElement> GetEnumerator()
		{
			return this.EnumerableWrapper.GetEnumerator();
		}

		#endregion
	}
}