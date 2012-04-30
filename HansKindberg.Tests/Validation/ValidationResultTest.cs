using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Tests.Validation
{
	[TestClass]
	public class ValidationResultTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AddExceptions_ShouldThrowAnArgumentException_IfTheExceptionsParameterContainsANullValue()
		{
			Exception[] exceptions = new Exception[1];
			exceptions[0] = null;
			new ValidationResult().AddExceptions(exceptions);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddExceptions_ShouldThrowAnArgumentNullException_IfTheExceptionsParameterIsNull()
		{
			new ValidationResult().AddExceptions(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Validation.ValidationResult")]
		public void Constructor_ShouldThrowAnArgumentNullException_IfTheValidationResultToCopyIsNull()
		{
			new ValidationResult(null);
		}

		[TestMethod]
		public void Constructor_WithNoParameters_ShouldByDefaultSetIsValidToTrue()
		{
			Assert.IsTrue(new ValidationResult().IsValid);
		}

		[TestMethod]
		public void Exceptions_ShouldNeverReturnNull()
		{
			Assert.IsNotNull(new ValidationResult(true, null).Exceptions);
		}

		#endregion
	}
}