using System.Collections.Generic;

namespace HansKindberg.Connections
{
	public interface IConnectionStringParser
	{
		#region Methods

		IDictionary<string, string> ToDictionary(string connectionString);

		#endregion
	}
}