using System;
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
		public void AddExceptions_IfTheExceptionsParameterContainsANullValue_ShouldThrowAnArgumentException()
		{
			Exception[] exceptions = new Exception[1];
			exceptions[0] = null;
			new ValidationResult().AddExceptions(exceptions);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddExceptions_IfTheExceptionsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new ValidationResult().AddExceptions(null);
		}

		[TestMethod]
		public void Exceptions_ShouldNeverReturnNull()
		{
			Assert.IsNotNull(new ValidationResult().Exceptions);
		}

		[TestMethod]
		public void IsValid_IfThereAreExceptions_ShouldReturnFalse()
		{
			ValidationResult validationResult = new ValidationResult();
			validationResult.Exceptions.Add(new InvalidOperationException());
			Assert.IsFalse(validationResult.IsValid);
		}

		[TestMethod]
		public void IsValid_IfThereAreNoExceptions_ShouldReturnTrue()
		{
			ValidationResult validationResult = new ValidationResult();
			validationResult.Exceptions.Add(new InvalidOperationException());
			Assert.IsFalse(validationResult.IsValid);
			validationResult.Exceptions.Clear();
			Assert.IsTrue(validationResult.IsValid);
		}

		[TestMethod]
		public void IsValid_ShouldByDefaultReturnTrue()
		{
			Assert.IsTrue(new ValidationResult().IsValid);
		}

		#endregion
	}
}