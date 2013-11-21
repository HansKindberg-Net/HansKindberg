using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace HansKindberg.Net.Mail
{
	public class SmtpClientWrapper : SmtpClientWrapper<SmtpClient>
	{
		#region Constructors

		public SmtpClientWrapper(SmtpClient smtpClient) : base(smtpClient) {}

		#endregion

		#region Methods

		public static SmtpClientWrapper FromSmtpClient(SmtpClient smtpClient)
		{
			return smtpClient;
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static SmtpClient ToSmtpClient(SmtpClientWrapper smtpClientWrapper)
		{
			return smtpClientWrapper == null ? null : smtpClientWrapper.SmtpClient;
		}

		#endregion

		#region Implicit operator

		public static implicit operator SmtpClientWrapper(SmtpClient smtpClient)
		{
			return smtpClient == null ? null : new SmtpClientWrapper(smtpClient);
		}

		public static implicit operator SmtpClient(SmtpClientWrapper smtpClientWrapper)
		{
			return smtpClientWrapper == null ? null : smtpClientWrapper.SmtpClient;
		}

		#endregion
	}

	public abstract class SmtpClientWrapper<TSmtpClient> : ISmtpClient where TSmtpClient : SmtpClient
	{
		#region Fields

		private readonly TSmtpClient _smtpClient;

		#endregion

		#region Constructors

		protected SmtpClientWrapper(TSmtpClient smtpClient)
		{
			if(smtpClient == null)
				throw new ArgumentNullException("smtpClient");

			this._smtpClient = smtpClient;
		}

		#endregion

		#region Events

		public virtual event SendCompletedEventHandler SendCompleted
		{
			add { this.SmtpClient.SendCompleted += value; }
			remove { this.SmtpClient.SendCompleted -= value; }
		}

		#endregion

		#region Properties

		public virtual X509CertificateCollection ClientCertificates
		{
			get { return this.SmtpClient.ClientCertificates; }
		}

		public virtual ICredentialsByHost Credentials
		{
			get { return this.SmtpClient.Credentials; }
			set { this.SmtpClient.Credentials = value; }
		}

		public virtual SmtpDeliveryMethod DeliveryMethod
		{
			get { return this.SmtpClient.DeliveryMethod; }
			set { this.SmtpClient.DeliveryMethod = value; }
		}

		public virtual bool EnableSsl
		{
			get { return this.SmtpClient.EnableSsl; }
			set { this.SmtpClient.EnableSsl = value; }
		}

		public virtual string Host
		{
			get { return this.SmtpClient.Host; }
			set { this.SmtpClient.Host = value; }
		}

		public virtual string PickupDirectoryLocation
		{
			get { return this.SmtpClient.PickupDirectoryLocation; }
			set { this.SmtpClient.PickupDirectoryLocation = value; }
		}

		public virtual int Port
		{
			get { return this.SmtpClient.Port; }
			set { this.SmtpClient.Port = value; }
		}

		public virtual ServicePoint ServicePoint
		{
			get { return this.SmtpClient.ServicePoint; }
		}

		protected internal virtual TSmtpClient SmtpClient
		{
			get { return this._smtpClient; }
		}

		public virtual string TargetName
		{
			get { return this.SmtpClient.TargetName; }
			set { this.SmtpClient.TargetName = value; }
		}

		public virtual int Timeout
		{
			get { return this.SmtpClient.Timeout; }
			set { this.SmtpClient.Timeout = value; }
		}

		public virtual bool UseDefaultCredentials
		{
			get { return this.SmtpClient.UseDefaultCredentials; }
			set { this.SmtpClient.UseDefaultCredentials = value; }
		}

		#endregion

		#region Methods

		public virtual void Send(MailMessage message)
		{
			this.SmtpClient.Send(message);
		}

		public virtual void Send(string from, string recipients, string subject, string body)
		{
			this.SmtpClient.Send(from, recipients, subject, body);
		}

		public virtual void SendAsync(MailMessage message, object userToken)
		{
			this.SmtpClient.SendAsync(message, userToken);
		}

		public virtual void SendAsync(string from, string recipients, string subject, string body, object userToken)
		{
			this.SmtpClient.SendAsync(from, recipients, subject, body, userToken);
		}

		public virtual void SendAsyncCancel()
		{
			this.SmtpClient.SendAsyncCancel();
		}

		#endregion
	}
}