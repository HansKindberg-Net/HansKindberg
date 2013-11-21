using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Configuration.UnitTests.Mocks
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class NamedConfigurationElementCollectionMock : NamedConfigurationElementCollection<NamedConfigurationElementMock>
	{
		#region Methods

		public new virtual object GetElementKey(ConfigurationElement element)
		{
			return base.GetElementKey(element);
		}

		#endregion
	}
}