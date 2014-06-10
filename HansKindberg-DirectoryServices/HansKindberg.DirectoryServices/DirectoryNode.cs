using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public class DirectoryNode : IDirectoryNode
	{
		#region Fields

		private readonly IDictionary<string, IEnumerable<object>> _properties = new Dictionary<string, IEnumerable<object>>();

		#endregion

		#region Properties

		public virtual Guid? Guid { get; set; }
		public virtual string Name { get; set; }
		public virtual string NativeGuid { get; set; }
		public virtual IDirectoryUri ParentPath { get; set; }
		public virtual IDirectoryUri Path { get; set; }

		public virtual IDictionary<string, IEnumerable<object>> Properties
		{
			get { return this._properties; }
		}

		public virtual string SchemaClassName { get; set; }

		#endregion
	}
}