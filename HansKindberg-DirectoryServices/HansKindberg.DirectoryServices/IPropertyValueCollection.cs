using System.Collections;

namespace HansKindberg.DirectoryServices
{
	public interface IPropertyValueCollection : IList
	{
		#region Properties

		string PropertyName { get; }
		object Value { get; set; }

		#endregion
	}
}