using System;
using HtmlAgilityPack;

namespace HansKindberg.Web
{
	[CLSCompliant(false)]
	public interface IHtmlDocumentFactory
	{
		#region Methods

		HtmlDocument Create();

		#endregion
	}
}