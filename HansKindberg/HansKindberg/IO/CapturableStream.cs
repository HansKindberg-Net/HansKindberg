using System;
using System.IO;
using System.Text;

namespace HansKindberg.IO
{
	/// <summary>
	/// This code is originally from <see cref="http://www.west-wind.com/weblog/posts/2009/Nov/13/Capturing-and-Transforming-ASPNET-Output-with-ResponseFilter" />.
	/// </summary>
	public class CapturableStream : Stream
	{
		#region Fields

		private readonly MemoryStream _capturedStream = new MemoryStream();
		private readonly Encoding _encoding;
		private readonly Stream _stream;

		#endregion

		#region Constructors

		public CapturableStream(Stream stream, Encoding encoding)
		{
			if(stream == null)
				throw new ArgumentNullException("stream");

			if(encoding == null)
				throw new ArgumentNullException("encoding");

			this._encoding = encoding;
			this._stream = stream;
		}

		#endregion

		#region Events

		public event EventHandler<StreamEventArgs> Capture;
		public event EventHandler<StreamWriteEventArgs> CaptureWrite;

		#endregion

		#region Properties

		public override bool CanRead
		{
			get { return this.Stream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return this.Stream.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return this.Stream.CanWrite; }
		}

		protected internal virtual MemoryStream CapturedStream
		{
			get { return this._capturedStream; }
		}

		protected internal virtual Encoding Encoding
		{
			get { return this._encoding; }
		}

		protected internal bool HasCaptureEvents
		{
			get { return this.Capture != null; }
		}

		protected internal bool HasCaptureWriteEvents
		{
			get { return this.CaptureWrite != null; }
		}

		protected internal virtual bool IsCaptured
		{
			get { return this.HasCaptureEvents; }
		}

		protected internal virtual bool IsClosed { get; set; }

		public override long Length
		{
			get { return this.Stream.Length; }
		}

		public override long Position
		{
			get { return this.Stream.Position; }
			set { this.Stream.Position = value; }
		}

		protected internal virtual Stream Stream
		{
			get { return this._stream; }
		}

		#endregion

		#region Methods

		protected internal virtual string CapturedStreamToString()
		{
			return this.Encoding.GetString(this.CapturedStream.ToArray());
		}

		public override void Close()
		{
			if(this.HasCaptureEvents && !this.IsClosed)
				this.OnCapture(new StreamEventArgs(this.CapturedStreamToString(), this.Encoding));

			this.IsClosed = true;
			this.CapturedStream.Close();
			this.Stream.Close();
		}

		public override void Flush()
		{
			this.Stream.Flush();
		}

		protected internal virtual void OnCapture(StreamEventArgs e)
		{
			if(this.HasCaptureEvents)
				this.Capture(this, e);
		}

		protected internal virtual void OnCaptureWrite(StreamWriteEventArgs e)
		{
			if(this.HasCaptureWriteEvents)
				this.CaptureWrite(this, e);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.Stream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.Stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			this.Stream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if(this.HasCaptureWriteEvents)
				this.OnCaptureWrite(new StreamWriteEventArgs(buffer, offset, count, this.Encoding));

			if(this.IsCaptured)
				this.CapturedStream.Write(buffer, offset, count);

			this.Stream.Write(buffer, offset, count);
		}

		#endregion
	}
}