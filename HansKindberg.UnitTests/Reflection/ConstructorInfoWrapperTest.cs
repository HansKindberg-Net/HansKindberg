using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HansKindberg.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.Reflection
{
	[TestClass]
	public class ConstructorInfoWrapperTest
	{
		#region Methods

		[TestMethod]
		public void ConstructorInfoIsNotAConstructorInfoWrapper()
		{
			ConstructorInfo constructorInfo = CreateConstructorInfo();
			Assert.IsNotNull(constructorInfo);
#pragma warning disable 184
			Assert.IsFalse(constructorInfo is ConstructorInfoWrapper);
#pragma warning restore 184
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Reflection.ConstructorInfoWrapper")]
		public void Constructor_ShouldThrowAnArgumentNullException_IfTheConstructorInfoParameterIsNull()
		{
			new ConstructorInfoWrapper(null);
		}

		private static ConstructorInfo CreateConstructorInfo()
		{
			return Mock.Of<ConstructorInfo>();
		}

		[TestMethod]
		public void FromConstructorInfo_ShouldNotReturnNull_IfTheConstructorInfoIsNotNull()
		{
			Assert.IsNotNull(ConstructorInfoWrapper.FromConstructorInfo(CreateConstructorInfo()));
		}

		[TestMethod]
		public void FromConstructorInfo_ShouldReturnNull_IfTheConstructorInfoIsNull()
		{
			Assert.IsNull(ConstructorInfoWrapper.FromConstructorInfo(null));
		}

		[TestMethod]
		public void ImplicitOperator_ShouldNotReturnNull_IfTheConstructorInfoIsNotNull()
		{
			Assert.IsNotNull((ConstructorInfoWrapper) CreateConstructorInfo());
		}

		[TestMethod]
		public void ImplicitOperator_ShouldReturnNull_IfTheConstructorInfoIsNull()
		{
			Assert.IsNull((ConstructorInfoWrapper) (ConstructorInfo) null);
		}

		[TestMethod]
		public void ImplicitOperator_Test()
		{
			Assert.IsNull(null as ConstructorInfoWrapper);
			Assert.IsNull((object) CreateConstructorInfo() as ConstructorInfoWrapper);
		}

		[TestMethod]
		public void Invoke_ShouldCallInvokeOnTheWrappedConstructorInfo()
		{
			ConstructorInfo constructorInfo = typeof(ConstructorInfoWrapperTestClass).GetConstructors()[0];
			object constructedObject = new ConstructorInfoWrapper(constructorInfo).Invoke(new[] {"", new object(), 0});
			Assert.IsTrue(constructedObject is ConstructorInfoWrapperTestClass);
		}

		#endregion
	}

	internal class ConstructorInfoWrapperTestClass
	{
		#region Constructors

		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "firstParameter")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "secondParameter")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "thirdParameter")]
		public ConstructorInfoWrapperTestClass(string firstParameter, object secondParameter, int thirdParameter) {}

		#endregion
	}
}