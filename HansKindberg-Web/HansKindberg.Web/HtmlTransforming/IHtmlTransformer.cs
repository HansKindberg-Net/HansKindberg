using System;
using HtmlAgilityPack;

namespace HansKindberg.Web.HtmlTransforming
{
	[CLSCompliant(false)]
	public interface IHtmlTransformer
	{
		#region Methods

		void Transform(HtmlNode htmlNode);

		#endregion
	}
}