using System;
using StructureMap.Configuration.DSL;

namespace HansKindberg.Configuration.IoC.StructureMap
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

		public static void Register(IRegistry registry)
		{
			if(registry == null)
				throw new ArgumentNullException("registry");

			registry.For<IConfigurationManager>().Singleton().Use<ConfigurationManagerWrapper>();
		}

		#endregion
	}
}