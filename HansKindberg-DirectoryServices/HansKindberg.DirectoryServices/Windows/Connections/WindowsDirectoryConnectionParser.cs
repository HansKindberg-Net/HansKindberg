using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;

namespace HansKindberg.DirectoryServices.Windows.Connections
{
	public class WindowsDirectoryConnectionParser : GeneralDirectoryConnectionParser, IWindowsDirectoryConnectionParser
	{
		#region Fields

		private readonly ILocalPathParser _localPathParser;

		#endregion

		#region Constructors

		public WindowsDirectoryConnectionParser(ILocalPathParser localPathParser)
		{
			if(localPathParser == null)
				throw new ArgumentNullException("localPathParser");

			this._localPathParser = localPathParser;
		}

		public WindowsDirectoryConnectionParser(char parameterDelimiter, char nameValueDelimiter, IEqualityComparer<string> stringComparer, ILocalPathParser localPathParser) : base(parameterDelimiter, nameValueDelimiter, stringComparer)
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

		public virtual IWindowsDirectoryConnection Parse(string connectionString)
		{
			if(connectionString == null)
				throw new ArgumentNullException("connectionString");

			var windowsDirectoryConnection = new WindowsDirectoryConnection();

			var dictionary = this.GetConnectionStringAsDictionary(connectionString);

			if(dictionary.Any())
			{
				try
				{
					foreach(var keyValuePair in dictionary)
					{
						this.TrySetValue(windowsDirectoryConnection, keyValuePair);
					}
				}
				catch(Exception exception)
				{
					throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The connection-string \"{0}\" could not be parsed.", connectionString), exception);
				}
			}

			return windowsDirectoryConnection;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual void TrySetValue(WindowsDirectoryConnection windowsDirectoryConnection, KeyValuePair<string, string> keyValuePair)
		{
			if(windowsDirectoryConnection == null)
				throw new ArgumentNullException("windowsDirectoryConnection");

			if(!this.TrySetAuthenticationValue(windowsDirectoryConnection, keyValuePair))
			{
				switch(keyValuePair.Key.ToLowerInvariant())
				{
					case "host":
					{
						windowsDirectoryConnection.Url.Host = keyValuePair.Value;
						break;
					}
					case "localpath":
					{
						windowsDirectoryConnection.Url.LocalPath.AddRange(this.LocalPathParser.Parse(keyValuePair.Value));
						break;
					}
					case "port":
					{
						windowsDirectoryConnection.Url.Port = int.Parse(keyValuePair.Value, CultureInfo.InvariantCulture);
						break;
					}
					case "scheme":
					{
						windowsDirectoryConnection.Url.Scheme = (WindowsScheme) Enum.Parse(typeof(WindowsScheme), keyValuePair.Value);
						break;
					}
					default:
					{
						throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The key \"{0}\" is not valid.", keyValuePair.Key));
					}
				}
			}
		}

		#endregion
	}
}