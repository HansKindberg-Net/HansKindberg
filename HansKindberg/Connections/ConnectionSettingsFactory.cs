using System;
using System.Globalization;
using System.Reflection;

namespace HansKindberg.Connections
{
	public class ConnectionSettingsFactory : IConnectionSettingsFactory
	{
		#region Fields

		private readonly IConnectionStringParser _connectionStringParser;

		#endregion

		#region Constructors

		public ConnectionSettingsFactory(IConnectionStringParser connectionStringParser)
		{
			if(connectionStringParser == null)
				throw new ArgumentNullException("connectionStringParser");

			this._connectionStringParser = connectionStringParser;
		}

		#endregion

		#region Methods

		public virtual TConnectionSettings Create<TConnectionSettings>(string connectionString) where TConnectionSettings : IConnectionSettings, new()
		{
			return (TConnectionSettings)this.Create(typeof(TConnectionSettings), connectionString);
		}

		public virtual TConnectionSettings Create<TConnectionSettings>(string connectionString, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : IConnectionSettings, new()
		{
			return (TConnectionSettings)this.Create(typeof(TConnectionSettings), connectionString, throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);
		}

		public virtual TConnectionSettings Create<TConnectionSettings>(string connectionString, IConnectionStringParser connectionStringParser, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : IConnectionSettings, new()
		{
			return (TConnectionSettings)this.Create(typeof(TConnectionSettings), connectionString, connectionStringParser, throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);
		}

		public virtual IConnectionSettings Create(Type connectionSettingsType, string connectionString)
		{
			return this.Create(connectionSettingsType, connectionString, true);
		}

		public virtual IConnectionSettings Create(Type connectionSettingsType, string connectionString, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys)
		{
			return this.Create(connectionSettingsType, connectionString, this._connectionStringParser, throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);
		}

		public virtual IConnectionSettings Create(Type connectionSettingsType, string connectionString, IConnectionStringParser connectionStringParser, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys)
		{
			if(connectionSettingsType == null)
				throw new ArgumentNullException("connectionSettingsType");

			if(!typeof(IConnectionSettings).IsAssignableFrom(connectionSettingsType))
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The connection settings type \"{0}\" must implement the interface \"{1}\".", connectionSettingsType.FullName, typeof(IConnectionSettings).FullName));

			ConstructorInfo constructor = connectionSettingsType.GetConstructor(new Type[0]);
			if(constructor == null)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The connection settings type \"{0}\" must have a public parameterless constructor.", connectionSettingsType.FullName));

			if (connectionString == null)
				throw new ArgumentNullException("connectionString");

			if (connectionStringParser == null)
				throw new ArgumentNullException("connectionStringParser");

			IConnectionSettings connectionSettings = (IConnectionSettings)constructor.Invoke(new object[0]);

			connectionSettings.Initialize(connectionStringParser.ToDictionary(connectionString), throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);

			return connectionSettings;
		}

		#endregion
	}
}