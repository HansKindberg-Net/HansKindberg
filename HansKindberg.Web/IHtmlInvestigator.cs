using System.Web;

namespace HansKindberg.Web
{
	public interface IHtmlInvestigator
	{
		#region Methods

		bool IsHtmlRequest(HttpContextBase httpContext);

		#endregion
	}
}