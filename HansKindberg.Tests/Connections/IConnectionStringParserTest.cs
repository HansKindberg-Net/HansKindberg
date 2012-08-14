using System.Collections.Generic;
using HansKindberg.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Tests.Connections
{
	// ReSharper disable InconsistentNaming
	[TestClass]
	public class IConnectionStringParserTest
		// ReSharper restore InconsistentNaming
	{
		#region Methods

		[TestMethod]
		public void ToDictionary_ShouldBeAbleToMock()
		{
			const string testConnectionString = "Test";
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			Mock<IConnectionStringParser> connectionStringParserMock = new Mock<IConnectionStringParser>();
			connectionStringParserMock.Setup(connectionStringParser => connectionStringParser.ToDictionary(testConnectionString)).Returns(dictionary);
			Assert.AreEqual(dictionary, connectionStringParserMock.Object.ToDictionary(testConnectionString));
		}

		#endregion
	}
}