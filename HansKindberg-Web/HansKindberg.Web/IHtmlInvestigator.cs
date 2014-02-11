using System.Web;

namespace HansKindberg.Web
{
	public interface IHtmlInvestigator
	{
		#region Methods

		bool IsHtmlResponse(HttpContextBase httpContext);

		#endregion
	}
}