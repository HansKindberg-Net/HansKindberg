using System.Collections.Generic;
using HansKindberg.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public class ReadOnlyDirectoryNode : IReadOnlyDirectoryNode
	{
		#region Fields

		private IReadOnlyDictionary<string, IEnumerable<object>> _properties;

		#endregion

		#region Properties

		public virtual IDirectoryUri Path { get; set; }

		public virtual IReadOnlyDictionary<string, IEnumerable<object>> Properties
		{
			get { return this._properties ?? (this._properties = new DictionaryWrapper<string, IEnumerable<object>>(new Dictionary<string, IEnumerable<object>>())); }
			set { this._properties = value; }
		}

		#endregion
	}
}