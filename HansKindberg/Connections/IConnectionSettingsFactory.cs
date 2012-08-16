using System;

namespace HansKindberg.Connections
{
	public interface IConnectionSettingsFactory
	{
		#region Methods

		TConnectionSettings Create<TConnectionSettings>(string connectionString) where TConnectionSettings : IConnectionSettings, new();
		TConnectionSettings Create<TConnectionSettings>(string connectionString, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : IConnectionSettings, new();
		TConnectionSettings Create<TConnectionSettings>(string connectionString, IConnectionStringParser connectionStringParser, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys) where TConnectionSettings : IConnectionSettings, new();
		IConnectionSettings Create(Type connectionSettingsType, string connectionString);
		IConnectionSettings Create(Type connectionSettingsType, string connectionString, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);
		IConnectionSettings Create(Type connectionSettingsType, string connectionString, IConnectionStringParser connectionStringParser, bool throwExceptionIfThereAreInvalidConnectionsStringParameterKeys);

		#endregion
	}
}