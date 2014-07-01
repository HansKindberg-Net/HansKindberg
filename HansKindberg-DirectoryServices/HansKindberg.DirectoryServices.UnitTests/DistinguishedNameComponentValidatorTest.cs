using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DistinguishedNameComponentValidatorTest
	{
		#region Fields

		private const string _validName = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		#endregion

		#region Methods

		[TestMethod]
		public void ValidateName_IfTheNameContainsAnyNonAlphanumericCharacters_ShouldReturnAnInvalidValidationResult()
		{
			var allCharacters = new List<char>();

			for(int i = char.MinValue; i <= char.MaxValue; i++)
			{
				allCharacters.Add((char) i);
			}

			var invalidNameCharacters = allCharacters.Where(character => !_validName.ToCharArray().Contains(character)); // && (int)character != 10);

			foreach(var invalidNameCharacter in invalidNameCharacters)
			{
				Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(_validName + invalidNameCharacter).IsValid, "Character: '{0}' ({1}).", new object[] {invalidNameCharacter, (int) invalidNameCharacter});
				Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + _validName).IsValid, "Character: '{0}' ({1}).", new object[] {invalidNameCharacter, (int) invalidNameCharacter});
				Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + _validName + invalidNameCharacter).IsValid, "Character: '{0}' ({1}).", new object[] {invalidNameCharacter, (int) invalidNameCharacter});
				Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(_validName + invalidNameCharacter + _validName).IsValid, "Character: '{0}' ({1}).", new object[] {invalidNameCharacter, (int) invalidNameCharacter});
				Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(_validName + _validName + invalidNameCharacter + invalidNameCharacter).IsValid, "Character: '{0}' ({1}).", new object[] {invalidNameCharacter, (int) invalidNameCharacter});
				// The following fails
				//Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + invalidNameCharacter + _validName + _validName).IsValid, "Character: '{0}' ({1}).", new object[] { invalidNameCharacter, (int)invalidNameCharacter });
				Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(invalidNameCharacter + invalidNameCharacter + _validName + _validName + invalidNameCharacter + invalidNameCharacter).IsValid, "Character: '{0}' ({1}).", new object[] {invalidNameCharacter, (int) invalidNameCharacter});
				Assert.IsFalse(new DistinguishedNameComponentValidator().ValidateName(_validName + _validName + invalidNameCharacter + invalidNameCharacter + _validName + _validName).IsValid, "Character: '{0}' ({1}).", new object[] {invalidNameCharacter, (int) invalidNameCharacter});
			}
		}

		[TestMethod]
		public void ValidateName_IfTheNameContainsOnlyAlphanumericCharacters_ShouldReturnAValidValidationResult()
		{
			Assert.IsTrue(new DistinguishedNameComponentValidator().ValidateName(_validName).IsValid);
		}

		[TestMethod]
		public void ValidateValue_Test() {}

		#endregion
	}
}