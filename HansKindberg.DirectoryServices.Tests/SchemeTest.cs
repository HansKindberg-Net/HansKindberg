using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.Tests
{
	[TestClass]
	public class SchemeTest
	{
		#region Methods

		[TestMethod]
		public void IIS_ShouldBeEqualToThree()
		{
			Assert.AreEqual(3, (int) Scheme.IIS);
		}

		[TestMethod]
		public void IIS_ToString_ShouldReturnAStringCasedCorrectly()
		{
			Assert.AreEqual("IIS", Scheme.IIS.ToString());
		}

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

		[TestMethod]
		public void WinNT_ShouldBeEqualToTwo()
		{
			Assert.AreEqual(2, (int) Scheme.WinNT);
		}

		[SuppressMessage("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId = "WinNT"), TestMethod]
		public void WinNT_ToString_ShouldReturnAStringCasedCorrectly()
		{
			Assert.AreEqual("WinNT", Scheme.WinNT.ToString());
		}

		#endregion
	}
}