using System;
using HansKindberg.Web.HtmlTransforming;
using HtmlAgilityPack;

namespace HansKindberg.Web.Tests.Configuration.Mocks
{
	[CLSCompliant(false)]
	public class HtmlTransformer : IHtmlTransformer
	{
		#region Methods

		public virtual void Transform(HtmlNode htmlNode) {}

		public virtual string Transform(string html)
		{
			return html;
		}

		#endregion
	}
}