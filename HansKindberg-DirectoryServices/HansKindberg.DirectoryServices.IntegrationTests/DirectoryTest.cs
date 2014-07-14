using System.DirectoryServices;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.IntegrationTests
{
	[TestClass]
	public class DirectoryTest
	{
		#region Methods

		private static void AssertSearchResultAndDirectoryItemAreEqual(SearchResult searchResult, IDirectoryItem directoryItem)
		{
			Assert.AreEqual(searchResult.Path, directoryItem.Url.ToString());

			Assert.AreEqual(searchResult.Properties.Count, directoryItem.Properties.Count);

			for(int i = 0; i < searchResult.Properties.Count; i++)
			{
				// ReSharper disable AssignNullToNotNullAttribute
				var propertyName = searchResult.Properties.PropertyNames.Cast<string>().ElementAt(i);
				// ReSharper restore AssignNullToNotNullAttribute

				Assert.AreEqual(propertyName, directoryItem.Properties.Keys.ElementAt(i));

				var searchResultPropertyValueAsEnumerable = searchResult.Properties[propertyName].Cast<object>().ToArray();
				var searchResultPropertyValue = searchResultPropertyValueAsEnumerable.Count() > 1 ? searchResultPropertyValueAsEnumerable : searchResultPropertyValueAsEnumerable[0];

				GeneralDirectoryTest.AssertPropertyValuesAreEqual(propertyName, searchResultPropertyValue, directoryItem.Properties[propertyName]);
			}
		}

		private static Directory CreateDefaultDomainDirectory()
		{
			return CreateDirectory(CreateDefaultDomainDirectoryConnection());
		}

		private static IDirectoryConnection CreateDefaultDomainDirectoryConnection()
		{
			var connection = new DirectoryConnection();

			connection.Url.DistinguishedName = CreateDistinguishedNameParser().Parse("OU=Tests,DC=local,DC=net");
			connection.Url.Host = "LOCAL";
			connection.Url.Scheme = Scheme.LDAP;

			return connection;
		}

		//private static Directory CreateDirectory()
		//{
		//	var distinguishedNameParser = CreateDistinguishedNameParser();

		//	return new Directory(CreateDirectoryUriParser(distinguishedNameParser), distinguishedNameParser);
		//}
		private static Directory CreateDirectory(IDirectoryConnection directoryConnection)
		{
			var distinguishedNameParser = CreateDistinguishedNameParser();

			return new Directory(directoryConnection, CreateDirectoryUriParser(distinguishedNameParser), distinguishedNameParser);
		}

		//private static IDirectoryUriParser CreateDirectoryUriParser()
		//{
		//	return CreateDirectoryUriParser(CreateDistinguishedNameParser());
		//}
		private static IDirectoryUriParser CreateDirectoryUriParser(IDistinguishedNameParser distinguishedNameParser)
		{
			return new DirectoryUriParser(distinguishedNameParser);
		}

		private static IDistinguishedNameComponentValidator CreateDistinguishedNameComponentValidator()
		{
			return new DistinguishedNameComponentValidator();
		}

		private static IDistinguishedNameParser CreateDistinguishedNameParser()
		{
			return new DistinguishedNameParser(CreateDistinguishedNameComponentValidator());
		}

		[TestMethod]
		public void Find_Test()
		{
			var directory = CreateDefaultDomainDirectory();

			foreach(var directoryItem in directory.Find())
			{
				using(var directoryEntry = new DirectoryEntry(directoryItem.Url.ToString()))
				{
					using(var directorySearcher = new DirectorySearcher(directoryEntry))
					{
						directorySearcher.SearchScope = SearchScope.Base;
						var searchResult = directorySearcher.FindOne();

						AssertSearchResultAndDirectoryItemAreEqual(searchResult, directoryItem);
					}
				}
			}
		}

		#endregion

		////private static Directory CreateDefaultDirectory()
		////{
		////	return new Directory(CreateDefaultDomainDirectoryConnection());
		////}

		////[TestMethod]
		////public void Get_WithDistinguishedNameParameter_IfTheDistinguishedNameDoesNotExist_Should()
		////{
		////	var directory = CreateDefaultDirectory();

		////	directory.Get("Hej=Hopp");
		////}

		//#region Methods

		//private static void RootTest(IDirectoryItem rootNode, DirectoryEntry rootEntry)
		//{
		//	const int expectedNumberOfProperties = 49;

		//	Assert.IsNotNull(rootNode);

		//	Assert.AreEqual(rootEntry.Guid, rootNode.Guid);
		//	Assert.AreEqual(rootEntry.NativeGuid, rootNode.NativeGuid);
		//	Assert.AreEqual(rootEntry.Properties.Count, rootNode.Properties.Count);
		//	Assert.AreEqual(expectedNumberOfProperties, rootNode.Properties.Count);
		//}

		//[TestMethod]
		//public void Root_Test()
		//{
		//	using (var rootEntry = new DirectoryEntry(Global.DefaultScheme.ToString() + "://" + Global.NetBiosDomainName))
		//	{
		//		var directory = new Directory
		//		{
		//			Host = Global.DomainName,
		//			Scheme = Global.DefaultScheme
		//		};

		//		RootTest(directory.Root, rootEntry);

		//		directory = new Directory
		//		{
		//			DistinguishedName = Global.DomainDistinguishedName,
		//			Host = Global.DomainName,
		//			Scheme = Global.DefaultScheme
		//		};

		//		RootTest(directory.Root, rootEntry);

		//		directory = new Directory
		//		{
		//			Host = Global.NetBiosDomainName,
		//			Scheme = Global.DefaultScheme
		//		};

		//		RootTest(directory.Root, rootEntry);

		//		directory = new Directory
		//		{
		//			DistinguishedName = Global.DomainDistinguishedName,
		//			Host = Global.NetBiosDomainName,
		//			Scheme = Global.DefaultScheme
		//		};

		//		RootTest(directory.Root, rootEntry);
		//	}
		//}

		//#endregion
	}
}