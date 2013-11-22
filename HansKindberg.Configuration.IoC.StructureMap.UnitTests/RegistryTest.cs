using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace HansKindberg.Configuration.IoC.StructureMap.UnitTests
{
	[TestClass]
	public class RegistryTest
	{
		#region Methods

		[TestMethod]
		public void Register_ShouldRegisterTypes()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			ObjectFactory.Initialize(Registry.Register);

			Assert.IsTrue(ObjectFactory.GetInstance<IConfigurationManager>() is ConfigurationManagerWrapper);

			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
		}

		#endregion
	}
}