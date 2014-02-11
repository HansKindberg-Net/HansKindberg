using System.Collections.Generic;

namespace HansKindberg.DirectoryServices
{
	public interface IPropertyCollection : IEnumerable<IPropertyValueCollection>
	{
		#region Properties

		IPropertyValueCollection this[string propertyName] { get; }

		#endregion
	}
}