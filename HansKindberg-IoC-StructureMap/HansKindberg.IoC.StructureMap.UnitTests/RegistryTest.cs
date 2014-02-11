using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace HansKindberg.IoC.StructureMap.UnitTests
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

			//Assert.IsTrue(ObjectFactory.GetInstance<ISomething>() is Something);

			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
		}

		#endregion
	}
}