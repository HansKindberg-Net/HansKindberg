using System;
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

		private static void AssertDirectoryEntryAndDirectoryNodeAreEqual(DirectoryEntry directoryEntry, IDirectoryNode directoryNode)
		{
			Assert.AreEqual(directoryEntry.Path, directoryNode.Url.ToString());

			Assert.AreEqual(directoryEntry.Properties.Count, directoryNode.Properties.Count);

			for(int i = 0; i < directoryEntry.Properties.Count; i++)
			{
				// ReSharper disable AssignNullToNotNullAttribute
				var propertyName = directoryEntry.Properties.PropertyNames.Cast<string>().ElementAt(i);
				// ReSharper restore AssignNullToNotNullAttribute

				Assert.AreEqual(propertyName, directoryNode.Properties.Keys.ElementAt(i));

				AssertPropertyValuesAreEqual(propertyName, directoryEntry.Properties[propertyName].Value, directoryNode.Properties[propertyName]);
			}
		}

		private static void AssertPropertyValuesAreEqual(string propertyName, object expectedPropertyValue, object actualPropertyValue)
		{
			var expectedPropertyValueAsArray = expectedPropertyValue as Array;

			if(expectedPropertyValueAsArray != null)
			{
				var actualPropertyValueAsArray = actualPropertyValue as Array;

				if(actualPropertyValueAsArray != null)
				{
					Assert.AreEqual(expectedPropertyValueAsArray.Length, actualPropertyValueAsArray.Length, "Property-name: \"{0}\". The length of the arrays are not equal.", new object[] {propertyName});

					for(int i = 0; i < expectedPropertyValueAsArray.Length; i++)
					{
						AssertPropertyValuesAreEqual(propertyName, expectedPropertyValueAsArray.GetValue(i), actualPropertyValueAsArray.GetValue(i));
					}
				}
				else
				{
					Assert.AreEqual(expectedPropertyValueAsArray, actualPropertyValue, "Property-name: \"{0}\". The value should be an array.", new object[] {propertyName});
				}
			}
			else
			{
				Assert.AreEqual(expectedPropertyValue, actualPropertyValue, "Property-name: \"{0}\".", new object[] {propertyName});
			}
		}

		private static void AssertSearchResultAndDirectoryNodeAreEqual(SearchResult searchResult, IDirectoryNode directoryNode)
		{
			Assert.AreEqual(searchResult.Path, directoryNode.Url.ToString());

			Assert.AreEqual(searchResult.Properties.Count, directoryNode.Properties.Count);

			for(int i = 0; i < searchResult.Properties.Count; i++)
			{
				// ReSharper disable AssignNullToNotNullAttribute
				var propertyName = searchResult.Properties.PropertyNames.Cast<string>().ElementAt(i);
				// ReSharper restore AssignNullToNotNullAttribute

				Assert.AreEqual(propertyName, directoryNode.Properties.Keys.ElementAt(i));

				var searchResultPropertyValueAsEnumerable = searchResult.Properties[propertyName].Cast<object>().ToArray();
				var searchResultPropertyValue = searchResultPropertyValueAsEnumerable.Count() > 1 ? searchResultPropertyValueAsEnumerable : searchResultPropertyValueAsEnumerable[0];

				AssertPropertyValuesAreEqual(propertyName, searchResultPropertyValue, directoryNode.Properties[propertyName]);
			}
		}

		private static Directory CreateDefaultDomainDirectory()
		{
			return CreateDirectory(CreateDefaultDomainDirectoryConnection());
		}

		private static IDirectoryConnection CreateDefaultDomainDirectoryConnection()
		{
			return new DirectoryConnection
			{
				DistinguishedName = CreateDistinguishedNameParser().Parse("OU=Tests,DC=local,DC=net"),
				Host = "LOCAL",
				Scheme = Scheme.LDAP
			};
		}

		private static Directory CreateDirectory()
		{
			var distinguishedNameParser = CreateDistinguishedNameParser();

			return new Directory(CreateDirectoryUriParser(distinguishedNameParser), distinguishedNameParser);
		}

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

		private static DirectoryEntry CreateLocalMachineEntry()
		{
			return new DirectoryEntry("WinNT://" + Environment.MachineName);
		}

		[TestMethod]
		public void Find_Test()
		{
			var directory = CreateDefaultDomainDirectory();

			foreach(var directoryNode in directory.Find())
			{
				using(var directoryEntry = new DirectoryEntry(directoryNode.Url.ToString()))
				{
					using(var directorySearcher = new DirectorySearcher(directoryEntry))
					{
						directorySearcher.SearchScope = SearchScope.Base;
						var searchResult = directorySearcher.FindOne();

						AssertSearchResultAndDirectoryNodeAreEqual(searchResult, directoryNode);
					}
				}
			}
		}

		[TestMethod]
		public void Get_WithPathParameter_IfTheConnectingToTheLocalMachine_ShouldReturnTheCorrectProperties()
		{
			using(var directoryEntry = CreateLocalMachineEntry())
			{
				var directory = (IGlobalDirectory) CreateDirectory();

				var directoryNode = directory.Get(directoryEntry.Path);

				AssertDirectoryEntryAndDirectoryNodeAreEqual(directoryEntry, directoryNode);
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

		//private static void RootTest(IDirectoryNode rootNode, DirectoryEntry rootEntry)
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