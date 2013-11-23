using System;
using HansKindberg.Web.HtmlTransforming;
using HtmlAgilityPack;

namespace HansKindberg.Web.Samples.MvpApplication.Business.Web.HtmlTransforming
{
	public class SecondHtmlTransformer : IHtmlTransformer
	{
		#region Methods

		public virtual void Transform(HtmlNode htmlNode)
		{
			if(htmlNode == null)
				throw new ArgumentNullException("htmlNode");

			foreach (var child in htmlNode.SelectNodes("//div[@id='html-transforming-result']") ?? new HtmlNodeCollection(null))
			{
				child.AppendChild(HtmlNode.CreateNode("<p>This text is added by the second html-transformer.</p>"));
			}
		}

		#endregion
	}
}