using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Xml.UnitTests
{
	// ReSharper disable InconsistentNaming
	[TestClass]
	public class IXmlDocumentTest
		// ReSharper restore InconsistentNaming
	{
		#region Methods

		[TestMethod]
		public void LoadXml_ShouldBeAbleToMock()
		{
			const string testXml = "Test";
			List<string> loadXmlCalls = new List<string>();
			Mock<IXmlDocument> xmlDocumentMock = new Mock<IXmlDocument>();
			xmlDocumentMock.Setup(xmlDocument => xmlDocument.LoadXml(testXml)).Callback<string>(loadXmlCalls.Add);
			Assert.IsFalse(loadXmlCalls.Any());
			xmlDocumentMock.Object.LoadXml(testXml);
			Assert.AreEqual(1, loadXmlCalls.Count);
			Assert.AreEqual(testXml, loadXmlCalls[0]);
		}

		[TestMethod]
		public void Load_WithOneStringParameter_ShouldBeAbleToMock()
		{
			const string testFileName = "Test";
			List<string> loadCalls = new List<string>();
			Mock<IXmlDocument> xmlDocumentMock = new Mock<IXmlDocument>();
			xmlDocumentMock.Setup(xmlDocument => xmlDocument.Load(testFileName)).Callback<string>(loadCalls.Add);
			Assert.IsFalse(loadCalls.Any());
			xmlDocumentMock.Object.Load(testFileName);
			Assert.AreEqual(1, loadCalls.Count);
			Assert.AreEqual(testFileName, loadCalls[0]);
		}

		[TestMethod]
		public void Save_WithOneStringParameter_ShouldBeAbleToMock()
		{
			const string testFileName = "Test";
			List<string> saveCalls = new List<string>();
			Mock<IXmlDocument> xmlDocumentMock = new Mock<IXmlDocument>();
			xmlDocumentMock.Setup(xmlDocument => xmlDocument.Save(testFileName)).Callback<string>(saveCalls.Add);
			Assert.IsFalse(saveCalls.Any());
			xmlDocumentMock.Object.Save(testFileName);
			Assert.AreEqual(1, saveCalls.Count);
			Assert.AreEqual(testFileName, saveCalls[0]);
		}

		#endregion
	}
}