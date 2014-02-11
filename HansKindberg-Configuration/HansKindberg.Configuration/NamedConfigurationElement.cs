using System;
using System.Configuration;

namespace HansKindberg.Configuration
{
	public abstract class NamedConfigurationElement : ConfigurationElement
	{
		#region Fields

		private const string _invalidNameCharacters = " ~!@#$%^&*()[]{}/;'\"|\\";
		private const int _maximumNameLength = 256;
		private const string _namePropertyName = "name";

		#endregion

		#region Properties

		protected internal virtual bool Initialized { get; set; }

		protected internal virtual string InvalidNameCharacters
		{
			get { return _invalidNameCharacters; }
		}

		protected internal virtual int MaximumNameLength
		{
			get { return _maximumNameLength; }
		}

		public virtual string Name
		{
			get { return (string) this[this.NamePropertyName]; }
			set
			{
				if(value == null)
					throw new ConfigurationErrorsException("The value can not be null.", new ArgumentNullException("value"));

				this[this.NamePropertyName] = value;
			}
		}

		protected internal virtual string NamePropertyName
		{
			get { return _namePropertyName; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if(!this.Initialized)
				{
					if(!base.Properties.Contains(this.NamePropertyName))
						base.Properties.Add(this.CreateNameConfigurationProperty());

					this.Initialized = true;
				}

				return base.Properties;
			}
		}

		#endregion

		#region Methods

		protected internal virtual ConfigurationProperty CreateNameConfigurationProperty()
		{
			return this.CreateNameConfigurationProperty(null, ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired, this.CreateNameConfigurationPropertyValidator());
		}

		protected internal virtual ConfigurationProperty CreateNameConfigurationProperty(object defaultValue, ConfigurationPropertyOptions options, ConfigurationValidatorBase validator)
		{
			return new ConfigurationProperty(this.NamePropertyName, typeof(string), defaultValue, null, validator, options);
		}

		protected internal virtual ConfigurationValidatorBase CreateNameConfigurationPropertyValidator()
		{
			return this.CreateNameConfigurationPropertyValidator(_invalidNameCharacters);
		}

		protected internal virtual ConfigurationValidatorBase CreateNameConfigurationPropertyValidator(string invalidNameCharacters)
		{
			return new StringValidator(1, this.MaximumNameLength, invalidNameCharacters);
		}

		#endregion
	}
}