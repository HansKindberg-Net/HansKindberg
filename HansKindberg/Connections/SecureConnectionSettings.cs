using System;
using System.Collections.Generic;
using System.Globalization;

namespace HansKindberg.Connections
{
	public abstract class SecureConnectionSettings : ISecureConnectionSettings
	{
		#region Fields

		public const string AuthenticationMethodKeyName = "AuthenticationMethod";
		public const string PasswordKeyName = "Password";
		public const string UserNameKeyName = "UserName";
		private AuthenticationMethod? _authenticationMethod;
		private string _password;
		private string _userName;

		#endregion

		#region Constructors

		protected SecureConnectionSettings(string connectionString, IConnectionStringParser connectionStringParser)
		{
			if(connectionString == null)
				throw new ArgumentNullException("connectionString");

			if(connectionStringParser == null)
				throw new ArgumentNullException("connectionStringParser");

			this.Initialize(connectionStringParser.ToDictionary(connectionString), true);
		}

		protected SecureConnectionSettings(IDictionary<string, string> connectionStringParameters, bool allowInvalidKeys)
		{
			if(connectionStringParameters == null)
				throw new ArgumentNullException("connectionStringParameters");

			this.Initialize(connectionStringParameters, allowInvalidKeys);
		}

		#endregion

		#region Properties

		public virtual AuthenticationMethod? AuthenticationMethod
		{
			get { return this._authenticationMethod; }
			set { this._authenticationMethod = value; }
		}

		public virtual string Password
		{
			get { return this._password; }
			set { this._password = value; }
		}

		public virtual string UserName
		{
			get { return this._userName; }
			set { this._userName = value; }
		}

		#endregion

		#region Methods

		private void Initialize(IDictionary<string, string> connectionStringParameters, bool allowInvalidKeys)
		{
			if(connectionStringParameters == null)
				throw new ArgumentNullException("connectionStringParameters");

			string parameterValue;

			string parameterKey = AuthenticationMethodKeyName;
			if(connectionStringParameters.TryGetValue(parameterKey, out parameterValue))
			{
				try
				{
					this._authenticationMethod = (AuthenticationMethod) Enum.Parse(typeof(AuthenticationMethod), parameterValue);
					if(!allowInvalidKeys)
						connectionStringParameters.Remove(parameterKey);
				}
				catch(Exception exception)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Could not parse the value \"{0}\" to \"{1}\".", parameterValue, typeof(AuthenticationMethod).FullName), "connectionStringParameters", exception);
				}
			}

			parameterKey = UserNameKeyName;
			if(connectionStringParameters.TryGetValue(parameterKey, out parameterValue))
			{
				this._userName = parameterValue;
				if(!allowInvalidKeys)
					connectionStringParameters.Remove(parameterKey);
			}

			parameterKey = PasswordKeyName;
			if(connectionStringParameters.TryGetValue(parameterKey, out parameterValue))
			{
				this._password = parameterValue;
				if(!allowInvalidKeys)
					connectionStringParameters.Remove(parameterKey);
			}
		}

		#endregion
	}
}