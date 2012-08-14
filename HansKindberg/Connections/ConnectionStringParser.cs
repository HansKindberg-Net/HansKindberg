using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace HansKindberg.Connections
{
	public class ConnectionStringParser : IConnectionStringParser
	{
		#region Fields

		public const char DefaultKeyValuePairDelimiter = ';';
		public const char DefaultKeyValueSeparator = '=';
		public const bool DefaultTrim = true;
		private readonly char _keyValuePairDelimiter;
		private readonly char _keyValueSeparator;
		private readonly StringComparer _stringComparer;
		private readonly bool _trim;

		#endregion

		#region Constructors

		public ConnectionStringParser() : this(DefaultTrim) {}
		public ConnectionStringParser(bool trim) : this(trim, DefaultKeyValuePairDelimiter, DefaultKeyValueSeparator, StringComparer.OrdinalIgnoreCase) {}

		public ConnectionStringParser(bool trim, char keyValuePairDelimiter, char keyValueSeparator, StringComparer stringComparer)
		{
			if(stringComparer == null)
				throw new ArgumentNullException("stringComparer");

			this._keyValuePairDelimiter = keyValuePairDelimiter;
			this._keyValueSeparator = keyValueSeparator;
			this._stringComparer = stringComparer;
			this._trim = trim;
		}

		#endregion

		#region Properties

		public virtual char KeyValuePairDelimiter
		{
			get { return this._keyValuePairDelimiter; }
		}

		public virtual char KeyValueSeparator
		{
			get { return this._keyValueSeparator; }
		}

		public virtual StringComparer StringComparer
		{
			get { return this._stringComparer; }
		}

		public virtual bool Trim
		{
			get { return this._trim; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		public virtual KeyValuePair<string, string> GetKeyValuePair(string keyValuePairString)
		{
			if(keyValuePairString == null)
				throw new ArgumentNullException("keyValuePairString");

			string[] keyValueArray = keyValuePairString.Split(new[] {this.KeyValueSeparator}, StringSplitOptions.None);

			if(keyValueArray.Length != 2)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Each keyvaluepair must contain exactly one separator, '{0}'.", this.KeyValueSeparator), "keyValuePairString");

			string key = this.Trim ? keyValueArray[0].Trim() : keyValueArray[0];
			if(string.IsNullOrEmpty(key))
				throw new ArgumentException("A key in a keyvaluepair can not be empty.", "keyValuePairString");

			string value = this.Trim ? keyValueArray[1].Trim() : keyValueArray[1];

			return new KeyValuePair<string, string>(key, value);
		}

		public virtual IEnumerable<string> GetKeyValuePairStrings(string connectionString)
		{
			if(connectionString == null)
				throw new ArgumentNullException("connectionString");

			return connectionString.Split(new[] {this.KeyValuePairDelimiter}, StringSplitOptions.RemoveEmptyEntries);
		}

		public virtual IDictionary<string, string> ToDictionary(string connectionString)
		{
			if(connectionString == null)
				throw new ArgumentNullException("connectionString");

			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.StringComparer);

			// ReSharper disable LoopCanBeConvertedToQuery
			foreach(string keyValuePairString in this.GetKeyValuePairStrings(connectionString))
			{
				string temporaryKeyValuePairString = this.Trim ? keyValuePairString.Trim() : keyValuePairString;

				if(this.Trim && string.IsNullOrEmpty(temporaryKeyValuePairString))
					continue;

				KeyValuePair<string, string> keyValuePair = this.GetKeyValuePair(temporaryKeyValuePairString);
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			// ReSharper restore LoopCanBeConvertedToQuery

			return dictionary;
		}

		#endregion
	}
}