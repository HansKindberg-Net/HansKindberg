using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using HansKindberg.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.IO
{
	[TestClass]
	public class StreamEventArgsTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.StreamEventArgs")]
		public void Constructor_IfTheEncodingParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new StreamEventArgs(string.Empty, null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "encoding")
					throw;
			}
		}

		[TestMethod]
		public void Constructor_ShouldSetTheContentPropertyToTheContentParameterValue()
		{
			const string content = "Test";
			Assert.AreEqual(content, new StreamEventArgs(content, Mock.Of<Encoding>()).Content);
		}

		[TestMethod]
		public void Constructor_ShouldSetTheEncodingPropertyToTheEncodingParameterValue()
		{
			Encoding encoding = Mock.Of<Encoding>();
			Assert.AreEqual(encoding, new StreamEventArgs(null, encoding).Encoding);
		}

		#endregion
	}
}