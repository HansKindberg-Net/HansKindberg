using System;
using System.Collections.Generic;
using System.DirectoryServices;
using HansKindberg.Connections;

namespace HansKindberg.DirectoryServices.Connections
{
	public class LdapConnectionSettings : SecureConnectionSettings, ILdapConnectionSettings
	{
		#region Fields

		public const string AuthenticationTypesKey = "AuthenticationTypes";
		public const string PathKey = "Path";
		private AuthenticationTypes? _authenticationTypes;
		private string _path;

		#endregion

		#region Properties

		public virtual AuthenticationTypes? AuthenticationTypes
		{
			get
			{
				this.ValidateInitialized();
				return this._authenticationTypes;
			}
			set { this._authenticationTypes = value; }
		}

		public virtual string Path
		{
			get
			{
				this.ValidateInitialized();
				return this._path;
			}
			set { this._path = value; }
		}

		protected override IEnumerable<string> ValidParameterKeys
		{
			get
			{
				List<string> validParameterKeys = new List<string>(base.ValidParameterKeys) {PathKey, AuthenticationTypesKey};
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

			this.InitializePath(parameters);
			this.InitializeUserName(parameters);
			this.InitializePassword(parameters);
			this.InitializeAuthenticationTypes(parameters);

			this.ValidateInvalidParameterKeys(parameters.Keys, throwExceptionIfThereAreInvalidParameterKeys);

			this.Initialized = true;
		}

		protected internal virtual void InitializeAuthenticationTypes(IDictionary<string, string> parameters)
		{
			object value;
			if(this.TryGetValueAsEnumAndRemove(parameters, typeof(AuthenticationTypes), AuthenticationTypesKey, out value))
				this._authenticationTypes = (AuthenticationTypes) value;
		}

		protected internal virtual void InitializePath(IDictionary<string, string> parameters)
		{
			string path;
			if(this.TryGetValueAndRemove(parameters, PathKey, out path))
				this._path = path;
		}

		#endregion
	}
}