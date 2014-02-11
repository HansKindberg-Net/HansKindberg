using HansKindberg.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.Connections
{
	// ReSharper disable InconsistentNaming
	[TestClass]
	public class ISecureConnectionSettingsTest
		// ReSharper restore InconsistentNaming
	{
		#region Methods

		[TestMethod]
		public void Password_ShouldBeAbleToMock()
		{
			const string testPassword = "Test";
			Mock<ISecureConnectionSettings> secureConnectionSettingsMock = new Mock<ISecureConnectionSettings>();
			secureConnectionSettingsMock.Setup(secureConnectionSettings => secureConnectionSettings.Password).Returns(testPassword);
			Assert.AreEqual(testPassword, secureConnectionSettingsMock.Object.Password);
		}

		[TestMethod]
		public void UserName_ShouldBeAbleToMock()
		{
			const string testUserName = "Test";
			Mock<ISecureConnectionSettings> secureConnectionSettingsMock = new Mock<ISecureConnectionSettings>();
			secureConnectionSettingsMock.Setup(secureConnectionSettings => secureConnectionSettings.UserName).Returns(testUserName);
			Assert.AreEqual(testUserName, secureConnectionSettingsMock.Object.UserName);
		}

		#endregion
	}
}