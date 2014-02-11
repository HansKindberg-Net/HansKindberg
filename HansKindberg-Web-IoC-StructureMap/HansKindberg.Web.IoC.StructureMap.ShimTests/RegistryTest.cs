using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Fakes;
using System.Web.SessionState;
using System.Web.SessionState.Fakes;
using HansKindberg.Configuration;
using HansKindberg.Web.HtmlTransforming;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace HansKindberg.Web.IoC.StructureMap.ShimTests
{
	[TestClass]
	public class RegistryTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		public void Register_ShouldRegisterTypes()
		{
			using(ShimsContext.Create())
			{
				ShimHttpContext.CurrentGet = () => new ShimHttpContext
					{
						ApplicationGet = () => new ShimHttpApplicationState(),
						ItemsGet = () => new Hashtable(),
						RequestGet = () => new ShimHttpRequest(),
						ResponseGet = () => new ShimHttpResponse(),
						ServerGet = () => new ShimHttpServerUtility(),
						SessionGet = () => new ShimHttpSessionState()
					};

				TestHelper.ClearStructureMap();
				TestHelper.AssertStructureMapIsCleared();

				ObjectFactory.Initialize(initializer =>
				{
					initializer.For<IConfigurationManager>().Singleton().Use<ConfigurationManagerWrapper>();
					Registry.Register(initializer);
				});

				IContainer container = ObjectFactory.Container;

				Assert.IsNotNull(container.GetInstance<HttpApplicationState>());
				Assert.IsNotNull(container.GetInstance<HttpApplicationStateBase>());

				Assert.IsNotNull(container.GetInstance<HttpContext>());
				Assert.IsNotNull(container.GetInstance<HttpContextBase>());

				Assert.IsNotNull(container.GetInstance<HttpRequest>());
				Assert.IsNotNull(container.GetInstance<HttpRequestBase>());

				Assert.IsNotNull(container.GetInstance<HttpResponse>());
				Assert.IsNotNull(container.GetInstance<HttpResponseBase>());

				Assert.IsNotNull(container.GetInstance<HttpServerUtility>());
				Assert.IsNotNull(container.GetInstance<HttpServerUtilityBase>());

				Assert.IsNotNull(container.GetInstance<HttpSessionState>());
				Assert.IsNotNull(container.GetInstance<HttpSessionStateBase>());

				Assert.IsTrue(container.GetInstance<IHtmlDocumentFactory>() is DefaultHtmlDocumentFactory);
				Assert.IsTrue(container.GetInstance<IHtmlInvestigator>() is DefaultHtmlInvestigator);
				Assert.IsTrue(container.GetInstance<IHtmlTransformerFactory>() is DefaultHtmlTransformerFactory);
				Assert.IsTrue(container.GetInstance<IHtmlTransformingContext>() is DefaultHtmlTransformingContext);
				Assert.IsTrue(container.GetInstance<IHtmlTransformingInitializer>() is DefaultHtmlTransformingInitializer);

				TestHelper.ClearStructureMap();
				TestHelper.AssertStructureMapIsCleared();
			}
		}

		#endregion
	}
}