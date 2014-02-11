using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HansKindberg.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.IO
{
	[TestClass]
	public class TransformableStreamTest
	{
		#region Methods

		[TestMethod]
		public void Close_IfHasTransformEventsAndIsClosedIsFalse_ShouldRaiseTransformEvents()
		{
			const string expectedContent = "Test";
			Encoding expectedEncoding = Encoding.UTF8;
			const string expectedTransformedContent = expectedContent;
			byte[] buffer = expectedEncoding.GetBytes(expectedContent);
			bool transformRaised = false;
			string actualContent = null;
			Encoding actualEncoding = null;
			string actualTransformedContent = null;
			object actualSender = null;
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), expectedEncoding);
			transformableStream.Transform += delegate(object sender, StreamTransformingEventArgs e)
			{
				transformRaised = true;
				actualSender = sender;
				actualContent = e.Content;
				actualEncoding = e.Encoding;
				actualTransformedContent = e.TransformedContent;
			};
			Assert.IsFalse(transformRaised);
			transformableStream.CapturedStream.Write(buffer, 0, buffer.Length);
			transformableStream.Close();
			Assert.IsTrue(transformRaised);
			Assert.AreEqual(transformableStream, actualSender);
			Assert.AreEqual(expectedContent, actualContent);
			Assert.AreEqual(expectedEncoding, actualEncoding);
			Assert.AreEqual(expectedTransformedContent, actualTransformedContent);
		}

		[TestMethod]
		public void Close_IfHasTransformEventsAndIsClosedIsTrue_ShouldNotRaiseTransformEvents()
		{
			bool transformRaised = false;
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			transformableStream.Transform += (sender, e) => { transformRaised = true; };
			Assert.IsFalse(transformRaised);
			transformableStream.IsClosed = true;
			transformableStream.Close();
			Assert.IsFalse(transformRaised);
		}

		[TestMethod]
		public void Close_IfNotHasTransformEvents_ShouldNotRaiseTransformEvents()
		{
			Mock<TransformableStream> transformableStreamMock = new Mock<TransformableStream>(new object[] {Mock.Of<Stream>(), Mock.Of<Encoding>()}) {CallBase = true};
			transformableStreamMock.Verify(transformableStream => transformableStream.OnTransform(It.IsAny<StreamTransformingEventArgs>()), Times.Never());
			transformableStreamMock.Object.IsClosed = DateTime.Now.Second%2 == 0; // Random boolean.
			transformableStreamMock.Object.Close();
			transformableStreamMock.Verify(transformableStream => transformableStream.OnTransform(It.IsAny<StreamTransformingEventArgs>()), Times.Never());
		}

		[TestMethod]
		public void Close_ShouldCallCloseOfTheCapturedStream()
		{
			Mock<MemoryStream> memoryStreamMock = new Mock<MemoryStream>();
			memoryStreamMock.Verify(memoryStream => memoryStream.Close(), Times.Never());
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			// ReSharper disable PossibleNullReferenceException
			typeof(CapturableStream).GetField("_capturedStream", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(transformableStream, memoryStreamMock.Object);
			// ReSharper restore PossibleNullReferenceException
			transformableStream.Close();
			memoryStreamMock.Verify(memoryStream => memoryStream.Close(), Times.Once());
		}

		[TestMethod]
		public void Close_ShouldCallCloseOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Close(), Times.Never());
			new TransformableStream(streamMock.Object, Mock.Of<Encoding>()).Close();
			streamMock.Verify(stream => stream.Close(), Times.Once());
		}

		[TestMethod]
		public void Close_ShouldSetIsClosedToTrue()
		{
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			transformableStream.Close();
			Assert.IsTrue(transformableStream.IsClosed);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.TransformableStream")]
		public void Constructor_IfTheEncodingParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new TransformableStream(Mock.Of<Stream>(), null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "encoding")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.TransformableStream")]
		public void Constructor_IfTheStreamParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new TransformableStream(null, Mock.Of<Encoding>());
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "stream")
					throw;
			}
		}

		[TestMethod]
		public void Constructor_ShouldSetTheEncodingPropertyToTheEncodingParameterValue()
		{
			Encoding encoding = Mock.Of<Encoding>();
			Assert.AreEqual(encoding, new TransformableStream(Mock.Of<Stream>(), encoding).Encoding);
		}

		[TestMethod]
		public void Constructor_ShouldSetTheStreamPropertyToTheStreamParameterValue()
		{
			Stream stream = Mock.Of<Stream>();
			Assert.AreEqual(stream, new TransformableStream(stream, Mock.Of<Encoding>()).Stream);
		}

		[TestMethod]
		public void Flush_IfHasTransformEvents_ShouldNotCallFlushOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Flush(), Times.Never());
			TransformableStream transformableStream = new TransformableStream(streamMock.Object, Mock.Of<Encoding>());
			transformableStream.Transform += (sender, e) => { };
			transformableStream.Flush();
			streamMock.Verify(stream => stream.Flush(), Times.Never());
		}

		[TestMethod]
		public void Flush_IfNotHasTransformEvents_ShouldCallFlushOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Flush(), Times.Never());
			new TransformableStream(streamMock.Object, Mock.Of<Encoding>()).Flush();
			streamMock.Verify(stream => stream.Flush(), Times.Once());
		}

		[TestMethod]
		public void HasTransformEvents_IfTransformEventsAreNotRegistered_ShouldReturnFalse()
		{
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(transformableStream.HasTransformEvents);
			transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(transformableStream.HasTransformEvents);
			transformableStream.Transform += TransformEventHandler;
			transformableStream.Transform -= TransformEventHandler;
			Assert.IsFalse(transformableStream.HasTransformEvents);
		}

		[TestMethod]
		public void HasTransformEvents_IfTransformEventsAreRegistered_ShouldReturnTrue()
		{
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			transformableStream.Transform += (sender, e) => { };
			Assert.IsTrue(transformableStream.HasTransformEvents);
		}

		[TestMethod]
		public void HasTransformWriteEvents_IfTransformWriteEventsAreNotRegistered_ShouldReturnFalse()
		{
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(transformableStream.HasTransformWriteEvents);
			transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(transformableStream.HasTransformWriteEvents);
			transformableStream.TransformWrite += TransformWriteEventHandler;
			transformableStream.TransformWrite -= TransformWriteEventHandler;
			Assert.IsFalse(transformableStream.HasTransformWriteEvents);
		}

		[TestMethod]
		public void HasTransformWriteEvents_IfTransformWriteEventsAreRegistered_ShouldReturnTrue()
		{
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			transformableStream.TransformWrite += (sender, e) => { };
			Assert.IsTrue(transformableStream.HasTransformWriteEvents);
		}

		[TestMethod]
		public void IsCaptured_ShouldReturnHasCaptureEventsOrHasTransformEvents()
		{
			bool hasCaptureEvents = DateTime.Now.Second%2 == 0;
			bool hasTransformEvents = DateTime.Now.Millisecond%2 == 0;
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			if(hasCaptureEvents)
				transformableStream.Capture += (sender, e) => { };
			if(hasTransformEvents)
				transformableStream.Transform += (sender, e) => { };
			Assert.AreEqual(hasCaptureEvents || hasTransformEvents, transformableStream.IsCaptured);
		}

		[TestMethod]
		public void OnTransformWrite_ShouldRaiseTransformWriteEvents()
		{
			int numberOfEventsRaised = 0;
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			transformableStream.TransformWrite += (sender, e) => numberOfEventsRaised++;
			transformableStream.TransformWrite += (sender, e) => numberOfEventsRaised++;
			transformableStream.TransformWrite += (sender, e) => numberOfEventsRaised++;
			Assert.AreEqual(0, numberOfEventsRaised);
			transformableStream.OnTransformWrite(new StreamWriteTransformingEventArgs(new byte[0], It.IsAny<int>(), It.IsAny<int>(), Mock.Of<Encoding>()));
			Assert.AreEqual(3, numberOfEventsRaised);
		}

		[TestMethod]
		public void OnTransform_ShouldRaiseTransformEvents()
		{
			int numberOfEventsRaised = 0;
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			transformableStream.Transform += (sender, e) => numberOfEventsRaised++;
			transformableStream.Transform += (sender, e) => numberOfEventsRaised++;
			transformableStream.Transform += (sender, e) => numberOfEventsRaised++;
			Assert.AreEqual(0, numberOfEventsRaised);
			transformableStream.OnTransform(new StreamTransformingEventArgs(null, Mock.Of<Encoding>()));
			Assert.AreEqual(3, numberOfEventsRaised);
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Prerequisite_MemoryStream_ToArray_IfCloseHaveBeenCalled_ShouldReturnTheBytesOfTheMemoryStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] buffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
			memoryStream.Write(buffer, 0, 10);
			memoryStream.Close();
			byte[] toArray = memoryStream.ToArray();
			Assert.AreEqual(10, toArray.Length);
			Assert.AreEqual(0, toArray[0]);
			Assert.AreEqual(1, toArray[1]);
			Assert.AreEqual(2, toArray[2]);
			Assert.AreEqual(3, toArray[3]);
			Assert.AreEqual(4, toArray[4]);
			Assert.AreEqual(5, toArray[5]);
			Assert.AreEqual(6, toArray[6]);
			Assert.AreEqual(7, toArray[7]);
			Assert.AreEqual(8, toArray[8]);
			Assert.AreEqual(9, toArray[9]);
		}

		private static void TransformEventHandler(object sender, StreamTransformingEventArgs e) {}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void TransformShouldHandleSettingContentToNull()
		{
			const string originalContent = "Original content";
			//const string transformedContent = null;
			Encoding encoding = Encoding.UTF8;
			byte[] originalBuffer = encoding.GetBytes(originalContent);
			string capturedContent = null;

			MemoryStream memoryStream = new MemoryStream();
			TransformableStream transformableStream = new TransformableStream(memoryStream, encoding);
			transformableStream.Transform += delegate(object sender, StreamTransformingEventArgs e) { e.TransformedContent = null; };
			transformableStream.Capture += delegate(object sender, StreamEventArgs e) { capturedContent = e.Content; };
			for(int i = 0; i < originalBuffer.Length; i++)
			{
				transformableStream.Write(originalBuffer, i, 1);
			}
			Assert.AreEqual(0, memoryStream.ToArray().Length);
			Assert.AreEqual(originalBuffer.Length, transformableStream.CapturedStream.ToArray().Length);
			transformableStream.Flush();
			Assert.AreEqual(0, memoryStream.ToArray().Length);
			Assert.AreEqual(originalBuffer.Length, transformableStream.CapturedStream.ToArray().Length);
			transformableStream.Close();
			Assert.AreEqual(string.Empty, encoding.GetString(memoryStream.ToArray()));
			Assert.AreEqual(string.Empty, capturedContent);
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void TransformTest()
		{
			const string originalContent = "Original content";
			const string transformedContent = "Transformed content";
			Encoding encoding = Encoding.UTF8;
			byte[] originalBuffer = encoding.GetBytes(originalContent);
			string capturedContent = null;

			MemoryStream memoryStream = new MemoryStream();
			TransformableStream transformableStream = new TransformableStream(memoryStream, encoding);
			transformableStream.Transform += delegate(object sender, StreamTransformingEventArgs e) { e.TransformedContent = transformedContent; };
			transformableStream.Capture += delegate(object sender, StreamEventArgs e) { capturedContent = e.Content; };
			for(int i = 0; i < originalBuffer.Length; i++)
			{
				transformableStream.Write(originalBuffer, i, 1);
			}
			Assert.AreEqual(0, memoryStream.ToArray().Length);
			Assert.AreEqual(originalBuffer.Length, transformableStream.CapturedStream.ToArray().Length);
			transformableStream.Flush();
			Assert.AreEqual(0, memoryStream.ToArray().Length);
			Assert.AreEqual(originalBuffer.Length, transformableStream.CapturedStream.ToArray().Length);
			transformableStream.Close();
			Assert.AreEqual(transformedContent, encoding.GetString(memoryStream.ToArray()));
			Assert.AreEqual(transformedContent, capturedContent);
		}

		private static void TransformWriteEventHandler(object sender, StreamWriteTransformingEventArgs e) {}

		[TestMethod]
		public void TransformWriteTest()
		{
			byte[] capturedBuffer = null;
			int? capturedOffset = null;
			int? capturedCount = null;

			Encoding encoding = Mock.Of<Encoding>();
			using(MemoryStream memoryStream = new MemoryStream())
			{
				TransformableStream transformableStream = new TransformableStream(memoryStream, encoding);
				transformableStream.TransformWrite += delegate(object sender, StreamWriteTransformingEventArgs e)
				{
					e.TransformedBuffer.Clear();
					e.TransformedBuffer.Add(10);
					e.TransformedBuffer.Add(11);
					e.TransformedBuffer.Add(12);
					e.TransformedBuffer.Add(13);
					e.TransformedOffset = 1;
					e.TransformedCount = 2;
				};
				transformableStream.CaptureWrite += delegate(object sender, StreamWriteEventArgs e)
				{
					capturedBuffer = e.Buffer.ToArray();
					capturedOffset = e.Offset;
					capturedCount = e.Count;
				};
				transformableStream.Write(new byte[] {1, 2, 3, 4, 5, 6, 7, 8}, 2, 4);
				byte[] toArray = memoryStream.ToArray();
				Assert.AreEqual(2, toArray.Length);
				Assert.AreEqual(11, toArray[0]);
				Assert.AreEqual(12, toArray[1]);
				Assert.AreEqual(4, capturedBuffer.Length);
				Assert.AreEqual(10, capturedBuffer[0]);
				Assert.AreEqual(11, capturedBuffer[1]);
				Assert.AreEqual(12, capturedBuffer[2]);
				Assert.AreEqual(13, capturedBuffer[3]);
				Assert.AreEqual(1, capturedOffset);
				Assert.AreEqual(2, capturedCount);
			}
		}

		[TestMethod]
		public void Write_IfCaptured_ShouldCallWriteOfTheCapturedStream()
		{
			byte[] buffer = new byte[4] {0, 1, 2, 3};
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			transformableStream.Capture += (sender, e) => { };
			Assert.IsFalse(transformableStream.CapturedStream.ToArray().Any());
			transformableStream.Write(buffer, 0, buffer.Length);
			Assert.AreEqual(4, transformableStream.CapturedStream.ToArray().Length);
			Assert.AreEqual(0, transformableStream.CapturedStream.ToArray()[0]);
			Assert.AreEqual(1, transformableStream.CapturedStream.ToArray()[1]);
			Assert.AreEqual(2, transformableStream.CapturedStream.ToArray()[2]);
			Assert.AreEqual(3, transformableStream.CapturedStream.ToArray()[3]);
		}

		[TestMethod]
		public void Write_IfHasCaptureWriteEvents_ShouldRaiseCaptureWriteEvents()
		{
			byte[] expectedBuffer = new byte[] {0, 1, 2, 3};
			const int expectedOffset = 1;
			const int expectedCount = 2;
			Encoding expectedEncoding = Encoding.UTF8;
			bool captureWriteRaised = false;
			IEnumerable<byte> actualBuffer = null;
			int? actualOffset = null;
			int? actualCount = null;
			Encoding actualEncoding = null;
			object actualSender = null;
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), expectedEncoding);
			transformableStream.CaptureWrite += delegate(object sender, StreamWriteEventArgs e)
			{
				captureWriteRaised = true;
				actualSender = sender;
				actualBuffer = e.Buffer;
				actualOffset = e.Offset;
				actualCount = e.Count;
				actualEncoding = e.Encoding;
			};
			Assert.IsFalse(captureWriteRaised);
			transformableStream.Write(expectedBuffer, expectedOffset, expectedCount);
			Assert.IsTrue(captureWriteRaised);
			Assert.AreEqual(transformableStream, actualSender);
			// ReSharper disable PossibleMultipleEnumeration
			Assert.AreEqual(expectedBuffer.Length, actualBuffer.Count());
			Assert.AreEqual(expectedBuffer[0], actualBuffer.ElementAt(0));
			Assert.AreEqual(expectedBuffer[1], actualBuffer.ElementAt(1));
			Assert.AreEqual(expectedBuffer[2], actualBuffer.ElementAt(2));
			Assert.AreEqual(expectedBuffer[3], actualBuffer.ElementAt(3));
			// ReSharper restore PossibleMultipleEnumeration
			Assert.AreEqual(expectedOffset, actualOffset);
			Assert.AreEqual(expectedCount, actualCount);
			Assert.AreEqual(expectedEncoding, actualEncoding);
		}

		[TestMethod]
		public void Write_IfHasTransformEvents_ShouldNotCallWriteOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
			TransformableStream transformableStream = new TransformableStream(streamMock.Object, Mock.Of<Encoding>());
			transformableStream.Transform += (sender, args) => { };
			byte[] buffer = new byte[DateTime.Now.Millisecond];
			transformableStream.Write(buffer, 0, buffer.Length);
			streamMock.Verify(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
		}

		[TestMethod]
		public void Write_IfNotCaptured_ShouldNotCallWriteOfTheCapturedStream()
		{
			byte[] buffer = new byte[4] {0, 1, 2, 3};
			TransformableStream transformableStream = new TransformableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(transformableStream.CapturedStream.ToArray().Any());
			transformableStream.Write(buffer, 0, buffer.Length);
			Assert.IsFalse(transformableStream.CapturedStream.ToArray().Any());
		}

		[TestMethod]
		public void Write_IfNotHasCaptureWriteEvents_ShouldNotRaiseCaptureWriteEvents()
		{
			Mock<TransformableStream> transformableStreamMock = new Mock<TransformableStream>(new object[] {Mock.Of<Stream>(), Mock.Of<Encoding>()}) {CallBase = true};
			transformableStreamMock.Verify(transformableStream => transformableStream.OnCaptureWrite(It.IsAny<StreamWriteEventArgs>()), Times.Never());
			transformableStreamMock.Object.Write(new byte[0], It.IsAny<int>(), It.IsAny<int>());
			transformableStreamMock.Verify(transformableStream => transformableStream.OnCaptureWrite(It.IsAny<StreamWriteEventArgs>()), Times.Never());
		}

		[TestMethod]
		public void Write_IfNotHasTransformEvents_ShouldCallWriteOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
			byte[] buffer = new byte[DateTime.Now.Millisecond];
			new TransformableStream(streamMock.Object, Mock.Of<Encoding>()).Write(buffer, 0, buffer.Length);
			streamMock.Verify(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
		}

		[TestMethod]
		public void Write_IfNotHasTransformWriteEvents_ShouldNotRaiseTransformWriteEvents()
		{
			Mock<TransformableStream> transformableStreamMock = new Mock<TransformableStream>(new object[] {Mock.Of<Stream>(), Mock.Of<Encoding>()}) {CallBase = true};
			transformableStreamMock.Verify(transformableStream => transformableStream.OnTransformWrite(It.IsAny<StreamWriteTransformingEventArgs>()), Times.Never());
			transformableStreamMock.Object.Write(new byte[0], It.IsAny<int>(), It.IsAny<int>());
			transformableStreamMock.Verify(transformableStream => transformableStream.OnTransformWrite(It.IsAny<StreamWriteTransformingEventArgs>()), Times.Never());
		}

		#endregion
	}
}