using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;

namespace HansKindberg.DirectoryServices.Connections
{
	public class DirectoryConnectionParser : IDirectoryConnectionParser
	{
		#region Fields

		private static readonly IEqualityComparer<string> _defaultStringComparer = System.StringComparer.OrdinalIgnoreCase;
		private readonly IDistinguishedNameParser _distinguishedNameParser;
		private readonly char _nameValueDelimiter;
		private readonly char _parameterDelimiter;
		private readonly IEqualityComparer<string> _stringComparer;

		#endregion

		#region Constructors

		public DirectoryConnectionParser(IDistinguishedNameParser distinguishedNameParser) : this(DirectoryConnection.DefaultParameterDelimiter, DirectoryConnection.DefaultNameValueDelimiter, _defaultStringComparer, distinguishedNameParser) {}

		public DirectoryConnectionParser(char parameterDelimiter, char nameValueDelimiter, IEqualityComparer<string> stringComparer, IDistinguishedNameParser distinguishedNameParser)
		{
			if(stringComparer == null)
				throw new ArgumentNullException("stringComparer");

			if(distinguishedNameParser == null)
				throw new ArgumentNullException("distinguishedNameParser");

			this._distinguishedNameParser = distinguishedNameParser;
			this._nameValueDelimiter = nameValueDelimiter;
			this._parameterDelimiter = parameterDelimiter;
			this._stringComparer = stringComparer;
		}

		#endregion

		#region Properties

		protected internal virtual IDistinguishedNameParser DistinguishedNameParser
		{
			get { return this._distinguishedNameParser; }
		}

		public virtual char NameValueDelimiter
		{
			get { return this._nameValueDelimiter; }
		}

		public virtual char ParameterDelimiter
		{
			get { return this._parameterDelimiter; }
		}

		public virtual IEqualityComparer<string> StringComparer
		{
			get { return this._stringComparer; }
		}

		#endregion

		#region Methods

		protected internal virtual IDictionary<string, string> GetConnectionStringAsDictionary(string connectionString)
		{
			var dictionary = new Dictionary<string, string>(this.StringComparer);

			if(!string.IsNullOrEmpty(connectionString))
			{
				foreach(var nameValue in connectionString.Split(new[] {this.ParameterDelimiter}))
				{
					var nameValueParts = nameValue.Split(new[] {this.NameValueDelimiter}, 2);

					if(nameValueParts.Length == 0)
						continue;

					var value = nameValueParts.Length > 1 ? nameValueParts[1] : string.Empty;

					dictionary.Add(nameValueParts[0], value);
				}
			}

			return dictionary;
		}

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

			if(string.IsNullOrEmpty(keyValuePair.Key))
				throw new FormatException("A key can not be empty.");

			switch(keyValuePair.Key.ToLowerInvariant())
			{
				case "authenticationtypes":
				{
					directoryConnection.AuthenticationTypes = (AuthenticationTypes) Enum.Parse(typeof(AuthenticationTypes), keyValuePair.Value);
					break;
				}
				case "distinguishedname":
				{
					directoryConnection.DistinguishedName = this.DistinguishedNameParser.Parse(keyValuePair.Value);
					break;
				}
				case "host":
				{
					directoryConnection.Host = keyValuePair.Value;
					break;
				}
				case "password":
				{
					directoryConnection.Password = keyValuePair.Value;
					break;
				}
				case "port":
				{
					directoryConnection.Port = int.Parse(keyValuePair.Value, CultureInfo.InvariantCulture);
					break;
				}
				case "scheme":
				{
					directoryConnection.Scheme = (Scheme) Enum.Parse(typeof(Scheme), keyValuePair.Value);
					break;
				}
				case "username":
				{
					directoryConnection.UserName = keyValuePair.Value;
					break;
				}
				default:
				{
					throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The key \"{0}\" is not valid.", keyValuePair.Key));
				}
			}
		}

		#endregion
	}
}