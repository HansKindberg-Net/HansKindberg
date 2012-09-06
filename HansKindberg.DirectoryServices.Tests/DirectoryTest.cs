using System.DirectoryServices;
using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.DirectoryServices.Tests
{
	[TestClass]
	public class DirectoryTest
	{
		#region Methods

		[TestMethod]
		public void DirectorySearcherPrerequisiteTest()
		{
			using(DirectorySearcher directorySearcher = new DirectorySearcher())
			{
				Assert.IsNotNull(directorySearcher.Filter);
				Assert.AreEqual(0, directorySearcher.PropertiesToLoad.Count);
				Assert.AreEqual(SearchScope.Subtree, directorySearcher.SearchScope);
				// Assert.IsNotNull(directorySearcher.SearchRoot); // This only works when the computer you run the unit tests on is on a domain.
			}

			using(DirectorySearcher directorySearcher = new DirectorySearcher((DirectoryEntry) null))
			{
				Assert.IsNotNull(directorySearcher.Filter);
				Assert.AreEqual(0, directorySearcher.PropertiesToLoad.Count);
				Assert.AreEqual(SearchScope.Subtree, directorySearcher.SearchScope);
				// Assert.IsNotNull(directorySearcher.SearchRoot); // This only works when the computer you run the unit tests on is on a domain.
			}

			using(DirectorySearcher directorySearcher = new DirectorySearcher((string) null))
			{
				Assert.IsNull(directorySearcher.Filter);
				Assert.AreEqual(0, directorySearcher.PropertiesToLoad.Count);
				Assert.AreEqual(SearchScope.Subtree, directorySearcher.SearchScope);
				// Assert.IsNotNull(directorySearcher.SearchRoot); // This only works when the computer you run the unit tests on is on a domain.
			}
		}

		[TestMethod]
		public void HostUrl_ShouldAlwaysReturnAStringWithATrailingSlash()
		{
			Mock<IConnectionSettings> connectionSettingsMock = new Mock<IConnectionSettings>();
			connectionSettingsMock.Setup(connectionSettings => connectionSettings.Scheme).Returns(Scheme.LDAP);

			Assert.AreEqual("LDAP://", new Directory(connectionSettingsMock.Object).HostUrl);

			connectionSettingsMock.Setup(connectionSettings => connectionSettings.Host).Returns("testhost");

			Assert.AreEqual("LDAP://testhost/", new Directory(connectionSettingsMock.Object).HostUrl);
		}

		#endregion
	}
}