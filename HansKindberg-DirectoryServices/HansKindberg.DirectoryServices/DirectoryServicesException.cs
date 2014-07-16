using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace HansKindberg.DirectoryServices
{
	[Serializable]
	public class DirectoryServicesException : COMException
	{
		#region Fields

		private static readonly Type _directoryServicesComExceptionType = typeof(DirectoryServicesCOMException);
		private static readonly FieldInfo _extendedErrorField = _directoryServicesComExceptionType.GetField("extendederror", BindingFlags.Instance | BindingFlags.NonPublic);
		private static readonly FieldInfo _extendedErrorMessageField = _directoryServicesComExceptionType.GetField("extendedmessage", BindingFlags.Instance | BindingFlags.NonPublic);
		private const string _extendedErrorMessagePropertyName = "ExtendedErrorMessage";
		private const string _extendedErrorPropertyName = "ExtendedError";

		#endregion

		#region Constructors

		public DirectoryServicesException() {}
		public DirectoryServicesException(string message) : base(message) {}
		public DirectoryServicesException(DirectoryServicesCOMException innerException) : this(null, innerException) {}
		public DirectoryServicesException(string message, Exception innerException) : base(message, innerException) {}

		public DirectoryServicesException(string message, DirectoryServicesCOMException innerException) : base(message == null && innerException != null ? string.Empty : message, innerException)
		{
			if(innerException != null)
				this.HResult = innerException.ErrorCode;
		}

		protected DirectoryServicesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			var directoryServicesComException = this.InnerException as DirectoryServicesCOMException;

			if(directoryServicesComException == null)
				return;

			_extendedErrorField.SetValue(directoryServicesComException, info.GetInt32(_extendedErrorPropertyName));
			_extendedErrorMessageField.SetValue(directoryServicesComException, info.GetString(_extendedErrorMessagePropertyName));
		}

		#endregion

		#region Properties

		public override string Message
		{
			get
			{
				var directoryServicesComException = this.InnerException as DirectoryServicesCOMException;

				if(directoryServicesComException != null)
				{
					var messageList = new List<string>
					{
						this.FormatMessage(base.Message),
						this.FormatMessage(directoryServicesComException.Message),
						this.FormatMessage(directoryServicesComException.ExtendedErrorMessage) + " (" + directoryServicesComException.ExtendedError + ")"
					};

					return string.Join(". ", messageList.Where(value => !string.IsNullOrEmpty(value)).ToArray()) + ".";
				}

				return base.Message;
			}
		}

		#endregion

		#region Methods

		protected internal virtual string FormatMessage(string message)
		{
			if(string.IsNullOrEmpty(message))
				return string.Empty;

			return message.TrimEnd(".".ToCharArray());
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			var directoryServicesComException = this.InnerException as DirectoryServicesCOMException;

			if(directoryServicesComException == null)
				return;

			info.AddValue(_extendedErrorPropertyName, directoryServicesComException.ExtendedError);
			info.AddValue(_extendedErrorMessagePropertyName, directoryServicesComException.ExtendedErrorMessage);
		}

		#endregion
	}
}