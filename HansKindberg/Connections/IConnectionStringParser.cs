using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HansKindberg.Connections
{
	public interface IConnectionStringParser
	{
		IDictionary<string, string> ToDictionary(string connectionString);
	}
}
