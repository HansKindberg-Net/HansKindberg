using System;
using HtmlAgilityPack;

namespace HansKindberg.Web
{
	[CLSCompliant(false)]
	public class DefaultHtmlDocumentFactory : IHtmlDocumentFactory
	{
		#region Methods

		public virtual HtmlDocument Create()
		{
			return new HtmlDocument();
		}

		#endregion
	}
}