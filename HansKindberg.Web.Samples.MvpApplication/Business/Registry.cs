using HansKindberg.IoC;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Models;
using StructureMapServiceLocator = HansKindberg.IoC.StructureMap.ServiceLocator;

namespace HansKindberg.Web.Samples.MvpApplication.Business
{
	public class Registry : StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		public Registry()
		{
			HansKindberg.IoC.StructureMap.Registry.Register(this);
			HansKindberg.Configuration.IoC.StructureMap.Registry.Register(this);
			HansKindberg.Web.IoC.StructureMap.Registry.Register(this);
			this.For<IModelFactory>().Singleton().Use<ModelFactory>();
			this.For<IServiceLocator>().Singleton().Use<StructureMapServiceLocator>();
		}

		#endregion
	}
}