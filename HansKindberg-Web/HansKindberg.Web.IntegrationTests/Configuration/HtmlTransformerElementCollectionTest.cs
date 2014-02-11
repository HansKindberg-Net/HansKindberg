using System;
using System.Configuration;
using System.Globalization;
using HansKindberg.Web.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.IntegrationTests.Configuration
{
	[TestClass]
	public class HtmlTransformerElementCollectionTest
	{
		#region Fields

		private const string _htmlTransformersSectionPath = "hansKindberg.web/htmlTransformers";

		#endregion

		#region Methods

		[TestMethod]
		public void Configuration_GetHtmlTransformerSection_HtmlTransformers_IfThereAreMultipleItemsWithTheSameCaseSensitiveNameAndTheSameType_ShouldOnlyIncludeTheFirstItem()
		{
			System.Configuration.Configuration configuration = CreateConfiguration("TripletItems");
			HtmlTransformersSection htmlTransformersSection = (HtmlTransformersSection) configuration.GetSection(_htmlTransformersSectionPath);
			HtmlTransformerElementCollection htmlTransformerElementCollection = htmlTransformersSection.HtmlTransformers;
			Assert.AreEqual(5, htmlTransformerElementCollection.Count);
			Assert.AreEqual("test", htmlTransformerElementCollection[0].Name);
			Assert.AreEqual("Test", htmlTransformerElementCollection[1].Name);
			Assert.AreEqual("TeSt", htmlTransformerElementCollection[2].Name);
			Assert.AreEqual("TEsT", htmlTransformerElementCollection[3].Name);
			Assert.AreEqual("TEST", htmlTransformerElementCollection[4].Name);
		}

		private static System.Configuration.Configuration CreateConfiguration(string configurationFileNameIdentity)
		{
			ConfigurationFileMap configurationFileMap = new ConfigurationFileMap
				{
					MachineConfigFilename = string.Format(CultureInfo.InvariantCulture, @"{0}\Configuration\Test-configurations\HtmlTransformerElementCollectionTest.{1}.Web.config", AppDomain.CurrentDomain.BaseDirectory, configurationFileNameIdentity)
				};

			return ConfigurationManager.OpenMappedMachineConfiguration(configurationFileMap);
		}

		#endregion
	}
}