using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HansKindberg.IO
{
	public class StreamWriteEventArgs : EventArgs
	{
		#region Fields

		private readonly IEnumerable<byte> _buffer;
		private readonly int _count;
		private readonly Encoding _encoding;
		private readonly int _offset;

		#endregion

		#region Constructors

		public StreamWriteEventArgs(IEnumerable<byte> buffer, int offset, int count, Encoding encoding)
		{
			if(buffer == null)
				throw new ArgumentNullException("buffer");

			if(encoding == null)
				throw new ArgumentNullException("encoding");

			this._buffer = buffer.ToArray();
			this._count = count;
			this._encoding = encoding;
			this._offset = offset;
		}

		#endregion

		#region Properties

		public virtual IEnumerable<byte> Buffer
		{
			get { return this._buffer; }
		}

		public virtual int Count
		{
			get { return this._count; }
		}

		public virtual Encoding Encoding
		{
			get { return this._encoding; }
		}

		public virtual int Offset
		{
			get { return this._offset; }
		}

		#endregion
	}
}