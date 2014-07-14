using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace HansKindberg.DirectoryServices.Windows
{
	public class WindowsDirectoryUriParser : IWindowsDirectoryUriParser
	{
		#region Fields

		private readonly ILocalPathParser _localPathParser;

		#endregion

		#region Constructors

		public WindowsDirectoryUriParser(ILocalPathParser localPathParser)
		{
			if(localPathParser == null)
				throw new ArgumentNullException("localPathParser");

			this._localPathParser = localPathParser;
		}

		#endregion

		#region Properties

		protected internal virtual ILocalPathParser LocalPathParser
		{
			get { return this._localPathParser; }
		}

		#endregion

		#region Methods

		public virtual IWindowsDirectoryUri Parse(string value)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			var windowsDirectoryUri = new WindowsDirectoryUri();

			try
			{
				var uri = new Uri(value);

				windowsDirectoryUri.Scheme = (WindowsScheme) Enum.Parse(typeof(WindowsScheme), uri.Scheme, true);

				var segments = value.Replace("://", "/").Split("/".ToCharArray()).Skip(1).ToArray();

				if(segments.Length > 0)
				{
					var hostAndPort = segments[0].Split(":".ToCharArray(), 2);

					windowsDirectoryUri.Host = hostAndPort[0];

					if(hostAndPort.Length > 1)
						windowsDirectoryUri.Port = int.Parse(hostAndPort[1], CultureInfo.InvariantCulture);
				}

				if(uri.LocalPath.Length > 1)
				{
					foreach(var segment in uri.Segments.Skip(1))
					{
						windowsDirectoryUri.LocalPath.Add(segment.TrimEnd("/".ToCharArray()));
					}
				}

				if(uri.LocalPath.Length > 1)
					windowsDirectoryUri.LocalPath.AddRange(this.LocalPathParser.Parse(uri.LocalPath.TrimStart(new[] {WindowsDirectoryUri.DefaultLocalPathDelimiter})));
			}
			catch(Exception exception)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The windows-directory-uri \"{0}\" is invalid.", value), exception);
			}

			return windowsDirectoryUri;
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool TryParse(string value, out IWindowsDirectoryUri windowsDirectoryUri)
		{
			windowsDirectoryUri = null;

			if(value == null)
				return false;

			try
			{
				windowsDirectoryUri = this.Parse(value);
				return true;
			}
			catch
			{
				windowsDirectoryUri = null;
				return false;
			}
		}

		#endregion
	}
}