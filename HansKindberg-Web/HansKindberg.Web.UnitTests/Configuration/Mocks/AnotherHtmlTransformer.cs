using System;
using HansKindberg.Web.HtmlTransforming;
using HtmlAgilityPack;

namespace HansKindberg.Web.UnitTests.Configuration.Mocks
{
	[CLSCompliant(false)]
	public class AnotherHtmlTransformer : IHtmlTransformer
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