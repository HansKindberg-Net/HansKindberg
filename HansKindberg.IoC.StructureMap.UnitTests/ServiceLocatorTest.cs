using System;
using System.Linq;
using HansKindberg.IoC.StructureMap.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace HansKindberg.IoC.StructureMap.UnitTests
{
	[TestClass]
	public class ServiceLocatorTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(StructureMapException))]
		public void GetService_WithTypeParameter_IfTheTypeIsAbstractAndIfTheTypeIsNotRegistered_ShouldThrowAnException()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
			new ServiceLocator(ObjectFactory.Container).GetService(typeof(SomeAbstractClass));
		}

		[TestMethod]
		[ExpectedException(typeof(StructureMapException))]
		public void GetService_WithTypeParameter_IfTheTypeIsAnInterfaceAndIfTheTypeIsNotRegistered_ShouldThrowAnException()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
			new ServiceLocator(ObjectFactory.Container).GetService(typeof(ISomeInterface));
		}

		[TestMethod]
		public void GetService_WithTypeParameter_IfTheTypeIsConcreteWithParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldReturnAnInstanceOfThatType()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			object instance = new ServiceLocator(ObjectFactory.Container).GetService(typeof(SomeConcreteClassWithParameterlessConstructor));

			Assert.IsNotNull(instance);
			Assert.IsTrue(instance is SomeConcreteClassWithParameterlessConstructor);
		}

		[TestMethod]
		[ExpectedException(typeof(StructureMapException))]
		public void GetService_WithTypeParameter_IfTheTypeIsConcreteWithoutParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldThrowAnException()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
			new ServiceLocator(ObjectFactory.Container).GetService(typeof(SomeConcreteClassWithoutParameterlessConstructor));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetService_WithTypeParameter_IfTheTypeParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			new ServiceLocator(ObjectFactory.Container).GetService(null);
		}

		[TestMethod]
		public void GetServices_WithTypeParameter_IfTheTypeIsAbstractAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new ServiceLocator(ObjectFactory.Container).GetServices(typeof(SomeAbstractClass)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		public void GetServices_WithTypeParameter_IfTheTypeIsAnInterfaceAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new ServiceLocator(ObjectFactory.Container).GetServices(typeof(ISomeInterface)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		public void GetServices_WithTypeParameter_IfTheTypeIsConcreteWithParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new ServiceLocator(ObjectFactory.Container).GetServices(typeof(SomeConcreteClassWithParameterlessConstructor)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		public void GetServices_WithTypeParameter_IfTheTypeIsConcreteWithParameterlessConstructorAndIfTheTypeIsRegistered_ShouldReturnACollectionWithAnInstanceOfThatType()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			ObjectFactory.Initialize(initializer => initializer.For<SomeConcreteClassWithParameterlessConstructor>().Use<SomeConcreteClassWithParameterlessConstructor>());
			var instances = new ServiceLocator(ObjectFactory.Container).GetServices(typeof(SomeConcreteClassWithParameterlessConstructor)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsTrue(instances.Count() == 1);
			Assert.IsTrue(instances.ElementAt(0) is SomeConcreteClassWithParameterlessConstructor);
		}

		[TestMethod]
		public void GetServices_WithTypeParameter_IfTheTypeIsConcreteWithoutParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new ServiceLocator(ObjectFactory.Container).GetServices(typeof(SomeConcreteClassWithoutParameterlessConstructor)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetServices_WithTypeParameter_IfTheTypeParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			new ServiceLocator(ObjectFactory.Container).GetServices(null);
		}

		#endregion
	}
}