using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryAuthenticationTest
	{
		#region Methods

		[TestMethod]
		public void AuthenticationTypes_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryAuthentication().AuthenticationTypes);
		}

		[TestMethod]
		public void Password_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryAuthentication().Password);
		}

		[TestMethod]
		public void UserName_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryAuthentication().UserName);
		}

		#endregion
	}
}