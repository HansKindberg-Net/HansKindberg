using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Configuration;
using HansKindberg.Web.HtmlTransforming;

namespace HansKindberg.Web.Configuration
{
	public class HtmlTransformerElement : NamedConfigurationElement
	{
		#region Fields

		private const string _typePropertyName = "type";

		#endregion

		#region Properties

		[ConfigurationProperty(_typePropertyName, DefaultValue = null, IsRequired = true)]
		[SubclassTypeValidator(typeof(IHtmlTransformer))]
		[TypeConverter(typeof(TypeNameConverter))]
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		public virtual Type Type
		{
			get { return (Type) this[_typePropertyName]; }
			set
			{
				if(value == null)
					throw new ConfigurationErrorsException("The value can not be null.", new ArgumentNullException("value"));

				this[_typePropertyName] = value;
			}
		}

		#endregion
	}
}