using System.DirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryTest
	{
		#region Methods

		[TestMethod]
		public void DefaultSearchFilter_ShouldReturnObjectClassWildcardByDefault()
		{
			Assert.AreEqual("(objectClass=*)", new Directory().DefaultSearchFilter);
		}

		[TestMethod]
		public void DefaultSearchScope_ShouldReturnSubtreeByDefault()
		{
			Assert.AreEqual(SearchScope.Subtree, new Directory().DefaultSearchScope);
		}

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

		#endregion
	}
}