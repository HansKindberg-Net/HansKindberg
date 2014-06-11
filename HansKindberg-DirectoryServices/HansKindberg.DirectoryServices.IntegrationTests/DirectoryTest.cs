using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using HansKindberg.DirectoryServices.Connections;
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

		private static void RootTest(IDirectoryNode rootNode, DirectoryEntry rootEntry)
		{
			const int expectedNumberOfProperties = 49;

			Assert.IsNotNull(rootNode);

			Assert.AreEqual(rootEntry.Guid, rootNode.Guid);
			Assert.AreEqual(rootEntry.NativeGuid, rootNode.NativeGuid);
			Assert.AreEqual(rootEntry.Properties.Count, rootNode.Properties.Count);
			Assert.AreEqual(expectedNumberOfProperties, rootNode.Properties.Count);
			
		}
	}
}
