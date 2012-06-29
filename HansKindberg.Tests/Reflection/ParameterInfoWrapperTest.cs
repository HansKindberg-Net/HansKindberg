using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HansKindberg.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Tests.Reflection
{
	[TestClass]
	public class ParameterInfoWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Reflection.ParameterInfoWrapper")]
		public void Constructor_ShouldThrowAnArgumentNullException_IfTheParameterInfoParameterIsNull()
		{
			new ParameterInfoWrapper(null);
		}

		private static ParameterInfo CreateParameterInfo()
		{
			return (ParameterInfo) Activator.CreateInstance(typeof(ParameterInfo), true);
		}

		[TestMethod]
		public void FromParameterInfo_ShouldNotReturnNull_IfTheParameterInfoIsNotNull()
		{
			Assert.IsNotNull(ParameterInfoWrapper.FromParameterInfo(CreateParameterInfo()));
		}

		[TestMethod]
		public void FromParameterInfo_ShouldReturnNull_IfTheParameterInfoIsNull()
		{
			Assert.IsNull(ParameterInfoWrapper.FromParameterInfo(null));
		}

		[TestMethod]
		public void ImplicitOperator_ShouldNotReturnNull_IfTheParameterInfoIsNotNull()
		{
			Assert.IsNotNull((ParameterInfoWrapper) CreateParameterInfo());
		}

		[TestMethod]
		public void ImplicitOperator_ShouldReturnNull_IfTheParameterInfoIsNull()
		{
			Assert.IsNull((ParameterInfoWrapper) (ParameterInfo) null);
		}

		[TestMethod]
		public void ImplicitOperator_Test()
		{
			Assert.IsNull(null as ParameterInfoWrapper);
			Assert.IsNull((object) CreateParameterInfo() as ParameterInfoWrapper);
		}

		[TestMethod]
		public void ParameterInfoIsNotAParameterInfoWrapper()
		{
			ParameterInfo parameterInfo = CreateParameterInfo();
			Assert.IsNotNull(parameterInfo);
#pragma warning disable 184
			Assert.IsFalse(parameterInfo is ParameterInfoWrapper);
#pragma warning restore 184
		}

		[TestMethod]
		public void ParameterType_ShouldReturnATypeWrapperAroundTheParameterTypeOfTheWrappedParameterInfo()
		{
			Mock<ParameterInfo> parameterInfoMock = new Mock<ParameterInfo>();
			parameterInfoMock.Setup(parameterInfo => parameterInfo.ParameterType).Returns(typeof(ParameterInfoWrapperTest));
			ParameterInfoWrapper parameterInfoWrapper = new ParameterInfoWrapper(parameterInfoMock.Object);
			IType parameterType = parameterInfoWrapper.ParameterType;
			Assert.IsNotNull(parameterType);
			Assert.IsTrue(parameterType is TypeWrapper);
			Type type = (Type) typeof(TypeWrapper).GetField("_type", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(parameterType);
			Assert.AreEqual(typeof(ParameterInfoWrapperTest), type);
		}

		#endregion
	}
}