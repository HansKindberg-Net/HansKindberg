using System;
using System.DirectoryServices;
using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryTest
	{
		#region Methods

		private static Directory CreateDirectory()
		{
			var directoryConnectionMock = new Mock<IDirectoryConnection>();
			directoryConnectionMock.Setup(directoryConnection => directoryConnection.Authentication).Returns(new DirectoryAuthentication());
			directoryConnectionMock.Setup(directoryConnection => directoryConnection.Url).Returns(new DirectoryUri {Host = "localhost", Scheme = Scheme.LDAP});

			return new Directory(directoryConnectionMock.Object, Mock.Of<IDirectoryUriParser>(), Mock.Of<IDistinguishedNameParser>());
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

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Find_WithPathAndSearchOptionsAndAuthenticationParameter_IfTheSearchOptionsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				CreateDirectory().Find(string.Empty, null, Mock.Of<IDirectoryAuthentication>());
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "searchOptions")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Get_WithPathAndSingleSearchOptionsAndAuthenticationParameter_IfTheSingleSearchOptionsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				CreateDirectory().Get(string.Empty, null, Mock.Of<IDirectoryAuthentication>());
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "singleSearchOptions")
					throw;
			}
		}

		#endregion

		//[TestMethod]
		//public void DefaultSearchFilter_ShouldReturnObjectClassWildcardByDefault()
		//{
		//	Assert.AreEqual("(objectClass=*)", new Directory().DefaultSearchFilter);
		//}

		//[TestMethod]
		//public void DefaultSearchScope_ShouldReturnSubtreeByDefault()
		//{
		//	Assert.AreEqual(SearchScope.Subtree, new Directory().DefaultSearchScope);
		//}
	}
}