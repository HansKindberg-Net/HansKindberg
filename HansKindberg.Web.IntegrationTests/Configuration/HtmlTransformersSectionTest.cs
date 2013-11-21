using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Threading;
using System.Web;
using HansKindberg.Web.Configuration;
using HansKindberg.Web.IntegrationTests.Configuration.Mocks;
using HansKindberg.Web.Simulation;
using HansKindberg.Web.Simulation.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.IntegrationTests.Configuration
{
	[TestClass]
	public class HtmlTransformersSectionTest
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
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "type")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "virtualPathProvider")]
		public void ConfigurationManager_GetHtmlTransformersSection_HtmlTransformers_ShouldDependOnLocations()
		{
			string physicalDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
			IFileTransfer fileTransfer = new FileTransfer(new FileSystem(), physicalDirectoryPath, physicalDirectoryPath);
			fileTransfer.AddFile(string.Format(CultureInfo.InvariantCulture, @"{0}\Configuration\Test-configurations\HtmlTransformersSectionTest.DifferentLocations.Web.config", AppDomain.CurrentDomain.BaseDirectory), Path.Combine(physicalDirectoryPath, "Web.config"));

			SimulatedVirtualPathProvider simulatedVirtualPathProvider = new SimulatedVirtualPathProvider();
			simulatedVirtualPathProvider.VirtualFiles.CreateWithDefaultWebFormContentAndAddFile("/Default.aspx");
			simulatedVirtualPathProvider.VirtualFiles.CreateWithDefaultWebFormContentAndAddFile("/FiveItemsUsingAddAndRemove/Default.aspx");
			simulatedVirtualPathProvider.VirtualFiles.CreateWithDefaultWebFormContentAndAddFile("/NoItemsUsingClear/Default.aspx");
			simulatedVirtualPathProvider.VirtualFiles.CreateWithDefaultWebFormContentAndAddFile("/ThreeItemsUsingRemove/Default.aspx");
			simulatedVirtualPathProvider.VirtualFiles.CreateWithDefaultWebFormContentAndAddFile("/ThreeItemsUsingRemove/TwoItemsUsingAddAndRemove/Default.aspx");
			simulatedVirtualPathProvider.VirtualFiles.CreateWithDefaultWebFormContentAndAddFile("/ThreeItemsUsingRemove/TwoItemsUsingAddAndRemove/NoItemsUsingRemove/Default.aspx");

			using(VirtualApplicationHostProxy virtualApplicationHostProxy = new VirtualApplicationHostProxyFactory().Create(fileTransfer, simulatedVirtualPathProvider))
			{
				virtualApplicationHostProxy.AnyApplicationEvent += delegate(HttpApplicationEvent httpApplicationEvent)
				{
					if(httpApplicationEvent != HttpApplicationEvent.PostRequestHandlerExecute)
						return;

					HttpRequest httpRequest = HttpContext.Current.Request;
					HtmlTransformerElementCollection htmlTransformers = ((HtmlTransformersSection) ConfigurationManager.GetSection("hansKindberg.web/htmlTransformers")).HtmlTransformers;

					if(httpRequest.RawUrl == "/Default.aspx")
					{
						Assert.AreEqual(5, htmlTransformers.Count);

						Assert.AreEqual("One", htmlTransformers[0].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Two", htmlTransformers[1].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Three", htmlTransformers[2].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Four", htmlTransformers[3].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Five", htmlTransformers[4].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						return;
					}

					if(httpRequest.RawUrl == "/FiveItemsUsingAddAndRemove/Default.aspx")
					{
						Assert.AreEqual(5, htmlTransformers.Count);

						Assert.AreEqual("One", htmlTransformers[0].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Two", htmlTransformers[1].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Three", htmlTransformers[2].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Six", htmlTransformers[3].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Seven", htmlTransformers[4].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						return;
					}

					if(httpRequest.RawUrl == "/NoItemsUsingClear/Default.aspx")
					{
						Assert.AreEqual(0, htmlTransformers.Count);

						return;
					}

					if(httpRequest.RawUrl == "/ThreeItemsUsingRemove/Default.aspx")
					{
						Assert.AreEqual(3, htmlTransformers.Count);

						Assert.AreEqual("One", htmlTransformers[0].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Two", htmlTransformers[1].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Three", htmlTransformers[2].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						return;
					}

					if(httpRequest.RawUrl == "/ThreeItemsUsingRemove/TwoItemsUsingAddAndRemove/Default.aspx")
					{
						Assert.AreEqual(2, htmlTransformers.Count);

						Assert.AreEqual("Four", htmlTransformers[0].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						Assert.AreEqual("Five", htmlTransformers[1].Name);
						Assert.AreEqual(typeof(HtmlTransformer), htmlTransformers[0].Type);

						return;
					}

					if(httpRequest.RawUrl == "/ThreeItemsUsingRemove/TwoItemsUsingAddAndRemove/NoItemsUsingRemove/Default.aspx")
					{
						Assert.AreEqual(0, htmlTransformers.Count);

						return;
					}

					throw new InvalidOperationException("There is an error in the test.");
				};

				virtualApplicationHostProxy.Run(browsingSession =>
				{
					RequestResult requestResult = browsingSession.ProcessRequest("/Default.aspx");

					Exception exception = requestResult.LastException;
					if(exception != null)
					{
						if(exception is UnitTestAssertException)
							throw exception;

						Assert.Fail("{0}: {1}", requestResult.LastException.GetType(), requestResult.LastException.Message);
					}

					requestResult = browsingSession.ProcessRequest("/FiveItemsUsingAddAndRemove/Default.aspx");

					exception = requestResult.LastException;
					if(exception != null)
					{
						if(exception is UnitTestAssertException)
							throw exception;

						Assert.Fail("{0}: {1}", requestResult.LastException.GetType(), requestResult.LastException.Message);
					}

					requestResult = browsingSession.ProcessRequest("/NoItemsUsingClear/Default.aspx");

					exception = requestResult.LastException;
					if(exception != null)
					{
						if(exception is UnitTestAssertException)
							throw exception;

						Assert.Fail("{0}: {1}", requestResult.LastException.GetType(), requestResult.LastException.Message);
					}

					requestResult = browsingSession.ProcessRequest("/ThreeItemsUsingRemove/Default.aspx");

					exception = requestResult.LastException;
					if(exception != null)
					{
						if(exception is UnitTestAssertException)
							throw exception;

						Assert.Fail("{0}: {1}", requestResult.LastException.GetType(), requestResult.LastException.Message);
					}

					requestResult = browsingSession.ProcessRequest("/ThreeItemsUsingRemove/TwoItemsUsingAddAndRemove/Default.aspx");

					exception = requestResult.LastException;
					if(exception != null)
					{
						if(exception is UnitTestAssertException)
							throw exception;

						Assert.Fail("{0}: {1}", requestResult.LastException.GetType(), requestResult.LastException.Message);
					}

					requestResult = browsingSession.ProcessRequest("/ThreeItemsUsingRemove/TwoItemsUsingAddAndRemove/NoItemsUsingRemove/Default.aspx");

					exception = requestResult.LastException;
					if(exception != null)
					{
						if(exception is UnitTestAssertException)
							throw exception;

						Assert.Fail("{0}: {1}", requestResult.LastException.GetType(), requestResult.LastException.Message);
					}
				});
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_IfAHtmlTransformerNameHasInvalidCharacters_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("HtmlTransformerNameWithInvalidCharacters");
				configuration.GetSection(_htmlTransformersSectionPath);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "The value for the property 'name' is not valid. The error is: The string cannot contain any of the following characters: ' ~!@#$%^&*()[]{}/;'\"|\\'.")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_IfTheNameAttributeOfAHtmlTransformerElementIsEmpty_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("TheNameAttributeOfAHtmlTransformerElementIsEmpty");
				configuration.GetSection(_htmlTransformersSectionPath);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "The value for the property 'name' is not valid. The error is: The string must be at least 1 characters long.")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_IfTheNameAttributeOfAHtmlTransformerElementIsMissing_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("TheNameAttributeOfAHtmlTransformerElementIsMissing");
				configuration.GetSection(_htmlTransformersSectionPath);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "Required attribute 'name' not found.")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_IfTheTypeAttributeOfAHtmlTransformerElementIsMissing_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("TheTypeAttributeOfAHtmlTransformerElementIsMissing");
				configuration.GetSection(_htmlTransformersSectionPath);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "Required attribute 'type' not found.")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Configuration_GetHtmlTransformersSection_IfThereAreTwoHtmlTransformersWithTheSameNameButDifferentTypes_ShouldThrowAConfigurationErrorsException()
		{
			try
			{
				System.Configuration.Configuration configuration = CreateConfiguration("TwoHtmlTransformersWithTheSameNameButDifferentTypes");
				configuration.GetSection(_htmlTransformersSectionPath);
			}
			catch(ConfigurationErrorsException configurationErrorsException)
			{
				if(configurationErrorsException.BareMessage == "The entry 'Test' has already been added.")
					throw;
			}
		}

		private static System.Configuration.Configuration CreateConfiguration(string configurationFileNameIdentity)
		{
			ConfigurationFileMap configurationFileMap = new ConfigurationFileMap
				{
					MachineConfigFilename = string.Format(CultureInfo.InvariantCulture, @"{0}\Configuration\Test-configurations\HtmlTransformersSectionTest.{1}.Web.config", AppDomain.CurrentDomain.BaseDirectory, configurationFileNameIdentity)
				};

			return ConfigurationManager.OpenMappedMachineConfiguration(configurationFileMap);
		}

		#endregion
	}
}