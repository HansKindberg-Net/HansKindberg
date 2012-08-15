using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace HansKindberg.Connections
{
	public abstract class ConnectionSettings
	{
		#region Constructors

		protected ConnectionSettings() {}

		#endregion

		#region Properties

		protected internal virtual bool Initialized { get; set; }

		protected internal virtual IEnumerable<string> ValidParameterKeys
		{
			get { return new string[0]; }
		}

		#endregion

		#region Methods

		public virtual void Initialize(IDictionary<string, string> parameters, bool throwExceptionIfThereAreInvalidParameterKeys)
		{
			this.ValidateNotInitialized();

			this.Initialized = true;
		}

		protected internal virtual void ThrowInvalidParameterKeyException(IEnumerable<string> invalidParameterKeys)
		{
			if(invalidParameterKeys == null)
				throw new ArgumentNullException("invalidParameterKeys");

			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There are invalid parameter keys in connection setting \"{0}\". The invalid parameter keys are: {1}. Valid parameter keys are: {2}.", this.GetType().FullName, string.Join(", ", invalidParameterKeys.ToArray()), string.Join(", ", this.ValidParameterKeys.ToArray())));
		}

		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected internal virtual bool TryGetValueAndRemove(IDictionary<string, string> connectionStringParameters, string key, out string value)
		{
			if(connectionStringParameters == null)
				throw new ArgumentNullException("connectionStringParameters");

			value = null;

			foreach(string currentKey in connectionStringParameters.Keys)
			{
				if(string.Equals(key, currentKey, StringComparison.OrdinalIgnoreCase))
				{
					value = connectionStringParameters[currentKey];
					connectionStringParameters.Remove(currentKey);
					return true;
				}
			}

			return false;
		}

		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		protected internal virtual bool TryGetValueAsEnumAndRemove(IDictionary<string, string> connectionStringParameters, Type enumType, string key, out object value)
		{
			if(enumType == null)
				throw new ArgumentNullException("enumType");

			value = null;
			string enumString;

			bool tryGetValue = this.TryGetValueAndRemove(connectionStringParameters, key, out enumString);

			if(tryGetValue)
			{
				try
				{
					enumString = enumString.Replace("|", ","); // To make flag enum strings separated by | to work.
					value = Enum.Parse(enumType, enumString, true);
				}
				catch(Exception exception)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Could not parse the value \"{0}\" to \"{1}\".", enumString, enumType.FullName), "connectionStringParameters", exception);
				}
			}

			return tryGetValue;
		}

		protected internal virtual void ValidateInitialized()
		{
			if(!this.Initialized)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The connection settings of type \"{0}\" have not been initialized.", this.GetType().FullName));
		}

		protected internal virtual bool ValidateInvalidParameterKeys(IEnumerable<string> parameterKeys, bool throwExceptionIfThereAreInvalidParameterKeys)
		{
			IEnumerable<string> parameterKeysCopy = parameterKeys;

			parameterKeysCopy = parameterKeysCopy == null ? new string[0] : parameterKeysCopy.ToArray();

			if(parameterKeysCopy.Any())
			{
				if(throwExceptionIfThereAreInvalidParameterKeys)
					this.ThrowInvalidParameterKeyException(parameterKeysCopy);

				return false;
			}

			return true;
		}

		protected internal virtual void ValidateNotInitialized()
		{
			if(this.Initialized)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The connection settings of type \"{0}\" have already been initialized.", this.GetType().FullName));
		}

		#endregion
	}
}