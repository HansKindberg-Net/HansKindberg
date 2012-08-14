using HansKindberg.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Tests.Connections
{
	[TestClass]
	public class AuthenticationMethodTest
	{
		#region Methods

		[TestMethod]
		public void Anonymous_ShouldBeEqualToZero()
		{
			Assert.AreEqual(0, (int) AuthenticationMethod.Anonymous);
		}

		[TestMethod]
		public void Credentials_ShouldBeEqualToOne()
		{
			Assert.AreEqual(1, (int) AuthenticationMethod.Credentials);
		}

		[TestMethod]
		public void Impersonation_ShouldBeEqualToTwo()
		{
			Assert.AreEqual(2, (int) AuthenticationMethod.Impersonation);
		}

		#endregion
	}
}