using System.DirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
				Assert.IsNotNull(directorySearcher.SearchRoot);
			}

			using(DirectorySearcher directorySearcher = new DirectorySearcher((DirectoryEntry) null))
			{
				Assert.IsNotNull(directorySearcher.Filter);
				Assert.AreEqual(0, directorySearcher.PropertiesToLoad.Count);
				Assert.AreEqual(SearchScope.Subtree, directorySearcher.SearchScope);
				Assert.IsNotNull(directorySearcher.SearchRoot);
			}

			using(DirectorySearcher directorySearcher = new DirectorySearcher((string) null))
			{
				Assert.IsNull(directorySearcher.Filter);
				Assert.AreEqual(0, directorySearcher.PropertiesToLoad.Count);
				Assert.AreEqual(SearchScope.Subtree, directorySearcher.SearchScope);
				Assert.IsNotNull(directorySearcher.SearchRoot);
			}
		}

		#endregion
	}
}