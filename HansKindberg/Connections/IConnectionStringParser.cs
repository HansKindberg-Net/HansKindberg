using System.Collections.Generic;

namespace HansKindberg.Connections
{
	public interface IConnectionStringParser
	{
		#region Methods

		string ToConnectionString(IDictionary<string, string> connectionStringParameters);
		IDictionary<string, string> ToDictionary(string connectionString);

		#endregion
	}
}