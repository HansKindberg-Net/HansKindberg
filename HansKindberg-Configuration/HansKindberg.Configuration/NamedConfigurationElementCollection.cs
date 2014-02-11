using System;
using System.Configuration;

namespace HansKindberg.Configuration
{
	public abstract class NamedConfigurationElementCollection<T> : ConfigurationElementCollection<T> where T : NamedConfigurationElement, new()
	{
		#region Methods

		protected override object GetElementKey(ConfigurationElement element)
		{
			if(element == null)
				throw new ArgumentNullException("element");

			return ((NamedConfigurationElement) element).Name;
		}

		#endregion
	}
}