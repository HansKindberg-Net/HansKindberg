using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Configuration.UnitTests.Mocks
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class ConfigurationElementCollectionMock : ConfigurationElementCollection<ConfigurationElementMock>
	{
		#region Methods

		public new virtual void BaseAdd(ConfigurationElement element)
		{
			base.BaseAdd(element);
		}

		public new virtual ConfigurationElement BaseGet(int index)
		{
			return base.BaseGet(index);
		}

		public new virtual ConfigurationElement CreateNewElement()
		{
			return base.CreateNewElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigurationElementMock) element).Name;
		}

		public new virtual void SetReadOnly()
		{
			base.SetReadOnly();
		}

		#endregion
	}
}