using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.UnitTests.Extensions
{
	[TestClass]
	public class StringExtensionTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "HansKindberg.Extensions.StringExtension.FirstCharacterToLower(System.String)")]
		public void FirstCharacterToLower_ShouldReturnAStringWithTheFirstCharacterAsLowercase()
		{
			Assert.AreEqual("tEST", "TEST".FirstCharacterToLower());
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "HansKindberg.Extensions.StringExtension.FirstCharacterToLower(System.String)")]
		public void FirstCharacterToLower_ShouldReturnAnEmptyString_IfTheStringIsEmpty()
		{
			Assert.AreEqual(string.Empty, string.Empty.FirstCharacterToLower());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "HansKindberg.Extensions.StringExtension.FirstCharacterToLower(System.String)")]
		public void FirstCharacterToLower_ShouldThrowAnArgumentNullException_IfTheStringIsNull()
		{
			((string) null).FirstCharacterToLower();
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "HansKindberg.Extensions.StringExtension.FirstCharacterToUpper(System.String)")]
		public void FirstCharacterToUpper_ShouldReturnAStringWithTheFirstCharacterAsUppercase()
		{
			Assert.AreEqual("Test", "test".FirstCharacterToUpper());
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "HansKindberg.Extensions.StringExtension.FirstCharacterToUpper(System.String)")]
		public void FirstCharacterToUpper_ShouldReturnAnEmptyString_IfTheStringIsEmpty()
		{
			Assert.AreEqual(string.Empty, string.Empty.FirstCharacterToUpper());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "HansKindberg.Extensions.StringExtension.FirstCharacterToUpper(System.String)")]
		public void FirstCharacterToUpper_ShouldThrowAnArgumentNullException_IfTheStringIsNull()
		{
			((string) null).FirstCharacterToUpper();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Like_IfPatternIsNull_ShouldThrowAnArgumentNullException()
		{
			Assert.IsFalse(string.Empty.Like(null));
		}

		[TestMethod]
		public void Like_IfValueIsEmptyAndPatternIsEmpty_ShouldReturnTrue()
		{
			Assert.IsTrue(string.Empty.Like(string.Empty));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Like_IfValueIsNull_ShouldThrowAnArgumentNullException()
		{
			Assert.IsFalse(((string) null).Like(string.Empty));
		}

		#endregion
	}
}