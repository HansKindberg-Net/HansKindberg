using System;
using System.Linq;
using HansKindberg.IoC.StructureMap.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace HansKindberg.IoC.StructureMap.UnitTests
{
	[TestClass]
	public class DependencyResolverTest
	{
		#region Methods

		[TestMethod]
		public void GetService_IfTheTypeIsAbstractAndIfTheTypeIsNotRegistered_ShouldReturnNull()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
			Assert.IsNull(new DependencyResolver(ObjectFactory.Container).GetService(typeof(SomeAbstractClass)));
		}

		[TestMethod]
		public void GetService_IfTheTypeIsAnInterfaceAndIfTheTypeIsNotRegistered_ShouldReturnNull()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
			Assert.IsNull(new DependencyResolver(ObjectFactory.Container).GetService(typeof(ISomeInterface)));
		}

		[TestMethod]
		public void GetService_IfTheTypeIsConcreteWithParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldReturnAnInstanceOfThatType()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			object instance = new DependencyResolver(ObjectFactory.Container).GetService(typeof(SomeConcreteClassWithParameterlessConstructor));

			Assert.IsNotNull(instance);
			Assert.IsTrue(instance is SomeConcreteClassWithParameterlessConstructor);
		}

		[TestMethod]
		public void GetService_IfTheTypeIsConcreteWithoutParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldReturnNull()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();
			Assert.IsNull(new DependencyResolver(ObjectFactory.Container).GetService(typeof(SomeConcreteClassWithoutParameterlessConstructor)));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetService_IfTheTypeParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			new DependencyResolver(ObjectFactory.Container).GetService(null);
		}

		[TestMethod]
		public void GetServices_IfTheTypeIsAbstractAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new DependencyResolver(ObjectFactory.Container).GetServices(typeof(SomeAbstractClass)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		public void GetServices_IfTheTypeIsAnInterfaceAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new DependencyResolver(ObjectFactory.Container).GetServices(typeof(ISomeInterface)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		public void GetServices_IfTheTypeIsConcreteWithParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new DependencyResolver(ObjectFactory.Container).GetServices(typeof(SomeConcreteClassWithParameterlessConstructor)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		public void GetServices_IfTheTypeIsConcreteWithParameterlessConstructorAndIfTheTypeIsRegistered_ShouldReturnACollectionWithAnInstanceOfThatType()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			ObjectFactory.Initialize(initializer => initializer.For<SomeConcreteClassWithParameterlessConstructor>().Use<SomeConcreteClassWithParameterlessConstructor>());
			var instances = new DependencyResolver(ObjectFactory.Container).GetServices(typeof(SomeConcreteClassWithParameterlessConstructor)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsTrue(instances.Count() == 1);
			Assert.IsTrue(instances.ElementAt(0) is SomeConcreteClassWithParameterlessConstructor);
		}

		[TestMethod]
		public void GetServices_IfTheTypeIsConcreteWithoutParameterlessConstructorAndIfTheTypeIsNotRegistered_ShouldReturnAnEmptyCollection()
		{
			TestHelper.ClearStructureMap();
			TestHelper.AssertStructureMapIsCleared();

			var instances = new DependencyResolver(ObjectFactory.Container).GetServices(typeof(SomeConcreteClassWithoutParameterlessConstructor)).ToArray();

			Assert.IsNotNull(instances);
			Assert.IsFalse(instances.Any());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetServices_IfTheTypeParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			new DependencyResolver(ObjectFactory.Container).GetServices(null);
		}

		#endregion
	}
}