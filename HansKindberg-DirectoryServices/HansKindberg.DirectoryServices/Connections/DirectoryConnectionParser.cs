using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace HansKindberg.DirectoryServices.Connections
{
	public class DirectoryConnectionParser : GeneralDirectoryConnectionParser, IDirectoryConnectionParser
	{
		#region Fields

		private readonly IDistinguishedNameParser _distinguishedNameParser;

		#endregion

		#region Constructors

		public DirectoryConnectionParser(IDistinguishedNameParser distinguishedNameParser)
		{
			if(distinguishedNameParser == null)
				throw new ArgumentNullException("distinguishedNameParser");

			this._distinguishedNameParser = distinguishedNameParser;
		}

		public DirectoryConnectionParser(char parameterDelimiter, char nameValueDelimiter, IEqualityComparer<string> stringComparer, IDistinguishedNameParser distinguishedNameParser) : base(parameterDelimiter, nameValueDelimiter, stringComparer)
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

		public virtual IDirectoryConnection Parse(string connectionString)
		{
			if(connectionString == null)
				throw new ArgumentNullException("connectionString");

			var directoryConnection = new DirectoryConnection();

			var dictionary = this.GetConnectionStringAsDictionary(connectionString);

			if(dictionary.Any())
			{
				try
				{
					foreach(var keyValuePair in dictionary)
					{
						this.TrySetValue(directoryConnection, keyValuePair);
					}
				}
				catch(Exception exception)
				{
					throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The connection-string \"{0}\" could not be parsed.", connectionString), exception);
				}
			}

			return directoryConnection;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual void TrySetValue(DirectoryConnection directoryConnection, KeyValuePair<string, string> keyValuePair)
		{
			if(directoryConnection == null)
				throw new ArgumentNullException("directoryConnection");

			if(!this.TrySetAuthenticationValue(directoryConnection, keyValuePair))
			{
				switch(keyValuePair.Key.ToLowerInvariant())
				{
					case "distinguishedname":
					{
						directoryConnection.Url.DistinguishedName = this.DistinguishedNameParser.Parse(keyValuePair.Value);
						break;
					}
					case "host":
					{
						directoryConnection.Url.Host = keyValuePair.Value;
						break;
					}
					case "port":
					{
						directoryConnection.Url.Port = int.Parse(keyValuePair.Value, CultureInfo.InvariantCulture);
						break;
					}
					case "scheme":
					{
						directoryConnection.Url.Scheme = (Scheme) Enum.Parse(typeof(Scheme), keyValuePair.Value);
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