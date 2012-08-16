using System.Collections.Generic;

namespace HansKindberg.Connections
{
	public interface IConnectionSettings
	{
		#region Methods

		void Initialize(IDictionary<string, string> parameters, bool throwExceptionIfThereAreInvalidParameterKeys);

		#endregion
	}
}