using System;
using System.Net.Mime;
using System.Web;

namespace HansKindberg.Web
{
	public class DefaultHtmlInvestigator : IHtmlInvestigator
	{
		#region Methods

		public virtual bool IsHtmlResponse(HttpContextBase httpContext)
		{
			if(httpContext == null)
				throw new ArgumentNullException("httpContext");

			return string.Equals(httpContext.Response.ContentType, MediaTypeNames.Text.Html, StringComparison.OrdinalIgnoreCase);
		}

		#endregion
	}
}