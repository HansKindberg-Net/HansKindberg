using System;
using System.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.ShimTests
{
	[TestClass]
	public class DisposableWrapperTest
	{
		#region Methods

		[TestMethod]
		public void Dispose_ShouldCallGarbageCollectorSuppressFinalize()
		{
			using(ShimsContext.Create())
			{
				object suppressFinalizeInstance = null;
				bool suppressFinalizeIsCalled = false;

				ShimGC.SuppressFinalizeObject = delegate(object instance)
				{
					suppressFinalizeInstance = instance;
					suppressFinalizeIsCalled = true;
				};

				Assert.IsNull(suppressFinalizeInstance);
				Assert.IsFalse(suppressFinalizeIsCalled);

				var disposableWrapper = new DisposableWrapper<IDisposable>(Mock.Of<IDisposable>());

				disposableWrapper.Dispose();

				Assert.IsNotNull(suppressFinalizeInstance);
				Assert.IsTrue(ReferenceEquals(disposableWrapper, suppressFinalizeInstance));
				Assert.IsTrue(suppressFinalizeIsCalled);
			}
		}

		#endregion
	}
}