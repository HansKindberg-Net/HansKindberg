using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryUriTest
	{
		#region Methods

		[TestMethod]
		public void Scheme_ShouldReturnLdapByDefault()
		{
			Assert.AreEqual(Scheme.LDAP, new DirectoryUri().Scheme);
		}

		#endregion
	}
}