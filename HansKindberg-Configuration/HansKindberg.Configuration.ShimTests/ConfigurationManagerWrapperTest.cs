using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Fakes;
using System.Linq;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Configuration.ShimTests
{
	[TestClass]
	public class ConfigurationManagerWrapperTest
	{
		#region Methods

		[TestMethod]
		public void AppSettings_ShouldCallAppSettingsOfTheWrappedConfigurationManager()
		{
			using(ShimsContext.Create())
			{
				bool appSettingsIsCalled = false;
				var shimAppSettings = new NameValueCollection
				{
					{"TestName", "TestValue"}
				};

				ShimConfigurationManager.AppSettingsGet = delegate
				{
					appSettingsIsCalled = true;
					return shimAppSettings;
				};

				Assert.IsFalse(appSettingsIsCalled);

				var appSettings = new ConfigurationManagerWrapper().AppSettings;

				Assert.AreEqual(shimAppSettings, appSettings);
				Assert.AreEqual(1, appSettings.Count);
				Assert.AreEqual(shimAppSettings[0], appSettings[0]);
				Assert.IsTrue(appSettingsIsCalled);
			}
		}

		[TestMethod]
		public void ConnectionStrings_ShouldCallConnectionStringsOfTheWrappedConfigurationManager()
		{
			using(ShimsContext.Create())
			{
				bool connectionStringsIsCalled = false;
				var shimConnectionStrings = new ConnectionStringSettingsCollection
				{
					new ConnectionStringSettings("ConnectionName", "ConnectionStringValue")
				};

				ShimConfigurationManager.ConnectionStringsGet = delegate
				{
					connectionStringsIsCalled = true;
					return shimConnectionStrings;
				};

				Assert.IsFalse(connectionStringsIsCalled);

				var connectionStrings = new ConfigurationManagerWrapper().ConnectionStrings.ToArray();

				Assert.AreEqual(1, connectionStrings.Count());
				Assert.AreEqual(shimConnectionStrings[0], connectionStrings.ElementAt(0));
				Assert.IsTrue(connectionStringsIsCalled);
			}
		}

		[TestMethod]
		public void GetSection_ShouldCallGetSectionOfTheWrappedConfigurationManager()
		{
			using(ShimsContext.Create())
			{
				bool getSectionIsCalled = false;
				const string shimSection = "TestSection";
				const string sectionNameParameter = "TestSectionName";

				ShimConfigurationManager.GetSectionString = delegate(string sectionName)
				{
					if(sectionName == sectionNameParameter)
					{
						getSectionIsCalled = true;
						return shimSection;
					}

					return null;
				};

				Assert.IsFalse(getSectionIsCalled);

				var section = new ConfigurationManagerWrapper().GetSection(sectionNameParameter);

				Assert.AreEqual(shimSection, section);
				Assert.IsTrue(getSectionIsCalled);
			}
		}

		[TestMethod]
		public void RefreshSection_ShouldCallRefreshSectionOfTheWrappedConfigurationManager()
		{
			using(ShimsContext.Create())
			{
				bool refreshSectionIsCalled = false;
				const string sectionNameParameter = "TestSectionName";

				ShimConfigurationManager.RefreshSectionString = delegate(string sectionName)
				{
					if(sectionName == sectionNameParameter)
						refreshSectionIsCalled = true;
				};

				Assert.IsFalse(refreshSectionIsCalled);

				new ConfigurationManagerWrapper().RefreshSection(sectionNameParameter);

				Assert.IsTrue(refreshSectionIsCalled);
			}
		}

		#endregion
	}
}