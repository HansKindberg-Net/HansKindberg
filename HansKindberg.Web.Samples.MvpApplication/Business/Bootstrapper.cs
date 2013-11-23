using HansKindberg.IoC;
using HansKindberg.Web.HtmlTransforming;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.IoC.StructureMap.Binder;
using StructureMap;
using WebFormsMvp.Binder;
using StructureMapServiceLocator = HansKindberg.IoC.StructureMap.ServiceLocator;

namespace HansKindberg.Web.Samples.MvpApplication.Business
{
	public class Bootstrapper : IBootstrapper
	{
		#region Fields

		private static bool _hasStarted;

		#endregion

		#region Methods

		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();

			IContainer container = ObjectFactory.Container;

			HtmlTransformingInitializer.Instance = container.GetInstance<IHtmlTransformingInitializer>();
			PresenterBinder.Factory = new PresenterFactory(container);
			ServiceLocator.Instance = new StructureMapServiceLocator(container);
		}

		public void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(initializer => { initializer.PullConfigurationFromAppConfig = true; });
		}

		public static void Restart()
		{
			if(_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		#endregion
	}
}