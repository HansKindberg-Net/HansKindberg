using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HansKindberg.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Tests.Connections
{
	// ReSharper disable InconsistentNaming
	[TestClass]
	public class ISecureConnectionSettingsTest
		// ReSharper restore InconsistentNaming
	{
		[TestMethod]
		public void AuthenticationMethod_ShouldBeAbleToMock()
		{
			Mock<ISecureConnectionSettings> connectionStringParserMock = new Mock<ISecureConnectionSettings>();
			connectionStringParserMock.SetupAllProperties();
			const AuthenticationMethod testAuthenticationMethod = AuthenticationMethod.Credentials;
			connectionStringParserMock.Object.AuthenticationMethod = testAuthenticationMethod;
			Assert.AreEqual(testAuthenticationMethod, connectionStringParserMock.Object.AuthenticationMethod);
		}

		[TestMethod]
		public void UserName_ShouldBeAbleToMock()
		{
			Mock<ISecureConnectionSettings> connectionStringParserMock = new Mock<ISecureConnectionSettings>();
			connectionStringParserMock.SetupAllProperties();
			const string testUserName = "Test";
			connectionStringParserMock.Object.UserName = testUserName;
			Assert.AreEqual(testUserName, connectionStringParserMock.Object.UserName);
		}

		[TestMethod]
		public void Password_ShouldBeAbleToMock()
		{
			Mock<ISecureConnectionSettings> connectionStringParserMock = new Mock<ISecureConnectionSettings>();
			connectionStringParserMock.SetupAllProperties();
			const string testPassword = "Test";
			connectionStringParserMock.Object.Password = testPassword;
			Assert.AreEqual(testPassword, connectionStringParserMock.Object.Password);
		}
	}
}
