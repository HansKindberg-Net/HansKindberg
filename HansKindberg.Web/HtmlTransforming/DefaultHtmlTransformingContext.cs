using System;
using System.Collections.Generic;
using System.Linq;
using HansKindberg.Configuration;
using HansKindberg.Web.Configuration;

namespace HansKindberg.Web.HtmlTransforming
{
	[CLSCompliant(false)]
	public class DefaultHtmlTransformingContext : IHtmlTransformingContext
	{
		#region Fields

		private readonly IConfigurationManager _configurationManager;
		private readonly IHtmlTransformerFactory _htmlTransformerFactory;
		private const string _htmlTransformersSectionName = WebSectionGroup.DefaultSectionGroupName + "/" + HtmlTransformersSection.DefaultSectionName;

		#endregion

		#region Constructors

		public DefaultHtmlTransformingContext(IConfigurationManager configurationManager, IHtmlTransformerFactory htmlTransformerFactory)
		{
			if(configurationManager == null)
				throw new ArgumentNullException("configurationManager");

			if(htmlTransformerFactory == null)
				throw new ArgumentNullException("htmlTransformerFactory");

			this._configurationManager = configurationManager;
			this._htmlTransformerFactory = htmlTransformerFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IConfigurationManager ConfigurationManager
		{
			get { return this._configurationManager; }
		}

		protected internal virtual IHtmlTransformerFactory HtmlTransformerFactory
		{
			get { return this._htmlTransformerFactory; }
		}

		protected internal virtual string HtmlTransformersSectionName
		{
			get { return _htmlTransformersSectionName; }
		}

		#endregion

		#region Methods

		public virtual IEnumerable<IHtmlTransformer> GetTransformers()
		{
			HtmlTransformersSection htmlTransformersSection = (HtmlTransformersSection) this.ConfigurationManager.GetSection(this.HtmlTransformersSectionName) ?? new HtmlTransformersSection();

			return htmlTransformersSection.HtmlTransformers.Select(htmlTransformerElement => this.HtmlTransformerFactory.Create(htmlTransformerElement.Type));
		}

		#endregion
	}
}