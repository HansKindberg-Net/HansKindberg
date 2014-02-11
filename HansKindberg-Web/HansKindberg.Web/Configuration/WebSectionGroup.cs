using System.Configuration;

namespace HansKindberg.Web.Configuration
{
	public class WebSectionGroup : ConfigurationSectionGroup
	{
		#region Fields

		public const string DefaultSectionGroupName = "hansKindberg.web";

		#endregion

		#region Properties

		[ConfigurationProperty(HtmlTransformersSection.DefaultSectionName)]
		public virtual HtmlTransformersSection HtmlTransformers
		{
			get { return (HtmlTransformersSection) this.Sections[HtmlTransformersSection.DefaultSectionName]; }
		}

		#endregion
	}
}