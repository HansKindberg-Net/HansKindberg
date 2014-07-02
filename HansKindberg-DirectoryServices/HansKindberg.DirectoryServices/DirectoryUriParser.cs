using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace HansKindberg.DirectoryServices
{
	public class DirectoryUriParser : IDirectoryUriParser
	{
		#region Fields

		private readonly IDistinguishedNameParser _distinguishedNameParser;

		#endregion

		#region Constructors

		public DirectoryUriParser(IDistinguishedNameParser distinguishedNameParser)
		{
			if(distinguishedNameParser == null)
				throw new ArgumentNullException("distinguishedNameParser");

			this._distinguishedNameParser = distinguishedNameParser;
		}

		#endregion

		#region Properties

		protected internal virtual IDistinguishedNameParser DistinguishedNameParser
		{
			get { return this._distinguishedNameParser; }
		}

		#endregion

		#region Methods

		public virtual IDirectoryUri Parse(string value)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			var directoryUri = new DirectoryUri();

			try
			{
				var uri = new Uri(value);

				directoryUri.Scheme = (Scheme) Enum.Parse(typeof(Scheme), uri.Scheme, true);

				var segments = value.Replace("://", "/").Split("/".ToCharArray()).Skip(1).ToArray();

				if(segments.Length > 0)
				{
					var hostAndPort = segments[0].Split(":".ToCharArray(), 2);

					directoryUri.Host = hostAndPort[0];

					if(hostAndPort.Length > 1)
						directoryUri.Port = int.Parse(hostAndPort[1], CultureInfo.InvariantCulture);
				}

				if(uri.LocalPath.Length > 1)
					directoryUri.DistinguishedName = this.DistinguishedNameParser.Parse(uri.LocalPath.TrimStart("/".ToCharArray()));
			}
			catch(Exception exception)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The directory-uri \"{0}\" is invalid.", value), exception);
			}

			return directoryUri;
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool TryParse(string value, out IDirectoryUri directoryUri)
		{
			directoryUri = null;

			if(value == null)
				return false;

			try
			{
				directoryUri = this.Parse(value);
				return true;
			}
			catch
			{
				directoryUri = null;
				return false;
			}
		}

		#endregion
	}
}