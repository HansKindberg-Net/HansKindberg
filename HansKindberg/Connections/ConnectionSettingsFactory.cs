using System;

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

		public virtual TConnectionSettings Create<TConnectionSettings>(string connectionString) where TConnectionSettings : ConnectionSettings, new()
		{
			return this.Create<TConnectionSettings>(connectionString, true);
		}

		public virtual TConnectionSettings Create<TConnectionSettings>(string connectionString, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : ConnectionSettings, new()
		{
			return this.Create<TConnectionSettings>(connectionString, this._connectionStringParser, throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);
		}

		public virtual TConnectionSettings Create<TConnectionSettings>(string connectionString, IConnectionStringParser connectionStringParser, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : ConnectionSettings, new()
		{
			if(connectionString == null)
				throw new ArgumentNullException("connectionString");

			if(connectionStringParser == null)
				throw new ArgumentNullException("connectionStringParser");

			TConnectionSettings connectionSettings = new TConnectionSettings();

			connectionSettings.Initialize(connectionStringParser.ToDictionary(connectionString), throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);

			return connectionSettings;
		}

		#endregion
	}
}