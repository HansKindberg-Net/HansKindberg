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

				Assert.IsNotNull(ObjectFactory.GetInstance<HttpApplicationState>());
				Assert.IsNotNull(ObjectFactory.GetInstance<HttpApplicationStateBase>());

				Assert.IsNotNull(ObjectFactory.GetInstance<HttpContext>());
				Assert.IsNotNull(ObjectFactory.GetInstance<HttpContextBase>());

				Assert.IsNotNull(ObjectFactory.GetInstance<HttpRequest>());
				Assert.IsNotNull(ObjectFactory.GetInstance<HttpRequestBase>());

				Assert.IsNotNull(ObjectFactory.GetInstance<HttpResponse>());
				Assert.IsNotNull(ObjectFactory.GetInstance<HttpResponseBase>());

				Assert.IsNotNull(ObjectFactory.GetInstance<HttpServerUtility>());
				Assert.IsNotNull(ObjectFactory.GetInstance<HttpServerUtilityBase>());

				Assert.IsNotNull(ObjectFactory.GetInstance<HttpSessionState>());
				Assert.IsNotNull(ObjectFactory.GetInstance<HttpSessionStateBase>());

				Assert.IsTrue(ObjectFactory.GetInstance<IHtmlDocumentFactory>() is DefaultHtmlDocumentFactory);
				Assert.IsTrue(ObjectFactory.GetInstance<IHtmlInvestigator>() is DefaultHtmlInvestigator);
				Assert.IsTrue(ObjectFactory.GetInstance<IHtmlTransformerFactory>() is DefaultHtmlTransformerFactory);
				Assert.IsTrue(ObjectFactory.GetInstance<IHtmlTransformingContext>() is DefaultHtmlTransformingContext);

				TestHelper.ClearStructureMap();
				TestHelper.AssertStructureMapIsCleared();
			}
		}

		#endregion
	}
}