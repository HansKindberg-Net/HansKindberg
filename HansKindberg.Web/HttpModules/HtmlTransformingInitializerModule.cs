using System.Web;
using HansKindberg.Web.HtmlTransforming;

namespace HansKindberg.Web.HttpModules
{
	public class HtmlTransformingInitializerModule : IHttpModule
	{
		#region Methods

		public virtual void Dispose() {}

		public virtual void Init(HttpApplication context)
		{
			HtmlTransformingInitializer.Instance.Initialize((HttpApplicationWrapper) context);
		}

		#endregion
	}
}