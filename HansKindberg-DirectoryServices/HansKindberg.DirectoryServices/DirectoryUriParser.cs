using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace HansKindberg.DirectoryServices
{
	public class DirectoryUriParser : IDirectoryUriParser
	{
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

				directoryUri.Host = uri.Host;

				if(uri.Authority.EndsWith(":" + uri.Port, StringComparison.OrdinalIgnoreCase))
					directoryUri.Port = uri.Port;

				if(uri.Segments.Length > 2)
					throw new UriFormatException("The directory-uri can contain a maximum of two segments.");

				if(uri.Segments.Length == 2)
					directoryUri.DistinguishedName = uri.Segments[1];
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