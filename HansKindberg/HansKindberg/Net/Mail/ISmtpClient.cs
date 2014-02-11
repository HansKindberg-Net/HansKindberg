using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace HansKindberg.Net.Mail
{
	public interface ISmtpClient
	{
		#region Events

		event SendCompletedEventHandler SendCompleted;

		#endregion

		#region Properties

		X509CertificateCollection ClientCertificates { get; }
		ICredentialsByHost Credentials { get; set; }
		SmtpDeliveryMethod DeliveryMethod { get; set; }
		bool EnableSsl { get; set; }
		string Host { get; set; }
		string PickupDirectoryLocation { get; set; }
		int Port { get; set; }
		ServicePoint ServicePoint { get; }
		string TargetName { get; set; }
		int Timeout { get; set; }
		bool UseDefaultCredentials { get; set; }

		#endregion

		#region Methods

		void Send(MailMessage message);
		void Send(string from, string recipients, string subject, string body);
		void SendAsync(MailMessage message, object userToken);
		void SendAsync(string from, string recipients, string subject, string body, object userToken);
		void SendAsyncCancel();

		#endregion
	}
}