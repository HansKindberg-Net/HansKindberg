using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using HansKindberg.Web.Configuration;
using HansKindberg.Web.UnitTests.Configuration.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.UnitTests.Configuration
{
	[TestClass]
	public class HtmlTransformerElementCollectionTest
	{
		#region Fields

		private static CultureInfo _currentCulture;
		private static CultureInfo _currentUiCulture;

		#endregion

		#region Methods

		[TestMethod]
		public void Add_IfAddingASecondItemWithTheSameNameAndType_ShouldOverwriteTheExistingItem()
		{
			const string name = "Test";
			Type type = typeof(HtmlTransformer);

			HtmlTransformerElement firstHtmlTransformerElement = new HtmlTransformerElement
			{
				Name = name,
				Type = type
			};

			HtmlTransformerElement secondHtmlTransformerElement = new HtmlTransformerElement
			{
				Name = name,
				Type = type
			};

			HtmlTransformerElementCollection htmlTransformerElementCollection = new HtmlTransformerElementCollection
			{
				firstHtmlTransformerElement,
				secondHtmlTransformerElement
			};

			Assert.AreEqual(1, htmlTransformerElementCollection.Count);
			Assert.IsTrue(ReferenceEquals(secondHtmlTransformerElement, htmlTransformerElementCollection[0]));
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Add_IfAddingASecondItemWithTheSameNameButDifferentType_ShouldThrowAConfigurationErrorsException()
		{
			const string name = "Test";

			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new HtmlTransformerElementCollection
				{
					new HtmlTransformerElement {Name = name, Type = typeof(HtmlTransformer)},
					new HtmlTransformerElement {Name = name, Type = typeof(AnotherHtmlTransformer)},
				};
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "The entry 'Test' has already been added.")
					throw;
			}
		}

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
		public void CollectionType_ShouldReturnAddRemoveClearMapByDefault()
		{
			Assert.AreEqual(ConfigurationElementCollectionType.AddRemoveClearMap, new HtmlTransformerElementCollection().CollectionType);
		}

		#endregion
	}
}