using System;

namespace HansKindberg.UnitTests.Mocks
{
	public class DisposableWrapperMock<T> : DisposableWrapper<T> where T : IDisposable
	{
		#region Constructors

		public DisposableWrapperMock(T disposable) : base(disposable) {}
		public DisposableWrapperMock(T disposable, string disposableParameterName) : base(disposable, disposableParameterName) {}

		#endregion

		#region Properties

		public virtual bool Disposing { get; set; }

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			this.Disposing = disposing;

			base.Dispose(disposing);
		}

		public virtual void DisposePublic(bool disposing)
		{
			this.Dispose(disposing);
		}

		#endregion
	}
}