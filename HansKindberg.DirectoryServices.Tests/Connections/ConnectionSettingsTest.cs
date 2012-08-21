using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.Tests.Connections
{
	[TestClass]
	public class ConnectionSettingsTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Initialize_IfThereIsNoSchemeParameter_ShouldThrowAnArgumentException()
		{
			new ConnectionSettings().Initialize(new Dictionary<string, string>(), DateTime.Now.Second%2 == 0);
		}

		[TestMethod]
		public void Initialize_IfThereIsNoSchemeParameter_ShouldThrowAnArgumentExceptionWithACorrectMessage()
		{
			CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

			string expectedMessage = "The parameter \"Scheme\" is required. Valid values are: LDAP, LDAPS, WinNT, IIS." + Environment.NewLine + "Parameter name: parameters";
			string actualMessage = null;

			try
			{
				new ConnectionSettings().Initialize(new Dictionary<string, string>(), DateTime.Now.Second%2 == 0);
			}
			catch(ArgumentException argumentException)
			{
				actualMessage = argumentException.Message;
			}

			Assert.AreEqual(expectedMessage, actualMessage);

			Thread.CurrentThread.CurrentUICulture = currentUiCulture;
		}

		#endregion
	}
}