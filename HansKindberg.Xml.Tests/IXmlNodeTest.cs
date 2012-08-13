using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Xml.Tests
{
	// ReSharper disable InconsistentNaming
	[TestClass]
	public class IXmlNodeTest
		// ReSharper restore InconsistentNaming
	{
		#region Methods

		[TestMethod]
		public void InnerXml_ShouldBeAbleToMock()
		{
			const string testInnerXml = "Test";
			Mock<IXmlNode> xmlNodeMock = new Mock<IXmlNode>();
			xmlNodeMock.SetupProperty(xmlNode => xmlNode.InnerXml);
			Assert.IsNull(xmlNodeMock.Object.InnerXml);
			xmlNodeMock.Object.InnerXml = testInnerXml;
			Assert.AreEqual(testInnerXml, xmlNodeMock.Object.InnerXml);
		}

		[TestMethod]
		public void OuterXml_ShouldBeAbleToMock()
		{
			const string testOuterXml = "Test";
			Mock<IXmlNode> xmlNodeMock = new Mock<IXmlNode>();
			Assert.IsNull(xmlNodeMock.Object.OuterXml);
			xmlNodeMock.SetupGet(xmlNode => xmlNode.OuterXml).Returns(testOuterXml);
			Assert.AreEqual(testOuterXml, xmlNodeMock.Object.OuterXml);
		}

		#endregion
	}
}