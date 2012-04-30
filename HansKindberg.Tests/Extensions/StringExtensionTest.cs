using System;
using HansKindberg.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Tests.Extensions
{
	[TestClass]
	public class StringExtensionTest
	{
		#region Methods

		[TestMethod]
		public void FirstCharacterToLower_ShouldReturnAStringWithTheFirstCharacterAsLowerCase()
		{
			Assert.AreEqual("aAAA", "AAAA".FirstCharacterToLower());
		}

		[TestMethod]
		public void FirstCharacterToLower_ShouldReturnAnEmptyString_IfTheStringIsEmpty()
		{
			Assert.AreEqual(string.Empty, string.Empty.FirstCharacterToLower());
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void FirstCharacterToLower_ShouldThrowANullReferenceException_IfTheStringIsNull()
		{
			string str = null;
			str.FirstCharacterToLower();
		}

		[TestMethod]
		public void FirstCharacterToUpper_ShouldReturnAStringWithTheFirstCharacterAsUpperCase()
		{
			Assert.AreEqual("Aaaa", "aaaa".FirstCharacterToUpper());
		}

		[TestMethod]
		public void FirstCharacterToUpper_ShouldReturnAnEmptyString_IfTheStringIsEmpty()
		{
			Assert.AreEqual(string.Empty, string.Empty.FirstCharacterToUpper());
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void FirstCharacterToUpper_ShouldThrowANullReferenceException_IfTheStringIsNull()
		{
			string str = null;
			str.FirstCharacterToUpper();
		}

		#endregion
	}
}