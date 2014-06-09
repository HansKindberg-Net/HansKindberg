using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml;
using HansKindberg.Web.Configuration;
using HansKindberg.Web.IntegrationTests.Configuration.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.IntegrationTests.Configuration
{
	[TestClass]
	public class HtmlTransformerElementTest
	{
		#region Fields

		private static CultureInfo _currentCulture;
		private static CultureInfo _currentUiCulture;
		private const string _htmlTransformersSectionPath = "hansKindberg.web/htmlTransformers";

		#endregion

		#region Methods

		[ClassCleanup]
		public static void ClassCleanup()
		{
			Thread.CurrentThread.CurrentCulture = _currentCulture;
			Thread.CurrentThread.CurrentUICulture = _currentUiCulture;
		}

		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			_currentCulture = Thread.CurrentThread.CurrentCulture;
			_currentUiCulture = Thread.CurrentThread.CurrentUICulture;

			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_HtmlTransformers_Item_Type_Get_IfTheTypeAttributeIsEmpty_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("TheTypeAttributeIsEmpty");
				HtmlTransformersSection htmlTransformersSection = (HtmlTransformersSection) configuration.GetSection(_htmlTransformersSectionPath);
				HtmlTransformerElementCollection htmlTransformerElementCollection = htmlTransformersSection.HtmlTransformers;
				HtmlTransformerElement htmlTransformerElement = htmlTransformerElementCollection[0];
				Assert.IsNull(htmlTransformerElement.Type);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "The value of the property 'type' cannot be parsed. The error is: The type '' cannot be resolved. Please verify the spelling is correct or that the full type name is provided.")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_HtmlTransformers_Item_Type_Get_IfTheValueOfTheNameAttributeIsValidAndIfTheValueOfTheTypeAttributeIsAHtmlTransformerType_ShouldReturnTheHtmlTransformerType()
		{
			System.Configuration.Configuration configuration = CreateConfiguration("TheValueOfTheTypeAttributeIsNotAHtmlTransformerType");
			HtmlTransformersSection htmlTransformersSection = (HtmlTransformersSection) configuration.GetSection(_htmlTransformersSectionPath);
			HtmlTransformerElementCollection htmlTransformerElementCollection = htmlTransformersSection.HtmlTransformers;
			HtmlTransformerElement htmlTransformerElement = htmlTransformerElementCollection[0];
			Assert.AreEqual(typeof(HtmlTransformer), htmlTransformerElement.Type);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_HtmlTransformers_Item_Type_Get_IfTheValueOfTheTypeAttributeIsNotAHtmlTransformerType_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("TheValueOfTheTypeAttributeIsNotAHtmlTransformerType");
				HtmlTransformersSection htmlTransformersSection = (HtmlTransformersSection) configuration.GetSection(_htmlTransformersSectionPath);
				HtmlTransformerElementCollection htmlTransformerElementCollection = htmlTransformersSection.HtmlTransformers;
				HtmlTransformerElement htmlTransformerElement = htmlTransformerElementCollection[0];
				Assert.IsNull(htmlTransformerElement.Type);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "The value for the property 'type' is not valid. The error is: The type 'HansKindberg.Web.IntegrationTests.Configuration.Mocks.InvalidHtmlTransformer' must be derived from the type 'HansKindberg.Web.HtmlTransforming.IHtmlTransformer'.")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_HtmlTransformers_Item_Type_Get_IfTheValueOfTheTypeAttributeIsNotAType_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("TheValueOfTheTypeAttributeIsNotAType");
				HtmlTransformersSection htmlTransformersSection = (HtmlTransformersSection) configuration.GetSection(_htmlTransformersSectionPath);
				HtmlTransformerElementCollection htmlTransformerElementCollection = htmlTransformersSection.HtmlTransformers;
				HtmlTransformerElement htmlTransformerElement = htmlTransformerElementCollection[0];
				Assert.IsNull(htmlTransformerElement.Type);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "The value of the property 'type' cannot be parsed. The error is: The type 'Test' cannot be resolved. Please verify the spelling is correct or that the full type name is provided.")
					throw;
			}
		}

		[TestMethod]
		public void Configuration_Save_ShouldSaveTheTypeAsAnAssemblyQualifiedName()
		{
			System.Configuration.Configuration configuration = CreateConfiguration("Save");
			HtmlTransformersSection htmlTransformersSection = (HtmlTransformersSection) configuration.GetSection(_htmlTransformersSectionPath);
			htmlTransformersSection.HtmlTransformers.Add(new HtmlTransformerElement {Name = "Test", Type = typeof(HtmlTransformer)});
			configuration.Save();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(configuration.FilePath);
			// ReSharper disable PossibleNullReferenceException
			Assert.AreEqual(typeof(HtmlTransformer).AssemblyQualifiedName, xmlDocument.GetElementsByTagName("add").Cast<XmlNode>().First().Attributes["type"].Value);
			// ReSharper restore PossibleNullReferenceException
		}

		private static System.Configuration.Configuration CreateConfiguration(string configurationFileNameIdentity)
		{
			ConfigurationFileMap configurationFileMap = new ConfigurationFileMap
			{
				MachineConfigFilename = string.Format(CultureInfo.InvariantCulture, @"{0}\Configuration\Test-configurations\HtmlTransformerElementTest.{1}.Web.config", AppDomain.CurrentDomain.BaseDirectory, configurationFileNameIdentity)
			};

			return ConfigurationManager.OpenMappedMachineConfiguration(configurationFileMap);
		}

		#endregion
	}
}