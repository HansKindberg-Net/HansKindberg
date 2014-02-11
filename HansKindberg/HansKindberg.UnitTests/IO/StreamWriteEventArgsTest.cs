using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using HansKindberg.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.IO
{
	[TestClass]
	public class StreamWriteEventArgsTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.StreamWriteEventArgs")]
		public void Constructor_IfTheBufferParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new StreamWriteEventArgs(null, 0, 0, Mock.Of<Encoding>());
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "buffer")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.StreamWriteEventArgs")]
		public void Constructor_IfTheEncodingParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new StreamWriteEventArgs(new byte[0], 0, 0, null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "encoding")
					throw;
			}
		}

		[TestMethod]
		public void Constructor_ShouldSetTheBufferPropertyToACopyOfTheBufferParameterValue()
		{
			// ReSharper disable PossibleMultipleEnumeration
			byte[] bufferParameterValue = new byte[] {0, 1, 2, 3};
			IEnumerable<byte> bufferPropertyValue = new StreamWriteEventArgs(bufferParameterValue, 0, 0, Mock.Of<Encoding>()).Buffer;
			Assert.AreNotEqual(bufferParameterValue, bufferPropertyValue);
			Assert.AreEqual(bufferParameterValue.Length, bufferPropertyValue.Count());
			Assert.AreEqual(bufferParameterValue[0], bufferPropertyValue.ElementAt(0));
			Assert.AreEqual(bufferParameterValue[1], bufferPropertyValue.ElementAt(1));
			Assert.AreEqual(bufferParameterValue[2], bufferPropertyValue.ElementAt(2));
			Assert.AreEqual(bufferParameterValue[3], bufferPropertyValue.ElementAt(3));
			// ReSharper restore PossibleMultipleEnumeration
		}

		[TestMethod]
		public void Constructor_ShouldSetTheCountPropertyToTheCountParameterValue()
		{
			int count = DateTime.Now.Millisecond;
			Assert.AreEqual(count, new StreamWriteEventArgs(new byte[0], 0, count, Mock.Of<Encoding>()).Count);
		}

		[TestMethod]
		public void Constructor_ShouldSetTheEncodingPropertyToTheEncodingParameterValue()
		{
			Encoding encoding = Mock.Of<Encoding>();
			Assert.AreEqual(encoding, new StreamWriteEventArgs(new byte[0], 0, 0, encoding).Encoding);
		}

		[TestMethod]
		public void Constructor_ShouldSetTheOffsetPropertyToTheOffsetParameterValue()
		{
			int offset = DateTime.Now.Millisecond;
			Assert.AreEqual(offset, new StreamWriteEventArgs(new byte[0], offset, 0, Mock.Of<Encoding>()).Offset);
		}

		[TestMethod]
		public void Constructor_TheCountParameterValueCanBeLessThanZero()
		{
			int count = (DateTime.Now.Millisecond + 1)*-1;
			Assert.AreEqual(count, new StreamWriteEventArgs(new byte[0], 0, count, Mock.Of<Encoding>()).Count);
			Assert.IsTrue(count < 0);
		}

		[TestMethod]
		public void Constructor_TheOffsetParameterValueCanBeLessThanZero()
		{
			int offset = (DateTime.Now.Millisecond + 1)*-1;
			Assert.AreEqual(offset, new StreamWriteEventArgs(new byte[0], offset, 0, Mock.Of<Encoding>()).Offset);
			Assert.IsTrue(offset < 0);
		}

		#endregion
	}
}