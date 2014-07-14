using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices.Connections
{
	public abstract class GeneralDirectoryConnectionParser
	{
		#region Fields

		private static readonly IEqualityComparer<string> _defaultStringComparer = System.StringComparer.OrdinalIgnoreCase;
		private readonly char _nameValueDelimiter;
		private readonly char _parameterDelimiter;
		private readonly IEqualityComparer<string> _stringComparer;

		#endregion

		#region Constructors

		protected GeneralDirectoryConnectionParser() : this(GeneralDirectoryConnection.DefaultParameterDelimiter, GeneralDirectoryConnection.DefaultNameValueDelimiter, _defaultStringComparer) {}

		protected GeneralDirectoryConnectionParser(char parameterDelimiter, char nameValueDelimiter, IEqualityComparer<string> stringComparer)
		{
			if(stringComparer == null)
				throw new ArgumentNullException("stringComparer");

			this._nameValueDelimiter = nameValueDelimiter;
			this._parameterDelimiter = parameterDelimiter;
			this._stringComparer = stringComparer;
		}

		#endregion

		#region Properties

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

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual bool TrySetAuthenticationValue(GeneralDirectoryConnection generalDirectoryConnection, KeyValuePair<string, string> keyValuePair)
		{
			if(generalDirectoryConnection == null)
				throw new ArgumentNullException("generalDirectoryConnection");

			if(string.IsNullOrEmpty(keyValuePair.Key))
				throw new FormatException("A key can not be empty.");

			switch(keyValuePair.Key.ToLowerInvariant())
			{
				case "authenticationtypes":
				{
					generalDirectoryConnection.Authentication.AuthenticationTypes = (AuthenticationTypes) Enum.Parse(typeof(AuthenticationTypes), keyValuePair.Value);
					return true;
				}
				case "password":
				{
					generalDirectoryConnection.Authentication.Password = keyValuePair.Value;
					return true;
				}
				case "username":
				{
					generalDirectoryConnection.Authentication.UserName = keyValuePair.Value;
					return true;
				}
				default:
				{
					return false;
				}
			}
		}

		#endregion
	}
}