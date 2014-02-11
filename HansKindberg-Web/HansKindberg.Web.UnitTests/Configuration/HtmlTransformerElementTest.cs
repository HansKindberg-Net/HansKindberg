using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using HansKindberg.Web.Configuration;
using HansKindberg.Web.HtmlTransforming;
using HansKindberg.Web.UnitTests.Configuration.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Web.UnitTests.Configuration
{
	[TestClass]
	public class HtmlTransformerElementTest
	{
		#region Fields

		private static CultureInfo _currentCulture;
		private static CultureInfo _currentUiCulture;

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
		public void Equals_IfTheNamesAndTypesAreEqual_ShouldReturnTrue()
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

			Assert.AreEqual(firstHtmlTransformerElement, secondHtmlTransformerElement);
			Assert.IsTrue(firstHtmlTransformerElement.Equals(secondHtmlTransformerElement));
		}

		[TestMethod]
		public void Equals_ShouldBeNameCaseSensitive()
		{
			Type type = typeof(HtmlTransformer);

			HtmlTransformerElement firstHtmlTransformerElement = new HtmlTransformerElement
				{
					Name = "Test",
					Type = type
				};

			HtmlTransformerElement secondHtmlTransformerElement = new HtmlTransformerElement
				{
					Name = "test",
					Type = type
				};

			Assert.AreNotEqual(firstHtmlTransformerElement, secondHtmlTransformerElement);
			Assert.IsFalse(firstHtmlTransformerElement.Equals(secondHtmlTransformerElement));
		}

		[TestMethod]
		public void Name_Get_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new HtmlTransformerElement().Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_Set_IfTheValueParameterIsAnEmptyString_ShouldThrowAConfigurationErrorsException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new HtmlTransformerElement
				{
					Name = string.Empty
				};
			// ReSharper restore ObjectCreationAsStatement
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_Set_IfTheValueParameterIsAnInvalidCharacter_ShouldThrowAConfigurationErrorsException()
		{
			HtmlTransformerElement htmlTransformerElement = new HtmlTransformerElement();
			int configurationErrorsExceptionThrown = 0;

			for(int i = 0; i < htmlTransformerElement.InvalidNameCharacters.Length; i++)
			{
				// ReSharper disable EmptyGeneralCatchClause
				try
				{
					htmlTransformerElement.Name = htmlTransformerElement.InvalidNameCharacters[i].ToString(CultureInfo.InvariantCulture);
				}
				catch(ConfigurationErrorsException)
				{
					configurationErrorsExceptionThrown++;
				}
				// ReSharper restore EmptyGeneralCatchClause
			}

			if(configurationErrorsExceptionThrown == htmlTransformerElement.InvalidNameCharacters.Length)
				throw new ConfigurationErrorsException();
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_Set_IfTheValueParameterIsNull_ShouldThrowAConfigurationErrorsException()
		{
			HtmlTransformerElement htmlTransformerElement = new HtmlTransformerElement();
			try
			{
				htmlTransformerElement.Name = null;
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
			HtmlTransformerElement htmlTransformerElement = new HtmlTransformerElement
				{
					Name = name
				};
			Assert.AreEqual(name, htmlTransformerElement.Name);
		}

		[TestMethod]
		public void Type_Get_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new HtmlTransformerElement().Type);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TypeSet")]
		public void Type_Set_IfNotTypeOfIHtmlTransformerIsAssignableFromTheValueParameter_ShouldThrowAConfigurationErrorsException()
		{
			Type type = typeof(object);

			HtmlTransformerElement htmlTransformerElement = new HtmlTransformerElement
				{
					Type = type
				};

			Assert.AreEqual(type, htmlTransformerElement.Type);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TypeSet")]
		public void Type_Set_IfTheValueParameterIsNull_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new HtmlTransformerElement
					{
						Type = null
					};
				// ReSharper restore ObjectCreationAsStatement
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
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TypeSet")]
		public void Type_Set_ShouldSetTheType()
		{
			Type type = Mock.Of<IHtmlTransformer>().GetType();

			HtmlTransformerElement htmlTransformerElement = new HtmlTransformerElement
				{
					Type = type
				};

			Assert.AreEqual(type, htmlTransformerElement.Type);
		}

		#endregion
	}
}