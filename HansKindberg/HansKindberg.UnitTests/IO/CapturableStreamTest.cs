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
	public class CapturableStreamTest
	{
		#region Methods

		[TestMethod]
		public void CanRead_ShouldCallCanReadOfTheWrappedStream()
		{
			bool randomCanRead = DateTime.Now.Second%2 == 0;
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Setup(stream => stream.CanRead).Returns(randomCanRead);
			streamMock.Verify(stream => stream.CanRead, Times.Never());
			Assert.AreEqual(randomCanRead, new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).CanRead);
			streamMock.Verify(stream => stream.CanRead, Times.Once());
		}

		[TestMethod]
		public void CanSeek_ShouldCallCanSeekOfTheWrappedStream()
		{
			bool randomCanSeek = DateTime.Now.Second%2 == 0;
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Setup(stream => stream.CanSeek).Returns(randomCanSeek);
			streamMock.Verify(stream => stream.CanSeek, Times.Never());
			Assert.AreEqual(randomCanSeek, new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).CanSeek);
			streamMock.Verify(stream => stream.CanSeek, Times.Once());
		}

		[TestMethod]
		public void CanWrite_ShouldCallCanWriteOfTheWrappedStream()
		{
			bool randomCanWrite = DateTime.Now.Second%2 == 0;
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Setup(stream => stream.CanWrite).Returns(randomCanWrite);
			streamMock.Verify(stream => stream.CanWrite, Times.Never());
			Assert.AreEqual(randomCanWrite, new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).CanWrite);
			streamMock.Verify(stream => stream.CanWrite, Times.Once());
		}

		private static void CaptureEventHandler(object sender, StreamEventArgs e) {}
		private static void CaptureWriteEventHandler(object sender, StreamWriteEventArgs e) {}

		[TestMethod]
		public void CapturedStreamToString_ShouldReturnTheBytesInTheCapturedStreamDecodedToAString()
		{
			const string expectedContent = "Test";
			Encoding encoding = Encoding.UTF8;
			byte[] buffer = encoding.GetBytes(expectedContent);

			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), encoding);
			capturableStream.CapturedStream.Write(buffer, 0, buffer.Length);
			Assert.AreEqual(expectedContent, capturableStream.CapturedStreamToString());
		}

		[TestMethod]
		public void CapturedStream_ShouldNotReturnNullByDefault()
		{
			Assert.IsNotNull(new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>()).CapturedStream);
		}

		[TestMethod]
		public void Close_IfHasCaptureEventsAndIsClosedIsFalse_ShouldRaiseCaptureEvents()
		{
			const string expectedContent = "Test";
			Encoding expectedEncoding = Encoding.UTF8;
			byte[] buffer = expectedEncoding.GetBytes(expectedContent);

			bool captureRaised = false;
			string actualContent = null;
			Encoding actualEncoding = null;
			object actualSender = null;

			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), expectedEncoding);
			capturableStream.Capture += delegate(object sender, StreamEventArgs e)
			{
				captureRaised = true;
				actualSender = sender;
				actualContent = e.Content;
				actualEncoding = e.Encoding;
			};

			Assert.IsFalse(captureRaised);

			capturableStream.CapturedStream.Write(buffer, 0, buffer.Length);
			capturableStream.Close();

			Assert.IsTrue(captureRaised);
			Assert.AreEqual(capturableStream, actualSender);
			Assert.AreEqual(expectedContent, actualContent);
			Assert.AreEqual(expectedEncoding, actualEncoding);
		}

		[TestMethod]
		public void Close_IfHasCaptureEventsAndIsClosedIsTrue_ShouldNotRaiseCaptureEvents()
		{
			bool captureRaised = false;
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			capturableStream.Capture += (sender, e) => { captureRaised = true; };
			Assert.IsFalse(captureRaised);
			capturableStream.IsClosed = true;
			capturableStream.Close();
			Assert.IsFalse(captureRaised);
		}

		[TestMethod]
		public void Close_IfNotHasCaptureEvents_ShouldNotRaiseCaptureEvents()
		{
			Mock<CapturableStream> capturableStreamMock = new Mock<CapturableStream>(new object[] {Mock.Of<Stream>(), Mock.Of<Encoding>()}) {CallBase = true};
			capturableStreamMock.Verify(capturableStream => capturableStream.OnCapture(It.IsAny<StreamEventArgs>()), Times.Never());
			capturableStreamMock.Object.IsClosed = DateTime.Now.Second%2 == 0; // Random boolean.
			capturableStreamMock.Object.Close();
			capturableStreamMock.Verify(capturableStream => capturableStream.OnCapture(It.IsAny<StreamEventArgs>()), Times.Never());
		}

		[TestMethod]
		public void Close_ShouldCallCloseOfTheCapturedStream()
		{
			Mock<MemoryStream> memoryStreamMock = new Mock<MemoryStream>();
			memoryStreamMock.Verify(memoryStream => memoryStream.Close(), Times.Never());
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			// ReSharper disable PossibleNullReferenceException
			typeof(CapturableStream).GetField("_capturedStream", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(capturableStream, memoryStreamMock.Object);
			// ReSharper restore PossibleNullReferenceException
			capturableStream.Close();
			memoryStreamMock.Verify(memoryStream => memoryStream.Close(), Times.Once());
		}

		[TestMethod]
		public void Close_ShouldCallCloseOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Close(), Times.Never());
			new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).Close();
			streamMock.Verify(stream => stream.Close(), Times.Once());
		}

		[TestMethod]
		public void Close_ShouldSetIsClosedToTrue()
		{
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			capturableStream.Close();
			Assert.IsTrue(capturableStream.IsClosed);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.CapturableStream")]
		public void Constructor_IfTheEncodingParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new CapturableStream(Mock.Of<Stream>(), null);
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
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.IO.CapturableStream")]
		public void Constructor_IfTheStreamParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new CapturableStream(null, Mock.Of<Encoding>());
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
			Assert.AreEqual(encoding, new CapturableStream(Mock.Of<Stream>(), encoding).Encoding);
		}

		[TestMethod]
		public void Constructor_ShouldSetTheStreamPropertyToTheStreamParameterValue()
		{
			Stream stream = Mock.Of<Stream>();
			Assert.AreEqual(stream, new CapturableStream(stream, Mock.Of<Encoding>()).Stream);
		}

		[TestMethod]
		public void Flush_ShouldCallFlushOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Flush(), Times.Never());
			new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).Flush();
			streamMock.Verify(stream => stream.Flush(), Times.Once());
		}

		[TestMethod]
		public void HasCaptureEvents_IfCaptureEventsAreNotRegistered_ShouldReturnFalse()
		{
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(capturableStream.HasCaptureEvents);

			capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(capturableStream.HasCaptureEvents);
			capturableStream.Capture += CaptureEventHandler;
			capturableStream.Capture -= CaptureEventHandler;
			Assert.IsFalse(capturableStream.HasCaptureEvents);
		}

		[TestMethod]
		public void HasCaptureEvents_IfCaptureEventsAreRegistered_ShouldReturnTrue()
		{
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			capturableStream.Capture += (sender, e) => { };
			Assert.IsTrue(capturableStream.HasCaptureEvents);
		}

		[TestMethod]
		public void HasCaptureWriteEvents_IfCaptureWriteEventsAreNotRegistered_ShouldReturnFalse()
		{
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(capturableStream.HasCaptureWriteEvents);

			capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(capturableStream.HasCaptureWriteEvents);
			capturableStream.CaptureWrite += CaptureWriteEventHandler;
			capturableStream.CaptureWrite -= CaptureWriteEventHandler;
			Assert.IsFalse(capturableStream.HasCaptureWriteEvents);
		}

		[TestMethod]
		public void HasCaptureWriteEvents_IfCaptureWriteEventsAreRegistered_ShouldReturnTrue()
		{
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			capturableStream.CaptureWrite += (sender, e) => { };
			Assert.IsTrue(capturableStream.HasCaptureWriteEvents);
		}

		[TestMethod]
		public void IsCaptured_ShouldReturnHasCaptureEvents()
		{
			bool hasCaptureEvents = DateTime.Now.Second%2 == 0;
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());

			if(hasCaptureEvents)
				capturableStream.Capture += (sender, e) => { };

			Assert.AreEqual(hasCaptureEvents, capturableStream.IsCaptured);
		}

		[TestMethod]
		public void IsClosed_Get_ShouldReturnFalseByDefault()
		{
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(capturableStream.IsClosed);
		}

		[TestMethod]
		public void Length_ShouldCallLengthOfTheWrappedStream()
		{
			long randomLength = DateTime.Now.Millisecond;
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Setup(stream => stream.Length).Returns(randomLength);
			streamMock.Verify(stream => stream.Length, Times.Never());
			Assert.AreEqual(randomLength, new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).Length);
			streamMock.Verify(stream => stream.Length, Times.Once());
		}

		[TestMethod]
		public void OnCaptureWrite_ShouldRaiseCaptureWriteEvents()
		{
			int numberOfEventsRaised = 0;
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			capturableStream.CaptureWrite += (sender, e) => numberOfEventsRaised++;
			capturableStream.CaptureWrite += (sender, e) => numberOfEventsRaised++;
			capturableStream.CaptureWrite += (sender, e) => numberOfEventsRaised++;
			Assert.AreEqual(0, numberOfEventsRaised);
			capturableStream.OnCaptureWrite(new StreamWriteEventArgs(new byte[0], It.IsAny<int>(), It.IsAny<int>(), Mock.Of<Encoding>()));
			Assert.AreEqual(3, numberOfEventsRaised);
		}

		[TestMethod]
		public void OnCapture_ShouldRaiseCaptureEvents()
		{
			int numberOfEventsRaised = 0;
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			capturableStream.Capture += (sender, e) => numberOfEventsRaised++;
			capturableStream.Capture += (sender, e) => numberOfEventsRaised++;
			capturableStream.Capture += (sender, e) => numberOfEventsRaised++;
			Assert.AreEqual(0, numberOfEventsRaised);
			capturableStream.OnCapture(new StreamEventArgs(null, Mock.Of<Encoding>()));
			Assert.AreEqual(3, numberOfEventsRaised);
		}

		[TestMethod]
		public void Position_Get_ShouldCallPositionGetOfTheWrappedStream()
		{
			long randomPosition = DateTime.Now.Millisecond;
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Setup(stream => stream.Position).Returns(randomPosition);
			streamMock.VerifyGet(stream => stream.Position, Times.Never());
			Assert.AreEqual(randomPosition, new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).Position);
			streamMock.VerifyGet(stream => stream.Position, Times.Once());
		}

		[TestMethod]
		public void Position_Set_ShouldCallPositionSetOfTheWrappedStream()
		{
			long randomPosition = DateTime.Now.Millisecond;
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.SetupProperty(stream => stream.Position, 0);
			streamMock.VerifySet(stream => stream.Position = It.IsAny<long>(), Times.Never());
			CapturableStream capturableStream = new CapturableStream(streamMock.Object, Mock.Of<Encoding>())
				{
					Position = randomPosition
				};
			Assert.AreEqual(randomPosition, capturableStream.Position);
			streamMock.VerifySet(stream => stream.Position = It.IsAny<long>(), Times.Once());
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public void Prerequisite_MemoryStream_Close_CallingCloseMultipleTimes_ShouldNotThrowAnException()
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] buffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
			memoryStream.Write(buffer, 0, 10);
			memoryStream.Close();
			memoryStream.Close();

			memoryStream = new MemoryStream();
			memoryStream.Close();
			memoryStream.Close();
		}

		[TestMethod]
		public void Prerequisite_MemoryStream_ToArray_IfTheLengthIsSetToZero_ShouldReturnAnEmptyArray()
		{
			using(MemoryStream memoryStream = new MemoryStream())
			{
				byte[] buffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
				memoryStream.Write(buffer, 0, 10);
				Assert.AreEqual(10, memoryStream.ToArray().Length);
				memoryStream.SetLength(0);
				Assert.IsFalse(memoryStream.ToArray().Any());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectDisposedException))]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Prerequisite_MemoryStream_Write_IfCloseHaveBeenCalled_ShouldThrowAnObjectDisposedException()
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] buffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
			memoryStream.Write(buffer, 0, 10);
			memoryStream.Close();
			memoryStream.Write(buffer, 0, 10);
		}

		[TestMethod]
		public void Prerequisite_MemoryStream_Write_Test()
		{
			using(MemoryStream memoryStream = new MemoryStream())
			{
				byte[] buffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
				memoryStream.Write(buffer, 2, 4);

				byte[] toArray = memoryStream.ToArray();
				Assert.AreEqual(4, toArray.Length);
				Assert.AreEqual(2, toArray[0]);
				Assert.AreEqual(3, toArray[1]);
				Assert.AreEqual(4, toArray[2]);
				Assert.AreEqual(5, toArray[3]);
			}
		}

		[TestMethod]
		public void Read_ShouldCallReadOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
			new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).Read(new byte[DateTime.Now.Second], DateTime.Now.Second, DateTime.Now.Millisecond);
			streamMock.Verify(stream => stream.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
		}

		[TestMethod]
		public void Seek_ShouldCallSeekOfTheWrappedStream()
		{
			SeekOrigin randomSeekOrigin;

			switch(DateTime.Now.Second%3)
			{
				case 1:
					randomSeekOrigin = SeekOrigin.Current;
					break;
				case 2:
					randomSeekOrigin = SeekOrigin.End;
					break;
				default:
					randomSeekOrigin = SeekOrigin.Begin;
					break;
			}

			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Seek(It.IsAny<long>(), It.IsAny<SeekOrigin>()), Times.Never());
			new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).Seek(DateTime.Now.Millisecond, randomSeekOrigin);
			streamMock.Verify(stream => stream.Seek(It.IsAny<long>(), It.IsAny<SeekOrigin>()), Times.Once());
		}

		[TestMethod]
		public void SetLength_ShouldCallSetLengthOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.SetLength(It.IsAny<long>()), Times.Never());
			new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).SetLength(DateTime.Now.Millisecond);
			streamMock.Verify(stream => stream.SetLength(It.IsAny<long>()), Times.Once());
		}

		[TestMethod]
		public void Write_IfCaptured_ShouldCallWriteOfTheCapturedStream()
		{
			byte[] buffer = new byte[4] {0, 1, 2, 3};
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			capturableStream.Capture += (sender, e) => { };
			Assert.IsFalse(capturableStream.CapturedStream.ToArray().Any());
			capturableStream.Write(buffer, 0, buffer.Length);
			Assert.AreEqual(4, capturableStream.CapturedStream.ToArray().Length);
			Assert.AreEqual(0, capturableStream.CapturedStream.ToArray()[0]);
			Assert.AreEqual(1, capturableStream.CapturedStream.ToArray()[1]);
			Assert.AreEqual(2, capturableStream.CapturedStream.ToArray()[2]);
			Assert.AreEqual(3, capturableStream.CapturedStream.ToArray()[3]);
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

			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), expectedEncoding);
			capturableStream.CaptureWrite += delegate(object sender, StreamWriteEventArgs e)
			{
				captureWriteRaised = true;
				actualSender = sender;
				actualBuffer = e.Buffer;
				actualOffset = e.Offset;
				actualCount = e.Count;
				actualEncoding = e.Encoding;
			};

			Assert.IsFalse(captureWriteRaised);

			capturableStream.Write(expectedBuffer, expectedOffset, expectedCount);

			Assert.IsTrue(captureWriteRaised);
			Assert.AreEqual(capturableStream, actualSender);
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
		public void Write_IfNotCaptured_ShouldNotCallWriteOfTheCapturedStream()
		{
			byte[] buffer = new byte[4] {0, 1, 2, 3};
			CapturableStream capturableStream = new CapturableStream(Mock.Of<Stream>(), Mock.Of<Encoding>());
			Assert.IsFalse(capturableStream.CapturedStream.ToArray().Any());
			capturableStream.Write(buffer, 0, buffer.Length);
			Assert.IsFalse(capturableStream.CapturedStream.ToArray().Any());
		}

		[TestMethod]
		public void Write_IfNotHasCaptureWriteEvents_ShouldNotRaiseCaptureWriteEvents()
		{
			Mock<CapturableStream> capturableStreamMock = new Mock<CapturableStream>(new object[] {Mock.Of<Stream>(), Mock.Of<Encoding>()}) {CallBase = true};
			capturableStreamMock.Verify(capturableStream => capturableStream.OnCaptureWrite(It.IsAny<StreamWriteEventArgs>()), Times.Never());
			capturableStreamMock.Object.Write(new byte[0], It.IsAny<int>(), It.IsAny<int>());
			capturableStreamMock.Verify(capturableStream => capturableStream.OnCaptureWrite(It.IsAny<StreamWriteEventArgs>()), Times.Never());
		}

		[TestMethod]
		public void Write_ShouldCallWriteOfTheWrappedStream()
		{
			Mock<Stream> streamMock = new Mock<Stream>();
			streamMock.Verify(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
			new CapturableStream(streamMock.Object, Mock.Of<Encoding>()).Write(new byte[DateTime.Now.Second], DateTime.Now.Second, DateTime.Now.Millisecond);
			streamMock.Verify(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
		}

		#endregion
	}
}