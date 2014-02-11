using System;
using HansKindberg.Web.Configuration;

namespace HansKindberg.Web.Samples.MvpApplication.Models
{
	public class HtmlTransformingModel
	{
		#region Fields

		private readonly HtmlTransformersSection _htmlTransformersSection;

		#endregion

		#region Constructors

		public HtmlTransformingModel(HtmlTransformersSection htmlTransformersSection)
		{
			if(htmlTransformersSection == null)
				throw new ArgumentNullException("htmlTransformersSection");

			this._htmlTransformersSection = htmlTransformersSection;
		}

		#endregion

		#region Properties

		public virtual HtmlTransformersSection HtmlTransformersSection
		{
			get { return this._htmlTransformersSection; }
		}

		#endregion
	}
}