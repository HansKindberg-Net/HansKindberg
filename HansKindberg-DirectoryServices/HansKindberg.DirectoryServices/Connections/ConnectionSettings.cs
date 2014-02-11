using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using HansKindberg.Connections;

namespace HansKindberg.DirectoryServices.Connections
{
	public class ConnectionSettings : SecureConnectionSettings, IConnectionSettings
	{
		#region Fields

		public const string AuthenticationTypesKey = "AuthenticationTypes";
		public const string DistinguishedNameKey = "DistinguishedName";
		public const string HostKey = "Host";
		public const string PortKey = "Port";
		public const string SchemeKey = "Scheme";
		private AuthenticationTypes? _authenticationTypes;
		private string _distinguishedName;
		private string _host;
		private int? _port;
		private Scheme _scheme;

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

		public virtual string DistinguishedName
		{
			get
			{
				this.ValidateInitialized();
				return this._distinguishedName;
			}
			set { this._distinguishedName = value; }
		}

		public virtual string Host
		{
			get
			{
				this.ValidateInitialized();
				return this._host;
			}
			set { this._host = value; }
		}

		public virtual int? Port
		{
			get
			{
				this.ValidateInitialized();
				return this._port;
			}
			set { this._port = value; }
		}

		public virtual Scheme Scheme
		{
			get
			{
				this.ValidateInitialized();
				return this._scheme;
			}
			set { this._scheme = value; }
		}

		protected override IEnumerable<string> ValidParameterKeys
		{
			get
			{
				List<string> validParameterKeys = new List<string>(base.ValidParameterKeys) {SchemeKey, HostKey, PortKey, DistinguishedNameKey, AuthenticationTypesKey};
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

			this.InitializeScheme(parameters);
			this.InitializeHost(parameters);
			this.InitializePort(parameters);

			this.InitializeDistinguishedName(parameters);

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

		protected internal virtual void InitializeDistinguishedName(IDictionary<string, string> parameters)
		{
			string distinguishedName;
			if(this.TryGetValueAndRemove(parameters, DistinguishedNameKey, out distinguishedName))
				this._distinguishedName = distinguishedName;
		}

		protected internal virtual void InitializeHost(IDictionary<string, string> parameters)
		{
			string host;
			if(this.TryGetValueAndRemove(parameters, HostKey, out host))
				this._host = host;
		}

		protected internal virtual void InitializePort(IDictionary<string, string> parameters)
		{
			string portString;

			if(!this.TryGetValueAndRemove(parameters, PortKey, out portString))
				return;

			int port;
			try
			{
				port = int.Parse(portString, CultureInfo.InvariantCulture);
			}
			catch(Exception exception)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The value \"{0}\" of the {1} parameter is not a valid {2}.", portString, PortKey, typeof(int).FullName), "parameters", exception);
			}

			if(port < 0)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The value \"{0}\" of the {1} parameter can not have a negative value.", portString, PortKey), "parameters");

			this._port = port;
		}

		protected internal virtual void InitializeScheme(IDictionary<string, string> parameters)
		{
			object value;
			if(this.TryGetValueAsEnumAndRemove(parameters, typeof(Scheme), SchemeKey, out value))
				this._scheme = (Scheme) value;
			else
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The parameter \"{0}\" is required. Valid values are: {1}.", SchemeKey, string.Join(", ", Enum.GetNames(typeof(Scheme)))), "parameters");
		}

		#endregion
	}
}