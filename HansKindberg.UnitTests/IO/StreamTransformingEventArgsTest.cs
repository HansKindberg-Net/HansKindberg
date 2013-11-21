using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using HansKindberg.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.IO
{
	[TestClass]
	public class StreamTransformingEventArgsTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.StreamTransformingEventArgs")]
		public void Constructor_IfTheEncodingParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new StreamTransformingEventArgs(string.Empty, null);
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
			Assert.AreEqual(content, new StreamTransformingEventArgs(content, Mock.Of<Encoding>()).Content);
		}

		[TestMethod]
		public void Constructor_ShouldSetTheEncodingPropertyToTheEncodingParameterValue()
		{
			Encoding encoding = Mock.Of<Encoding>();
			Assert.AreEqual(encoding, new StreamTransformingEventArgs(null, encoding).Encoding);
		}

		[TestMethod]
		public void Constructor_ShouldSetTheTransformedContentPropertyToTheContentParameterValue()
		{
			const string content = "Test";
			Assert.AreEqual(content, new StreamTransformingEventArgs(content, Mock.Of<Encoding>()).TransformedContent);
		}

		[TestMethod]
		public void TransformedContent_Set_ShouldBeAbleToSetToNull()
		{
			StreamTransformingEventArgs streamTransformingEventArgs = new StreamTransformingEventArgs("Test", Mock.Of<Encoding>())
				{
					TransformedContent = null
				};
			Assert.IsNull(streamTransformingEventArgs.TransformedContent);
		}

		[TestMethod]
		public void TransformedContent_Set_Test()
		{
			const string transformedContent = "Transformed content";
			StreamTransformingEventArgs streamTransformingEventArgs = new StreamTransformingEventArgs("Test", Mock.Of<Encoding>())
				{
					TransformedContent = transformedContent
				};
			Assert.AreEqual(transformedContent, streamTransformingEventArgs.TransformedContent);
		}

		#endregion
	}
}