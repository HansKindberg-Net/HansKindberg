using System.DirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.IntegrationTests
{
	[TestClass]
	public class DirectoryTest
	{
		//private static IDirectoryConnection CreateDefaultDomainDirectoryConnection()
		//{
		//	return new DirectoryConnection
		//	{
		//		Host = "LOCAL",
		//		Scheme = Scheme.LDAP
		//	};
		//}

		//private static Directory CreateDefaultDirectory()
		//{
		//	return new Directory(CreateDefaultDomainDirectoryConnection());
		//}

		//[TestMethod]
		//public void Get_WithDistinguishedNameParameter_IfTheDistinguishedNameDoesNotExist_Should()
		//{
		//	var directory = CreateDefaultDirectory();

		//	directory.Get("Hej=Hopp");
		//}

		#region Methods

		private static void RootTest(IDirectoryNode rootNode, DirectoryEntry rootEntry)
		{
			const int expectedNumberOfProperties = 49;

			Assert.IsNotNull(rootNode);

			Assert.AreEqual(rootEntry.Guid, rootNode.Guid);
			Assert.AreEqual(rootEntry.NativeGuid, rootNode.NativeGuid);
			Assert.AreEqual(rootEntry.Properties.Count, rootNode.Properties.Count);
			Assert.AreEqual(expectedNumberOfProperties, rootNode.Properties.Count);
		}

		[TestMethod]
		public void Root_Test()
		{
			using(var rootEntry = new DirectoryEntry(Global.DefaultScheme.ToString() + "://" + Global.NetBiosDomainName))
			{
				var directory = new Directory
				{
					Host = Global.DomainName,
					Scheme = Global.DefaultScheme
				};

				RootTest(directory.Root, rootEntry);

				directory = new Directory
				{
					DistinguishedName = Global.DomainDistinguishedName,
					Host = Global.DomainName,
					Scheme = Global.DefaultScheme
				};

				RootTest(directory.Root, rootEntry);

				directory = new Directory
				{
					Host = Global.NetBiosDomainName,
					Scheme = Global.DefaultScheme
				};

				RootTest(directory.Root, rootEntry);

				directory = new Directory
				{
					DistinguishedName = Global.DomainDistinguishedName,
					Host = Global.NetBiosDomainName,
					Scheme = Global.DefaultScheme
				};

				RootTest(directory.Root, rootEntry);
			}
		}

		#endregion
	}
}