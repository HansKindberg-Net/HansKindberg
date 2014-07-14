using System.Diagnostics.CodeAnalysis;
using HansKindberg.DirectoryServices.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests.Windows
{
	[TestClass]
	public class WindowsSchemeTest
	{
		#region Methods

		[TestMethod]
		public void IIS_ShouldBeEqualToOne()
		{
			Assert.AreEqual(1, (int) WindowsScheme.IIS);
		}

		[TestMethod]
		public void IIS_ToString_ShouldReturnAStringCasedCorrectly()
		{
			Assert.AreEqual("IIS", WindowsScheme.IIS.ToString());
		}

		[TestMethod]
		public void WinNT_ShouldBeEqualToZero()
		{
			Assert.AreEqual(0, (int) WindowsScheme.WinNT);
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId = "WinNT")]
		public void WinNT_ToString_ShouldReturnAStringCasedCorrectly()
		{
			Assert.AreEqual("WinNT", WindowsScheme.WinNT.ToString());
		}

		#endregion
	}
}