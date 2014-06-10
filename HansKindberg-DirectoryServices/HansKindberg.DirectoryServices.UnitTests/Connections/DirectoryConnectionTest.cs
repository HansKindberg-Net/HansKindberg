using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests.Connections
{
	[TestClass]
	public class DirectoryConnectionTest
	{
		#region Methods

		[TestMethod]
		public void AuthenticationTypes_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryConnection().AuthenticationTypes);
		}

		[TestMethod]
		public void DistinguishedName_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryConnection().DistinguishedName);
		}

		[TestMethod]
		public void Host_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryConnection().Host);
		}

		[TestMethod]
		public void Password_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryConnection().Password);
		}

		[TestMethod]
		public void Port_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryConnection().Port);
		}

		[TestMethod]
		public void Scheme_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryConnection().Scheme);
		}

		[TestMethod]
		public void UserName_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new DirectoryConnection().UserName);
		}

		#endregion
	}
}