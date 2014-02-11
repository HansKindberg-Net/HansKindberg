using System.Configuration;

namespace HansKindberg.Web.Configuration
{
	public class HtmlTransformersSection : ConfigurationSection
	{
		#region Fields

		public const string DefaultSectionName = "htmlTransformers";
		private const string _htmlTransformersPropertyName = "";

		#endregion

		#region Properties

		[ConfigurationProperty(_htmlTransformersPropertyName, IsDefaultCollection = true)]
		public virtual HtmlTransformerElementCollection HtmlTransformers
		{
			get { return (HtmlTransformerElementCollection) this[_htmlTransformersPropertyName]; }
		}

		#endregion
	}
}