using System.Configuration;

namespace HansKindberg.Configuration.UnitTests.Mocks
{
	public class ConfigurationElementMock : ConfigurationElement
	{
		#region Properties

		[ConfigurationProperty("name")]
		public virtual string Name
		{
			get { return (string) this["name"]; }
			set { this["name"] = value; }
		}

		#endregion
	}
}