using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class SchemeTest
	{
		#region Methods

		[TestMethod]
		public void LDAPS_ShouldBeEqualToOne()
		{
			Assert.AreEqual(1, (int) Scheme.LDAPS);
		}

		[TestMethod]
		public void LDAPS_ToString_ShouldReturnAStringCasedCorrectly()
		{
			Assert.AreEqual("LDAPS", Scheme.LDAPS.ToString());
		}

		[TestMethod]
		public void LDAP_ShouldBeEqualToZero()
		{
			Assert.AreEqual(0, (int) Scheme.LDAP);
		}

		[TestMethod]
		public void LDAP_ToString_ShouldReturnAStringCasedCorrectly()
		{
			Assert.AreEqual("LDAP", Scheme.LDAP.ToString());
		}

		#endregion
	}
}