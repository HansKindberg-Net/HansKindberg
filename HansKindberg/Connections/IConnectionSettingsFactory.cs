namespace HansKindberg.Connections
{
	public interface IConnectionSettingsFactory
	{
		#region Methods

		TConnectionSettings Create<TConnectionSettings>(string connectionString) where TConnectionSettings : ConnectionSettings, new();
		TConnectionSettings Create<TConnectionSettings>(string connectionString, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : ConnectionSettings, new();
		TConnectionSettings Create<TConnectionSettings>(string connectionString, IConnectionStringParser connectionStringParser, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : ConnectionSettings, new();

		#endregion
	}
}