using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Tests
{
	[TestClass]
	public class XmlNodeWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Xml.XmlNodeWrapper`1<System.Xml.XmlNode>")]
		public void Constructor_IfTheXmlNodeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new XmlNodeWrapper<XmlNode>(null);
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Xml.XmlNodeWrapper`1<System.Xml.XmlNode>")]
		public void Constructor_IfTheXmlNodeParameterIsNull_ShouldThrowAnArgumentNullExceptionWithParameterNameSetToXmlNodeWithTheFirstCharacterToLower()
		{
			string parameterName = null;

			try
			{
				new XmlNodeWrapper<XmlNode>(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				parameterName = argumentNullException.ParamName;
			}

			Assert.AreEqual("xmlNode", parameterName);
		}

		#endregion
	}
}