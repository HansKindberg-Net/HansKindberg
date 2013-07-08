using System.Net;
using System.Net.Fakes;
using System.Net.Mail.Fakes;
using System.Security.Cryptography.X509Certificates;
using HansKindberg.Net.Mail;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.ShimTests.Net.Mail
{
    [TestClass]
    public class SmtpClientWrapperTest
    {
        #region Methods

        [TestMethod]
        public void ClientCertificates_Get_ShouldCallClientCertificatesGetOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool clientCertificatesGetIsCalled = false;
                X509CertificateCollection clientCertificates = new X509CertificateCollection();

                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        ClientCertificatesGet = delegate
                        {
                            clientCertificatesGetIsCalled = true;
                            return clientCertificates;
                        }
                    };

                Assert.IsFalse(clientCertificatesGetIsCalled);

                Assert.AreEqual(clientCertificates, new SmtpClientWrapper(shimSmtpClient.Instance).ClientCertificates);
                Assert.IsTrue(clientCertificatesGetIsCalled);
            }
        }

        [TestMethod]
        public void SendAsyncCancel_ShouldCallSendAsyncCancelOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool sendAsyncCancelIsCalled = false;

                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        SendAsyncCancel = delegate { sendAsyncCancelIsCalled = true; }
                    };

                Assert.IsFalse(sendAsyncCancelIsCalled);

                new SmtpClientWrapper(shimSmtpClient.Instance).SendAsyncCancel();
                Assert.IsTrue(sendAsyncCancelIsCalled);
            }
        }

        [TestMethod]
        public void SendAsync_WithFiveParameters_ShouldCallSendAsyncWithFiveParametersOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool sendAsyncIsCalled = false;

                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        SendAsyncStringStringStringStringObject = delegate { sendAsyncIsCalled = true; }
                    };

                Assert.IsFalse(sendAsyncIsCalled);

                new SmtpClientWrapper(shimSmtpClient.Instance).SendAsync(null, null, null, null, null);
                Assert.IsTrue(sendAsyncIsCalled);
            }
        }

        [TestMethod]
        public void SendAsync_WithTwoParameters_ShouldCallSendAsyncWithTwoParametersOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool sendAsyncIsCalled = false;

                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        SendAsyncMailMessageObject = delegate { sendAsyncIsCalled = true; }
                    };

                Assert.IsFalse(sendAsyncIsCalled);

                new SmtpClientWrapper(shimSmtpClient.Instance).SendAsync(null, null);
                Assert.IsTrue(sendAsyncIsCalled);
            }
        }

        [TestMethod]
        public void SendCompleted_Add_ShouldCallSendCompletedAddOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool invoked = false;
                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        SendCompletedAddSendCompletedEventHandler = delegate { invoked = true; }
                    };
                new SmtpClientWrapper(shimSmtpClient.Instance).SendCompleted += ((sender, args) => { });
                Assert.IsTrue(invoked);
            }
        }

        [TestMethod]
        public void SendCompleted_Remove_ShouldCallSendCompletedRemoveOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool invoked = false;
                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        SendCompletedRemoveSendCompletedEventHandler = delegate { invoked = true; }
                    };
                new SmtpClientWrapper(shimSmtpClient.Instance).SendCompleted -= ((sender, args) => { });
                Assert.IsTrue(invoked);
            }
        }

        [TestMethod]
        public void Send_WithFourParameters_ShouldCallSendWithFourParametersOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool sendIsCalled = false;

                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        SendStringStringStringString = delegate { sendIsCalled = true; }
                    };

                Assert.IsFalse(sendIsCalled);

                new SmtpClientWrapper(shimSmtpClient.Instance).Send(null, null, null, null);
                Assert.IsTrue(sendIsCalled);
            }
        }

        [TestMethod]
        public void Send_WithOneParameter_ShouldCallSendWithOneParameterOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool sendIsCalled = false;

                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        SendMailMessage = delegate { sendIsCalled = true; }
                    };

                Assert.IsFalse(sendIsCalled);

                new SmtpClientWrapper(shimSmtpClient.Instance).Send(null);
                Assert.IsTrue(sendIsCalled);
            }
        }

        [TestMethod]
        public void ServicePoint_Get_ShouldCallServicePointGetOfTheWrappedSmtpClient()
        {
            using(ShimsContext.Create())
            {
                bool servicePointGetIsCalled = false;
                ServicePoint servicePoint = new ShimServicePoint().Instance;

                ShimSmtpClient shimSmtpClient = new ShimSmtpClient()
                    {
                        ServicePointGet = delegate
                        {
                            servicePointGetIsCalled = true;
                            return servicePoint;
                        }
                    };

                Assert.IsFalse(servicePointGetIsCalled);

                Assert.AreEqual(servicePoint, new SmtpClientWrapper(shimSmtpClient.Instance).ServicePoint);
                Assert.IsTrue(servicePointGetIsCalled);
            }
        }

        #endregion
    }
}