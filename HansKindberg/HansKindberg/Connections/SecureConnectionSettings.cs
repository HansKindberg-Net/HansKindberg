using System;
using System.Collections.Generic;

namespace HansKindberg.Connections
{
	public abstract class SecureConnectionSettings : ConnectionSettings, ISecureConnectionSettings
	{
		#region Fields

		public const string PasswordKey = "Password";
		public const string UserNameKey = "UserName";
		private string _password;
		private string _userName;

		#endregion

		#region Properties

		public virtual string Password
		{
			get
			{
				this.ValidateInitialized();
				return this._password;
			}
			set { this._password = value; }
		}

		public virtual string UserName
		{
			get
			{
				this.ValidateInitialized();
				return this._userName;
			}
			set { this._userName = value; }
		}

		protected internal override IEnumerable<string> ValidParameterKeys
		{
			get
			{
				List<string> validParameterKeys = new List<string>(base.ValidParameterKeys) {UserNameKey, PasswordKey};
				return validParameterKeys.ToArray();
			}
		}

		#endregion

		#region Methods

		public override void Initialize(IDictionary<string, string> parameters, bool throwExceptionIfThereAreInvalidParameterKeys)
		{
			if(parameters == null)
				throw new ArgumentNullException("parameters");

			this.ValidateNotInitialized();

			this.InitializeUserName(parameters);
			this.InitializePassword(parameters);

			this.ValidateInvalidParameterKeys(parameters.Keys, throwExceptionIfThereAreInvalidParameterKeys);

			this.Initialized = true;
		}

		protected internal virtual void InitializePassword(IDictionary<string, string> parameters)
		{
			string password;
			if(this.TryGetValueAndRemove(parameters, PasswordKey, out password))
				this._password = password;
		}

		protected internal virtual void InitializeUserName(IDictionary<string, string> parameters)
		{
			string userName;
			if(this.TryGetValueAndRemove(parameters, UserNameKey, out userName))
				this._userName = userName;
		}

		#endregion
	}
}