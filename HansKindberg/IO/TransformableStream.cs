using System;
using System.IO;
using System.Linq;
using System.Text;

namespace HansKindberg.IO
{
	/// <summary>
	/// This code is originally from <see cref="http://www.west-wind.com/weblog/posts/2009/Nov/13/Capturing-and-Transforming-ASPNET-Output-with-ResponseFilter" />.
	/// </summary>
	public class TransformableStream : CapturableStream
	{
		#region Constructors

		public TransformableStream(Stream stream, Encoding encoding) : base(stream, encoding) {}

		#endregion

		#region Events

		public event EventHandler<StreamTransformingEventArgs> Transform;
		public event EventHandler<StreamWriteTransformingEventArgs> TransformWrite;

		#endregion

		#region Properties

		protected internal bool HasTransformEvents
		{
			get { return this.Transform != null; }
		}

		protected internal bool HasTransformWriteEvents
		{
			get { return this.TransformWrite != null; }
		}

		protected internal override bool IsCaptured
		{
			get { return this.HasCaptureEvents || this.HasTransformEvents; }
		}

		#endregion

		#region Methods

		public override void Close()
		{
			if(this.HasTransformEvents && !this.IsClosed)
			{
				StreamTransformingEventArgs streamTransformingEventArgs = new StreamTransformingEventArgs(this.CapturedStreamToString(), this.Encoding);

				this.OnTransform(streamTransformingEventArgs);

				if(streamTransformingEventArgs.Content != streamTransformingEventArgs.TransformedContent)
				{
					this.CapturedStream.SetLength(0); // Clear the captured stream.
					byte[] transformedBuffer = this.Encoding.GetBytes(streamTransformingEventArgs.TransformedContent ?? string.Empty);
					this.CapturedStream.Write(transformedBuffer, 0, transformedBuffer.Length);
				}

				byte[] buffer = this.CapturedStream.ToArray();
				this.Stream.Write(buffer, 0, buffer.Length);
				this.Stream.Flush();
			}

			base.Close();
		}

		public override void Flush()
		{
			if(!this.HasTransformEvents)
				base.Flush();
		}

		protected internal virtual void OnTransform(StreamTransformingEventArgs e)
		{
			if(this.HasTransformEvents)
				this.Transform(this, e);
		}

		protected internal virtual void OnTransformWrite(StreamWriteTransformingEventArgs e)
		{
			if(this.HasTransformWriteEvents)
				this.TransformWrite(this, e);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if(this.HasTransformWriteEvents)
			{
				StreamWriteTransformingEventArgs streamWriteTransformingEventArgs = new StreamWriteTransformingEventArgs(buffer, offset, count, this.Encoding);

				this.OnTransformWrite(streamWriteTransformingEventArgs);

				buffer = streamWriteTransformingEventArgs.TransformedBuffer.ToArray();
				offset = streamWriteTransformingEventArgs.TransformedOffset;
				count = streamWriteTransformingEventArgs.TransformedCount;
			}

			if(this.HasCaptureWriteEvents)
				this.OnCaptureWrite(new StreamWriteEventArgs(buffer, offset, count, this.Encoding));

			if(this.IsCaptured)
				this.CapturedStream.Write(buffer, offset, count);

			if(!this.HasTransformEvents)
				this.Stream.Write(buffer, offset, count);
		}

		#endregion
	}
}