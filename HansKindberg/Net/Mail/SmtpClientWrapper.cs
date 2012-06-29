using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace HansKindberg.Net.Mail
{
	public class SmtpClientWrapper : ISmtpClient
	{
		#region Fields

		private readonly SmtpClient _smtpClient;

		#endregion

		#region Constructors

		public SmtpClientWrapper(SmtpClient smtpClient)
		{
			if(smtpClient == null)
				throw new ArgumentNullException("smtpClient");

			this._smtpClient = smtpClient;
		}

		#endregion

		#region Events

		public virtual event SendCompletedEventHandler SendCompleted
		{
			add { this._smtpClient.SendCompleted += value; }
			remove { this._smtpClient.SendCompleted -= value; }
		}

		#endregion

		#region Properties

		public virtual X509CertificateCollection ClientCertificates
		{
			get { return this._smtpClient.ClientCertificates; }
		}

		public virtual ICredentialsByHost Credentials
		{
			get { return this._smtpClient.Credentials; }
			set { this._smtpClient.Credentials = value; }
		}

		public virtual SmtpDeliveryMethod DeliveryMethod
		{
			get { return this._smtpClient.DeliveryMethod; }
			set { this._smtpClient.DeliveryMethod = value; }
		}

		public virtual bool EnableSsl
		{
			get { return this._smtpClient.EnableSsl; }
			set { this._smtpClient.EnableSsl = value; }
		}

		public virtual string Host
		{
			get { return this._smtpClient.Host; }
			set { this._smtpClient.Host = value; }
		}

		public virtual string PickupDirectoryLocation
		{
			get { return this._smtpClient.PickupDirectoryLocation; }
			set { this._smtpClient.PickupDirectoryLocation = value; }
		}

		public virtual int Port
		{
			get { return this._smtpClient.Port; }
			set { this._smtpClient.Port = value; }
		}

		public virtual ServicePoint ServicePoint
		{
			get { return this._smtpClient.ServicePoint; }
		}

		public virtual string TargetName
		{
			get { return this._smtpClient.TargetName; }
			set { this._smtpClient.TargetName = value; }
		}

		public virtual int Timeout
		{
			get { return this._smtpClient.Timeout; }
			set { this._smtpClient.Timeout = value; }
		}

		public virtual bool UseDefaultCredentials
		{
			get { return this._smtpClient.UseDefaultCredentials; }
			set { this._smtpClient.UseDefaultCredentials = value; }
		}

		#endregion

		#region Methods

		public static SmtpClientWrapper FromSmtpClient(SmtpClient smtpClient)
		{
			return smtpClient;
		}

		public virtual void Send(MailMessage message)
		{
			this._smtpClient.Send(message);
		}

		public virtual void Send(string from, string recipients, string subject, string body)
		{
			this._smtpClient.Send(from, recipients, subject, body);
		}

		public virtual void SendAsync(MailMessage message, object userToken)
		{
			this._smtpClient.SendAsync(message, userToken);
		}

		public virtual void SendAsync(string from, string recipients, string subject, string body, object userToken)
		{
			this._smtpClient.SendAsync(from, recipients, subject, body, userToken);
		}

		public virtual void SendAsyncCancel()
		{
			this._smtpClient.SendAsyncCancel();
		}

		public static SmtpClient ToSmtpClient(ISmtpClient smtpClient)
		{
			if(smtpClient == null)
				return null;

			return new SmtpClient
				{
					Credentials = smtpClient.Credentials,
					DeliveryMethod = smtpClient.DeliveryMethod,
					EnableSsl = smtpClient.EnableSsl,
					Host = smtpClient.Host,
					PickupDirectoryLocation = smtpClient.PickupDirectoryLocation,
					Port = smtpClient.Port,
					TargetName = smtpClient.TargetName,
					Timeout = smtpClient.Timeout,
					UseDefaultCredentials = smtpClient.UseDefaultCredentials
				};
		}

		#endregion

		#region Implicit operator

		public static implicit operator SmtpClientWrapper(SmtpClient smtpClient)
		{
			return smtpClient == null ? null : new SmtpClientWrapper(smtpClient);
		}

		#endregion
	}
}