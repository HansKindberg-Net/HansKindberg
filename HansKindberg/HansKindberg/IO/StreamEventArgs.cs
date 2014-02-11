using System;
using System.Text;

namespace HansKindberg.IO
{
	public class StreamEventArgs : EventArgs
	{
		#region Fields

		private readonly string _content;
		private readonly Encoding _encoding;

		#endregion

		#region Constructors

		public StreamEventArgs(string content, Encoding encoding)
		{
			if(encoding == null)
				throw new ArgumentNullException("encoding");

			this._content = content;
			this._encoding = encoding;
		}

		#endregion

		#region Properties

		public virtual string Content
		{
			get { return this._content; }
		}

		public virtual Encoding Encoding
		{
			get { return this._encoding; }
		}

		#endregion
	}
}