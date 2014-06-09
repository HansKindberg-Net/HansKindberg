using System;
using System.Collections;
using System.Collections.Generic;
using HansKindberg.Abstractions;

namespace HansKindberg.Collections.Generic
{
	public class EnumeratorWrapper<T> : Wrapper<IEnumerator>, IEnumerator<T>
	{
		#region Fields

		private readonly Func<object, T> _elementWrapper;

		#endregion

		#region Constructors

		public EnumeratorWrapper(IEnumerator enumerator, Func<object, T> elementWrapper) : base(enumerator, "enumerator")
		{
			if(elementWrapper == null)
				throw new ArgumentNullException("elementWrapper");

			this._elementWrapper = elementWrapper;
		}

		#endregion

		#region Properties

		public virtual T Current
		{
			get { return this.ElementWrapper(this.WrappedInstance.Current); }
		}

		object IEnumerator.Current
		{
			get { return this.Current; }
		}

		public virtual Func<object, T> ElementWrapper
		{
			get { return this._elementWrapper; }
		}

		#endregion

		#region Methods

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(!disposing)
				return;

			var disposable = this.WrappedInstance as IDisposable;

			if(disposable != null)
				disposable.Dispose();
		}

		public virtual bool MoveNext()
		{
			return this.WrappedInstance.MoveNext();
		}

		public virtual void Reset()
		{
			this.WrappedInstance.Reset();
		}

		#endregion
	}
}