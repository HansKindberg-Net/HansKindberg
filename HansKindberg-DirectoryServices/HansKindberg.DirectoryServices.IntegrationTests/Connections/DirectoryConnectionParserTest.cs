using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.IntegrationTests.Connections
{
	[TestClass]
	public class DirectoryConnectionParserTest
	{
		#region Methods

		private static DirectoryConnectionParser CreateDirectoryConnectionParser()
		{
			return new DirectoryConnectionParser(new DistinguishedNameParser(new DistinguishedNameComponentValidator()));
		}

		[TestMethod]
		public void Parse_Test()
		{
			const string distinguishedName = "CN=Test,OU=Test,DC=local,DC=net";

			var directoryConnectionParser = CreateDirectoryConnectionParser();

			var directoryConnection = directoryConnectionParser.Parse("DistinguishedName=" + distinguishedName);

			Assert.AreEqual(distinguishedName, directoryConnection.DistinguishedName.ToString());
		}

		#endregion
	}
}