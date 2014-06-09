using System;
using HansKindberg.Abstractions;

namespace HansKindberg
{
	public class DisposableWrapper<T> : Wrapper<T>, IDisposable where T : IDisposable
	{
		#region Constructors

		public DisposableWrapper(T disposable) : this(disposable, "disposable") {}
		protected DisposableWrapper(T disposable, string disposableParameterName) : base(disposable, disposableParameterName) {}

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

			this.WrappedInstance.Dispose();
		}

		#endregion
	}
}