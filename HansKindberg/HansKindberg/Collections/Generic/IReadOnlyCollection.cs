using System.Collections.Generic;

namespace HansKindberg.Collections.Generic
{
	public interface IReadOnlyCollection<T> : IEnumerable<T>
	{
		#region Properties

		int Count { get; }

		#endregion
	}
}