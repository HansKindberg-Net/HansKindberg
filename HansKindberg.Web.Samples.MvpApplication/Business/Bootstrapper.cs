using HansKindberg.IoC;
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
			PresenterBinder.Factory = new PresenterFactory(ObjectFactory.Container);
			ServiceLocator.Instance = new StructureMapServiceLocator(ObjectFactory.Container);
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