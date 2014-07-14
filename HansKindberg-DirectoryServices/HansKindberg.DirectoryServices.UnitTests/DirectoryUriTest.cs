using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryUriTest
	{
		#region Methods

		[TestMethod]
		public void DistinguishedName_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryUri().DistinguishedName);
		}

		[TestMethod]
		public void Host_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryUri().Host);
		}

		[TestMethod]
		public void Port_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryUri().Port);
		}

		[TestMethod]
		public void Scheme_ShouldReturnLdapByDefault()
		{
			Assert.AreEqual(Scheme.LDAP, new DirectoryUri().Scheme);
		}

		#endregion
	}
}