using System.Configuration;

namespace HansKindberg.Configuration.UnitTests.Mocks
{
	public class NamedConfigurationElementMock : NamedConfigurationElement
	{
		#region Properties

		public new virtual ConfigurationPropertyCollection Properties
		{
			get { return base.Properties; }
		}

		#endregion
	}
}