using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests.Connections
{
	[TestClass]
	public class DirectoryConnectionTest
	{
		#region Methods

		[TestMethod]
		public void Authentication_ShouldNotReturnNullByDefault()
		{
			Assert.IsNotNull(new DirectoryConnection().Authentication);
		}

		[TestMethod]
		public void Url_ShouldNotReturnNullByDefault()
		{
			Assert.IsNotNull(new DirectoryConnection().Url);
		}

		#endregion
	}
}