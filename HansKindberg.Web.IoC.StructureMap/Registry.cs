using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.SessionState;
using HansKindberg.Web.HtmlTransforming;
using StructureMap.Configuration.DSL;

namespace HansKindberg.Web.IoC.StructureMap
{
	[CLSCompliant(false)]
	public abstract class Registry : global::StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			Register(this);
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		public static void Register(IRegistry registry)
		{
			if(registry == null)
				throw new ArgumentNullException("registry");

			registry.For<HttpApplicationState>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Application);
			registry.For<HttpApplicationStateBase>().HybridHttpOrThreadLocalScoped().Use<HttpApplicationStateWrapper>();
			registry.For<HttpContext>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current);
			registry.For<HttpContextBase>().HybridHttpOrThreadLocalScoped().Use<HttpContextWrapper>();
			registry.For<HttpRequest>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Request);
			registry.For<HttpRequestBase>().HybridHttpOrThreadLocalScoped().Use<HttpRequestWrapper>();
			registry.For<HttpResponse>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Response);
			registry.For<HttpResponseBase>().HybridHttpOrThreadLocalScoped().Use<HttpResponseWrapper>();
			registry.For<HttpServerUtility>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Server);
			registry.For<HttpServerUtilityBase>().HybridHttpOrThreadLocalScoped().Use<HttpServerUtilityWrapper>();
			registry.For<HttpSessionState>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Session);
			registry.For<HttpSessionStateBase>().HybridHttpOrThreadLocalScoped().Use<HttpSessionStateWrapper>();

			registry.For<IHtmlDocumentFactory>().Singleton().Use<DefaultHtmlDocumentFactory>();
			registry.For<IHtmlInvestigator>().Singleton().Use<DefaultHtmlInvestigator>();
			registry.For<IHtmlTransformerFactory>().Singleton().Use<DefaultHtmlTransformerFactory>();
			registry.For<IHtmlTransformingContext>().Singleton().Use<DefaultHtmlTransformingContext>();
			registry.For<IHtmlTransformingInitializer>().Singleton().Use<DefaultHtmlTransformingInitializer>();
		}

		#endregion
	}
}