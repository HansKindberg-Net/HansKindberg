using System;
using System.Configuration;
using HansKindberg.Configuration.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Configuration.UnitTests
{
	[TestClass]
	public class NamedConfigurationElementCollectionTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(InvalidCastException))]
		public void GetElementKey_IfTheConfigurationElementParameterValueIsNotOfTypeNamedConfigurationElement_ShouldThrowAnInvalidCastException()
		{
			new NamedConfigurationElementCollectionMock().GetElementKey(Mock.Of<ConfigurationElement>());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetElementKey_IfTheConfigurationElementParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new NamedConfigurationElementCollectionMock().GetElementKey(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "element")
					throw;
			}
		}

		[TestMethod]
		public void GetElementKey_ShouldReturnTheNameOfTheNamedConfigurationElement()
		{
			const string name = "Test";
			NamedConfigurationElementCollectionMock namedConfigurationElementCollection = new NamedConfigurationElementCollectionMock();
			NamedConfigurationElementMock namedConfigurationElement = new NamedConfigurationElementMock {Name = name};
			Assert.AreEqual(name, namedConfigurationElementCollection.GetElementKey(namedConfigurationElement));
		}

		#endregion
	}
}