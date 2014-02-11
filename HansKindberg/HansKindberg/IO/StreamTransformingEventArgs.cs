using System.Text;

namespace HansKindberg.IO
{
	public class StreamTransformingEventArgs : StreamEventArgs
	{
		#region Fields

		private string _transformedContent;

		#endregion

		#region Constructors

		public StreamTransformingEventArgs(string content, Encoding encoding) : base(content, encoding)
		{
			this._transformedContent = content;
		}

		#endregion

		#region Properties

		public virtual string TransformedContent
		{
			get { return this._transformedContent; }
			set { this._transformedContent = value; }
		}

		#endregion
	}
}