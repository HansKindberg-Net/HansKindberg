using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using HansKindberg.Configuration.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Configuration.UnitTests
{
	[TestClass]
	public class NamedConfigurationElementTest
	{
		#region Methods

		private static NamedConfigurationElement CreateNamedConfigurationElement()
		{
			return new Mock<NamedConfigurationElement> {CallBase = true}.Object;
		}

		[TestMethod]
		public void Equals_IfTheNamesAreEqual_ShouldReturnTrue()
		{
			const string name = "Test";

			NamedConfigurationElement firstNamedConfigurationElement = CreateNamedConfigurationElement();
			firstNamedConfigurationElement.Name = name;

			NamedConfigurationElement secondNamedConfigurationElement = CreateNamedConfigurationElement();
			secondNamedConfigurationElement.Name = name;

			Assert.AreEqual(firstNamedConfigurationElement, secondNamedConfigurationElement);
			Assert.IsTrue(firstNamedConfigurationElement.Equals(secondNamedConfigurationElement));
		}

		[TestMethod]
		public void Equals_ShouldBeNameCaseSensitive()
		{
			NamedConfigurationElement firstNamedConfigurationElement = CreateNamedConfigurationElement();
			firstNamedConfigurationElement.Name = "Test";

			NamedConfigurationElement secondNamedConfigurationElement = CreateNamedConfigurationElement();
			secondNamedConfigurationElement.Name = "test";

			Assert.AreNotEqual(firstNamedConfigurationElement, secondNamedConfigurationElement);
			Assert.IsFalse(firstNamedConfigurationElement.Equals(secondNamedConfigurationElement));
		}

		[TestMethod]
		public void Name_Get_ShouldReturnNullByDefault()
		{
			Assert.IsNull(CreateNamedConfigurationElement().Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_Set_IfTheValueParameterIsAnEmptyString_ShouldThrowAConfigurationErrorsException()
		{
			NamedConfigurationElement namedConfigurationElement = CreateNamedConfigurationElement();
			namedConfigurationElement.Name = string.Empty;
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_Set_IfTheValueParameterIsAnInvalidCharacter_ShouldThrowAConfigurationErrorsException()
		{
			NamedConfigurationElement namedConfigurationElement = CreateNamedConfigurationElement();
			int configurationErrorsExceptionThrown = 0;

			for(int i = 0; i < namedConfigurationElement.InvalidNameCharacters.Length; i++)
			{
				// ReSharper disable EmptyGeneralCatchClause
				try
				{
					namedConfigurationElement.Name = namedConfigurationElement.InvalidNameCharacters[i].ToString(CultureInfo.InvariantCulture);
				}
				catch(ConfigurationErrorsException)
				{
					configurationErrorsExceptionThrown++;
				}
				// ReSharper restore EmptyGeneralCatchClause
			}

			if(configurationErrorsExceptionThrown == namedConfigurationElement.InvalidNameCharacters.Length)
				throw new ConfigurationErrorsException();
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_Set_IfTheValueParameterIsNull_ShouldThrowAConfigurationErrorsException()
		{
			NamedConfigurationElement namedConfigurationElement = CreateNamedConfigurationElement();
			try
			{
				namedConfigurationElement.Name = null;
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.Message != "The value can not be null.")
					return;

				ArgumentNullException innerException = configurationErrorsException.InnerException as ArgumentNullException;

				if(innerException != null && innerException.ParamName == "value")
					throw;
			}
		}

		[TestMethod]
		public void Name_Set_ShouldSetTheName()
		{
			const string name = "Test";
			NamedConfigurationElement namedConfigurationElement = CreateNamedConfigurationElement();
			namedConfigurationElement.Name = name;
			Assert.AreEqual(name, namedConfigurationElement.Name);
		}

		[TestMethod]
		public void Name_ShouldBeAbleToOverride()
		{
			AlreadyNamedConfigurationElementMock namedConfigurationElement = new AlreadyNamedConfigurationElementMock();

			Assert.AreEqual(AlreadyNamedConfigurationElementMock.DefaultValue, namedConfigurationElement.Name);

			Assert.AreEqual(1, namedConfigurationElement.Properties.Count);

			ConfigurationProperty nameConfigurationProperty = namedConfigurationElement.Properties.Cast<ConfigurationProperty>().First();

			Assert.AreEqual("name", nameConfigurationProperty.Name);

			StringValidator stringValidator = (StringValidator) nameConfigurationProperty.Validator;

			// ReSharper disable PossibleNullReferenceException
			string invalidCharacters = (string) typeof(StringValidator).GetField("_invalidChars", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(stringValidator);
			Assert.AreEqual(AlreadyNamedConfigurationElementMock.InvalidCharacters, invalidCharacters);

			int maxLength = (int) typeof(StringValidator).GetField("_maxLength", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(stringValidator);
			Assert.AreEqual(AlreadyNamedConfigurationElementMock.MaxLength, maxLength);

			int minLength = (int) typeof(StringValidator).GetField("_minLength", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(stringValidator);
			Assert.AreEqual(AlreadyNamedConfigurationElementMock.MinLength, minLength);
			// ReSharper restore PossibleNullReferenceException
		}

		[TestMethod]
		public void Prerequisite_ConfigurationPropertyCollection_Add_IfAddingPropertiesWithTheSameName_ShouldOnlyAddTheFirst()
		{
			ConfigurationProperty firstConfigurationProperty = new ConfigurationProperty("First", typeof(object));
			ConfigurationProperty secondConfigurationProperty = new ConfigurationProperty("First", typeof(string));

			ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection
				{
					firstConfigurationProperty,
					secondConfigurationProperty
				};

			Assert.AreEqual(1, configurationPropertyCollection.Count);
			Assert.AreEqual(firstConfigurationProperty, configurationPropertyCollection.Cast<ConfigurationProperty>().First());
			Assert.AreEqual(typeof(object), firstConfigurationProperty.Type);
		}

		[TestMethod]
		public void Prerequisite_ConfigurationPropertyCollection_Contains_IsCaseSensitive()
		{
			ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection
				{
					new ConfigurationProperty("First", typeof(object)),
				};

			Assert.IsTrue(configurationPropertyCollection.Contains("First"));
			Assert.IsFalse(configurationPropertyCollection.Contains("first"));
		}

		[TestMethod]
		public void Prerequisite_ConfigurationProperty_DefaultValue_Get_IfTheConstructorWithTwoParametersIsUsedAndTheTypeParameterIsOfTypeString_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new ConfigurationProperty("Test", typeof(string)).DefaultValue);
		}

		[TestMethod]
		public void Properties_ShouldOnlyContainTheNameProperty()
		{
			NamedConfigurationElementMock namedConfigurationElement = new NamedConfigurationElementMock();
			Assert.AreEqual(1, namedConfigurationElement.Properties.Count);
			Assert.AreEqual(namedConfigurationElement.NamePropertyName, namedConfigurationElement.Properties.Cast<ConfigurationProperty>().First().Name);
		}

		#endregion
	}
}