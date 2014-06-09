using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests
{
	[TestClass]
	public class DisposableWrapperTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithOneParameter_IfTheDisposableParameterIsNotNull_ShouldSetTheWrappedInstance()
		{
			var disposable = Mock.Of<IDisposable>();

			using(var disposableWrapper = new DisposableWrapper<IDisposable>(disposable))
			{
				Assert.IsTrue(ReferenceEquals(disposable, disposableWrapper.WrappedInstance));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.DisposableWrapper`1<System.IDisposable>")]
		public void Constructor_WithOneParameter_IfTheDisposableParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new DisposableWrapper<IDisposable>(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "disposable")
					throw;
			}
		}

		[TestMethod]
		public void Constructor_WithTwoParameters_IfTheDisposableParameterIsNotNull_ShouldSetTheWrappedInstance()
		{
			var disposable = Mock.Of<IDisposable>();

			using(var disposableWrapper = new DisposableWrapperMock<IDisposable>(disposable, "Test"))
			{
				Assert.IsTrue(ReferenceEquals(disposable, disposableWrapper.WrappedInstance));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.UnitTests.Mocks.DisposableWrapperMock`1<System.IDisposable>")]
		public void Constructor_WithTwoParameters_IfTheDisposableParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new DisposableWrapperMock<IDisposable>(null, "Test");
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "Test")
					throw;
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Dispose_ShouldCallDisposeWithOneParameterAndTheDisposingParameterSetToTrue()
		{
			var disposableWrapperMock = new DisposableWrapperMock<IDisposable>(Mock.Of<IDisposable>());

			Assert.IsFalse(disposableWrapperMock.Disposing);

			disposableWrapperMock.Dispose();

			Assert.IsTrue(disposableWrapperMock.Disposing);
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Dispose_WithOneParameter_IfTheDisposingParameterIsSetToFalse_ShouldNotCallDisposeOnTheWrappedInstance()
		{
			var disposeIsCalled = false;
			var disposableMock = new Mock<IDisposable>();
			disposableMock.Setup(disposable => disposable.Dispose()).Callback(delegate { disposeIsCalled = true; });

			var disposableWrapperMock = new DisposableWrapperMock<IDisposable>(disposableMock.Object);

			Assert.IsFalse(disposeIsCalled);

			disposableWrapperMock.DisposePublic(false);

			Assert.IsFalse(disposeIsCalled);
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Dispose_WithOneParameter_IfTheDisposingParameterIsSetToTrue_ShouldCallDisposeOnTheWrappedInstance()
		{
			var disposeIsCalled = false;
			var disposableMock = new Mock<IDisposable>();
			disposableMock.Setup(disposable => disposable.Dispose()).Callback(delegate { disposeIsCalled = true; });

			var disposableWrapperMock = new DisposableWrapperMock<IDisposable>(disposableMock.Object);

			Assert.IsFalse(disposeIsCalled);

			disposableWrapperMock.DisposePublic(true);

			Assert.IsTrue(disposeIsCalled);
		}

		#endregion
	}
}