using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public class DirectoryNode : IDirectoryNode
	{
		#region Fields

		private IDictionary<string, object> _properties;

		#endregion

		#region Properties

		public virtual IDictionary<string, object> Properties
		{
			get { return this._properties ?? (this._properties = new Dictionary<string, object>(this.PropertyKeyComparer)); }
		}

		protected internal virtual StringComparer PropertyKeyComparer
		{
			get { return StringComparer.OrdinalIgnoreCase; }
		}

		public virtual IDirectoryUri Url { get; set; }

		#endregion
	}
}