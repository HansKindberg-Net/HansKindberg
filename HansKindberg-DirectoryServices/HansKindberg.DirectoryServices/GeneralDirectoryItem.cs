using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public abstract class GeneralDirectoryItem : IGeneralDirectoryItem
	{
		#region Fields

		private IDictionary<string, object> _properties;

		#endregion

		#region Properties

		public abstract string Path { get; }

		public virtual IDictionary<string, object> Properties
		{
			get { return this._properties ?? (this._properties = new Dictionary<string, object>(this.PropertyKeyComparer)); }
		}

		protected internal virtual StringComparer PropertyKeyComparer
		{
			get { return StringComparer.OrdinalIgnoreCase; }
		}

		#endregion
	}
}