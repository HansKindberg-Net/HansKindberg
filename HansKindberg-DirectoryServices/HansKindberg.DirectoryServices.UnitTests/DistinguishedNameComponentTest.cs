using HansKindberg.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DistinguishedNameComponentTest
	{
		#region Methods

		private static IDistinguishedNameComponentValidator CreateAcceptanceDistinguishedNameComponentValidator()
		{
			var validValidationResult = CreateValidValidationResult();

			var distinguishedNameComponentValidatorMock = new Mock<IDistinguishedNameComponentValidator>();

			distinguishedNameComponentValidatorMock.Setup(distinguishedNameComponentValidator => distinguishedNameComponentValidator.ValidateName(It.IsAny<string>())).Returns(validValidationResult);
			distinguishedNameComponentValidatorMock.Setup(distinguishedNameComponentValidator => distinguishedNameComponentValidator.ValidateValue(It.IsAny<string>())).Returns(validValidationResult);

			return distinguishedNameComponentValidatorMock.Object;
		}

		private static DistinguishedNameComponent CreateDefaultDistinguishedNameComponent()
		{
			return CreateDefaultDistinguishedNameComponent("TestName", "TestValue");
		}

		private static DistinguishedNameComponent CreateDefaultDistinguishedNameComponent(string name, string value)
		{
			return new DistinguishedNameComponent(name, value, CreateAcceptanceDistinguishedNameComponentValidator());
		}

		private static IValidationResult CreateValidValidationResult()
		{
			var validationResultMock = new Mock<IValidationResult>();

			validationResultMock.Setup(validationResult => validationResult.IsValid).Returns(true);

			return validationResultMock.Object;
		}

		[TestMethod]
		public void PersistNameCaseWhenConvertingToString_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(CreateDefaultDistinguishedNameComponent().PersistNameCaseWhenConvertingToString);
		}

		#endregion
	}
}