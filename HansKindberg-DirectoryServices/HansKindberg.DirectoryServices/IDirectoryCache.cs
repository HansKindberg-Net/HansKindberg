using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryCache
	{
		#region Properties

		IDictionary<string, object> Items { get; }

		#endregion

		#region Methods

		void Clear();

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		object Get(string key);

		bool Remove(string key);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set")]
		void Set(string key, object value);

		#endregion
	}
}