using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Tests
{
	[TestClass]
	public class XmlDocumentWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Xml.XmlDocumentWrapper")]
		public void Constructor_IfTheXmlDocumentParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new XmlDocumentWrapper(null);
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Xml.XmlDocumentWrapper")]
		public void Constructor_IfTheXmlDocumentParameterIsNull_ShouldThrowAnArgumentNullExceptionWithParameterNameSetToXmlDocumentWithTheFirstCharacterToLower()
		{
			string parameterName = null;

			try
			{
				new XmlDocumentWrapper(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				parameterName = argumentNullException.ParamName;
			}

			Assert.AreEqual("xmlDocument", parameterName);
		}

		#endregion
	}
}