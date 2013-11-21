using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HansKindberg.IO
{
	public class StreamWriteTransformingEventArgs : StreamWriteEventArgs
	{
		#region Fields

		private readonly IList<byte> _transformedBuffer;
		private int _transformedCount;
		private int _transformedOffset;

		#endregion

		// ReSharper disable PossibleMultipleEnumeration

		#region Constructors

		public StreamWriteTransformingEventArgs(IEnumerable<byte> buffer, int offset, int count, Encoding encoding) : base(buffer, offset, count, encoding)
		{
			this._transformedBuffer = new List<byte>(buffer.ToArray());
			this._transformedCount = count;
			this._transformedOffset = offset;
		}

		#endregion

		// ReSharper restore PossibleMultipleEnumeration

		#region Properties

		public virtual IList<byte> TransformedBuffer
		{
			get { return this._transformedBuffer; }
		}

		public virtual int TransformedCount
		{
			get { return this._transformedCount; }
			set { this._transformedCount = value; }
		}

		public virtual int TransformedOffset
		{
			get { return this._transformedOffset; }
			set { this._transformedOffset = value; }
		}

		#endregion
	}
}