using System.Configuration;

namespace HansKindberg.Configuration.UnitTests.Mocks
{
	public class AlreadyNamedConfigurationElementMock : NamedConfigurationElement
	{
		#region Fields

		protected internal const string DefaultValue = "DefaultValue";
		protected internal const string InvalidCharacters = "1";
		protected internal const int MaxLength = 20;
		protected internal const int MinLength = 10;
		private const string _namePropertyName = "name";

		#endregion

		#region Properties

		[ConfigurationProperty(_namePropertyName, DefaultValue = DefaultValue, IsKey = true, IsRequired = true)]
		[StringValidator(InvalidCharacters = InvalidCharacters, MaxLength = MaxLength, MinLength = MinLength)]
		public override string Name
		{
			get { return (string) this[this.NamePropertyName]; }
			set { this[this.NamePropertyName] = value; }
		}

		public new virtual ConfigurationPropertyCollection Properties
		{
			get { return base.Properties; }
		}

		#endregion
	}
}