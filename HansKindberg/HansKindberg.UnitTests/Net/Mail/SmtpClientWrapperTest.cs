using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;
using System.Threading;
using HansKindberg.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.UnitTests.Net.Mail
{
	[TestClass]
	public class SmtpClientWrapperTest
	{
		#region Fields

		private static string _pickupDirectoryLocation;

		#endregion

		#region Methods

		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			if(testContext == null)
				throw new ArgumentNullException("testContext");

			_pickupDirectoryLocation = testContext.TestDeploymentDir;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Net.Mail.SmtpClientWrapper")]
		public void Constructor_IfTheSmtpClientParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new SmtpClientWrapper(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "smtpClient")
					throw;
			}
		}

		private static void CredentialsTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.IsNull(smtpClient.Credentials);
			Assert.IsNull(smtpClientWrapper.Credentials);
			Assert.AreEqual(smtpClient.Credentials, smtpClientWrapper.Credentials);

			ICredentialsByHost credentials = Mock.Of<ICredentialsByHost>();
			smtpClientWrapper.Credentials = credentials;

			Assert.AreEqual(credentials, smtpClient.Credentials);
			Assert.AreEqual(credentials, smtpClientWrapper.Credentials);
			Assert.AreEqual(smtpClient.Credentials, smtpClientWrapper.Credentials);
		}

		[TestMethod]
		public void Credentials_Get_ShouldReturnTheCredentialsOfTheWrappedSmtpClient()
		{
			CredentialsTest();
		}

		[TestMethod]
		public void Credentials_Set_ShouldSetTheCredentialsOfTheWrappedSmtpClient()
		{
			CredentialsTest();
		}

		private static void DeliveryMethodTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.AreEqual(SmtpDeliveryMethod.Network, smtpClient.DeliveryMethod);
			Assert.AreEqual(SmtpDeliveryMethod.Network, smtpClientWrapper.DeliveryMethod);
			Assert.AreEqual(smtpClient.DeliveryMethod, smtpClientWrapper.DeliveryMethod);

			SmtpDeliveryMethod smtpDeliveryMethod;

			switch(DateTime.Now.Second%3)
			{
				case 0:
					smtpDeliveryMethod = SmtpDeliveryMethod.Network;
					break;
				case 1:
					smtpDeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
					break;
				default:
					smtpDeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
					break;
			}

			smtpClientWrapper.DeliveryMethod = smtpDeliveryMethod;

			Assert.AreEqual(smtpDeliveryMethod, smtpClient.DeliveryMethod);
			Assert.AreEqual(smtpDeliveryMethod, smtpClientWrapper.DeliveryMethod);
			Assert.AreEqual(smtpClient.DeliveryMethod, smtpClientWrapper.DeliveryMethod);
		}

		[TestMethod]
		public void DeliveryMethod_Get_ShouldReturnTheDeliveryMethodOfTheWrappedSmtpClient()
		{
			DeliveryMethodTest();
		}

		[TestMethod]
		public void DeliveryMethod_Set_ShouldSetTheDeliveryMethodOfTheWrappedSmtpClient()
		{
			DeliveryMethodTest();
		}

		private static void EnableSslTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.IsFalse(smtpClient.EnableSsl);
			Assert.IsFalse(smtpClientWrapper.EnableSsl);
			Assert.AreEqual(smtpClient.EnableSsl, smtpClientWrapper.EnableSsl);

			bool enableSsl = DateTime.Now.Second%2 == 0;

			smtpClientWrapper.EnableSsl = enableSsl;

			Assert.AreEqual(enableSsl, smtpClient.EnableSsl);
			Assert.AreEqual(enableSsl, smtpClientWrapper.EnableSsl);
			Assert.AreEqual(smtpClient.EnableSsl, smtpClientWrapper.EnableSsl);
		}

		[TestMethod]
		public void EnableSsl_Get_ShouldReturnEnableSslOfTheWrappedSmtpClient()
		{
			EnableSslTest();
		}

		[TestMethod]
		public void EnableSsl_Set_ShouldSetEnableSslOfTheWrappedSmtpClient()
		{
			EnableSslTest();
		}

		[TestMethod]
		public void FromSmtpClient_IfTheSmtpClientParameterIsNotNull_ShouldReturnAWrapperForTheSmtpClient()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = SmtpClientWrapper.FromSmtpClient(smtpClient);

			Assert.IsNotNull(smtpClientWrapper);
			Assert.AreEqual(smtpClient, smtpClientWrapper.SmtpClient);
		}

		[TestMethod]
		public void FromSmtpClient_IfTheSmtpClientParameterIsNull_ShouldReturnNull()
		{
			Assert.IsNull(SmtpClientWrapper.FromSmtpClient(null));
		}

		private static void HostTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.IsNull(smtpClient.Host);
			Assert.IsNull(smtpClientWrapper.Host);
			Assert.AreEqual(smtpClient.Host, smtpClientWrapper.Host);

			string host = Guid.NewGuid().ToString();

			smtpClientWrapper.Host = host;

			Assert.AreEqual(host, smtpClient.Host);
			Assert.AreEqual(host, smtpClientWrapper.Host);
			Assert.AreEqual(smtpClient.Host, smtpClientWrapper.Host);
		}

		[TestMethod]
		public void Host_Get_ShouldReturnTheHostOfTheWrappedSmtpClient()
		{
			HostTest();
		}

		[TestMethod]
		public void Host_Set_ShouldSetTheHostOfTheWrappedSmtpClient()
		{
			HostTest();
		}

		[TestMethod]
		public void ImplicitOperatorsTest()
		{
			SmtpClient smtpClient = null;
			// ReSharper disable ExpressionIsAlwaysNull
			SmtpClientWrapper smtpClientWrapper = smtpClient;
			// ReSharper restore ExpressionIsAlwaysNull

			Assert.IsNull(smtpClientWrapper);

			smtpClient = new SmtpClient();
			smtpClientWrapper = smtpClient;

			Assert.IsNotNull(smtpClientWrapper);
			Assert.AreEqual(smtpClient, smtpClientWrapper.SmtpClient);

			smtpClient = new SmtpClient();
			smtpClientWrapper = new SmtpClientWrapper(smtpClient);
			SmtpClient castedSmtpClient = smtpClientWrapper;

			Assert.IsNotNull(castedSmtpClient);
			Assert.AreEqual(smtpClientWrapper.SmtpClient, castedSmtpClient);

			smtpClientWrapper = null;
			// ReSharper disable ExpressionIsAlwaysNull
			smtpClient = smtpClientWrapper;
			// ReSharper restore ExpressionIsAlwaysNull

			Assert.IsNull(smtpClient);
		}

		private static void PickupDirectoryLocationTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.IsNull(smtpClient.PickupDirectoryLocation);
			Assert.IsNull(smtpClientWrapper.PickupDirectoryLocation);
			Assert.AreEqual(smtpClient.PickupDirectoryLocation, smtpClientWrapper.PickupDirectoryLocation);

			string pickupDirectoryLocation = Guid.NewGuid().ToString();

			smtpClientWrapper.PickupDirectoryLocation = pickupDirectoryLocation;

			Assert.AreEqual(pickupDirectoryLocation, smtpClient.PickupDirectoryLocation);
			Assert.AreEqual(pickupDirectoryLocation, smtpClientWrapper.PickupDirectoryLocation);
			Assert.AreEqual(smtpClient.PickupDirectoryLocation, smtpClientWrapper.PickupDirectoryLocation);
		}

		[TestMethod]
		public void PickupDirectoryLocation_Get_ShouldReturnThePickupDirectoryLocationOfTheWrappedSmtpClient()
		{
			PickupDirectoryLocationTest();
		}

		[TestMethod]
		public void PickupDirectoryLocation_Set_ShouldSetThePickupDirectoryLocationOfTheWrappedSmtpClient()
		{
			PickupDirectoryLocationTest();
		}

		private static void PortTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.AreEqual(smtpClient.Port, smtpClientWrapper.Port);

			int port = DateTime.Now.Millisecond;

			smtpClientWrapper.Port = port;

			Assert.AreEqual(port, smtpClient.Port);
			Assert.AreEqual(port, smtpClientWrapper.Port);
			Assert.AreEqual(smtpClient.Port, smtpClientWrapper.Port);
		}

		[TestMethod]
		public void Port_Get_ShouldReturnThePortOfTheWrappedSmtpClient()
		{
			PortTest();
		}

		[TestMethod]
		public void Port_Set_ShouldSetThePortOfTheWrappedSmtpClient()
		{
			PortTest();
		}

		[TestMethod]
		public void SendAsync_WithFiveParameters_IfSendCompletedIsSubscribedTo_ShouldInvokeTheEventHandler()
		{
			bool sendCompletedIsCalled = false;

			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient)
			{
				DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
				PickupDirectoryLocation = _pickupDirectoryLocation
			};

			smtpClientWrapper.SendCompleted += delegate { sendCompletedIsCalled = true; };

			smtpClientWrapper.SendAsync("test@test", "test@test", "Test", "Test", null);

			Thread.Sleep(10); // Wait so the SendCompleted event-handler has been invoked.

			Assert.IsTrue(sendCompletedIsCalled);
		}

		[TestMethod]
		public void SendAsync_WithTwoParameters_IfSendCompletedIsSubscribedTo_ShouldInvokeTheEventHandler()
		{
			bool sendCompletedIsCalled = false;

			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient)
			{
				DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
				PickupDirectoryLocation = AppDomain.CurrentDomain.BaseDirectory
			};

			smtpClientWrapper.SendCompleted += delegate { sendCompletedIsCalled = true; };

			using(MailMessage mailMessage = new MailMessage("test@test", "test@test", "Test", "Test"))
			{
				smtpClientWrapper.SendAsync(mailMessage, null);
			}

			Thread.Sleep(10); // Wait so the SendCompleted event-handler has been invoked.

			Assert.IsTrue(sendCompletedIsCalled);
		}

		private static void TargetNameTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			const string defaultTargetName = "SMTPSVC/";

			Assert.AreEqual(defaultTargetName, smtpClient.TargetName);
			Assert.AreEqual(defaultTargetName, smtpClientWrapper.TargetName);
			Assert.AreEqual(smtpClient.TargetName, smtpClientWrapper.TargetName);

			string targetName = Guid.NewGuid().ToString();

			smtpClientWrapper.TargetName = targetName;

			Assert.AreEqual(targetName, smtpClient.TargetName);
			Assert.AreEqual(targetName, smtpClientWrapper.TargetName);
			Assert.AreEqual(smtpClient.TargetName, smtpClientWrapper.TargetName);
		}

		[TestMethod]
		public void TargetName_Get_ShouldReturnTheTargetNameOfTheWrappedSmtpClient()
		{
			TargetNameTest();
		}

		[TestMethod]
		public void TargetName_Set_ShouldSetTheTargetNameOfTheWrappedSmtpClient()
		{
			TargetNameTest();
		}

		private static void TimeoutTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.AreEqual(smtpClient.Timeout, smtpClientWrapper.Timeout);

			int timeout = DateTime.Now.Millisecond;

			smtpClientWrapper.Timeout = timeout;

			Assert.AreEqual(timeout, smtpClient.Timeout);
			Assert.AreEqual(timeout, smtpClientWrapper.Timeout);
			Assert.AreEqual(smtpClient.Timeout, smtpClientWrapper.Timeout);
		}

		[TestMethod]
		public void Timeout_Get_ShouldReturnTheTimeoutOfTheWrappedSmtpClient()
		{
			TimeoutTest();
		}

		[TestMethod]
		public void Timeout_Set_ShouldSetTheTimeoutOfTheWrappedSmtpClient()
		{
			TimeoutTest();
		}

		[TestMethod]
		public void ToSmtpClient_IfTheSmtpClientWrapperParameterIsNotNull_ShouldReturnTheWrappedSmtpClient()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);
			Assert.AreEqual(smtpClient, SmtpClientWrapper.ToSmtpClient(smtpClientWrapper));
		}

		[TestMethod]
		public void ToSmtpClient_IfTheSmtpClientWrapperParameterIsNull_ShouldReturnNull()
		{
			Assert.IsNull(SmtpClientWrapper.ToSmtpClient(null));
		}

		private static void UseDefaultCredentialsTest()
		{
			SmtpClient smtpClient = new SmtpClient();
			SmtpClientWrapper smtpClientWrapper = new SmtpClientWrapper(smtpClient);

			Assert.IsFalse(smtpClient.UseDefaultCredentials);
			Assert.IsFalse(smtpClientWrapper.UseDefaultCredentials);
			Assert.AreEqual(smtpClient.UseDefaultCredentials, smtpClientWrapper.UseDefaultCredentials);

			bool useDefaultCredentials = DateTime.Now.Second%2 == 0;

			smtpClientWrapper.UseDefaultCredentials = useDefaultCredentials;

			Assert.AreEqual(useDefaultCredentials, smtpClient.UseDefaultCredentials);
			Assert.AreEqual(useDefaultCredentials, smtpClientWrapper.UseDefaultCredentials);
			Assert.AreEqual(smtpClient.UseDefaultCredentials, smtpClientWrapper.UseDefaultCredentials);
		}

		[TestMethod]
		public void UseDefaultCredentials_Get_ShouldReturnUseDefaultCredentialsOfTheWrappedSmtpClient()
		{
			UseDefaultCredentialsTest();
		}

		[TestMethod]
		public void UseDefaultCredentials_Set_ShouldSetUseDefaultCredentialsOfTheWrappedSmtpClient()
		{
			UseDefaultCredentialsTest();
		}

		#endregion
	}
}