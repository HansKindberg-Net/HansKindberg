using System;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HansKindberg.DirectoryServices.Connections
{
	public class DirectoryConnection : IDirectoryConnection
	{
		#region Fields

		public const char DefaultNameValueDelimiter = '=';
		public const char DefaultParameterDelimiter = ';';

		#endregion

		#region Properties

		public virtual AuthenticationTypes? AuthenticationTypes { get; set; }
		public virtual IDistinguishedName DistinguishedName { get; set; }
		public virtual string Host { get; set; }

		protected internal virtual char NameValueDelimiter
		{
			get { return DefaultNameValueDelimiter; }
		}

		protected internal virtual char ParameterDelimiter
		{
			get { return DefaultParameterDelimiter; }
		}

		public virtual string Password { get; set; }
		public virtual int? Port { get; set; }
		public virtual Scheme? Scheme { get; set; }
		public virtual string UserName { get; set; }

		#endregion

		#region Methods

		protected internal virtual void AddParameter(StringBuilder value, string parameterName, object parameterValue)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(string.IsNullOrEmpty(parameterName))
				throw new ArgumentException("The parameter-name can not be empty.", "parameterName");

			if(string.IsNullOrEmpty(parameterName.Trim()))
				throw new ArgumentException("The parameter-name can not only contain white-spaces.", "parameterName");

			var parameterDelimiter = !string.IsNullOrEmpty(value.ToString().Trim()) ? this.ParameterDelimiter.ToString(CultureInfo.InvariantCulture) : string.Empty;

			value.Append(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new[] {parameterDelimiter, parameterName, this.NameValueDelimiter, parameterValue ?? string.Empty}));
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			if(this.AuthenticationTypes.HasValue)
				this.AddParameter(stringBuilder, "AuthenticationTypes", this.AuthenticationTypes.Value);

			if(this.DistinguishedName != null && this.DistinguishedName.Components.Any())
				this.AddParameter(stringBuilder, "DistinguishedName", this.DistinguishedName);

			if(!string.IsNullOrEmpty(this.Host))
				this.AddParameter(stringBuilder, "Host", this.Host);

			if(!string.IsNullOrEmpty(this.Password))
				this.AddParameter(stringBuilder, "Password", this.Password);

			if(this.Port.HasValue)
				this.AddParameter(stringBuilder, "Port", this.Port.Value);

			if(this.Scheme.HasValue)
				this.AddParameter(stringBuilder, "Scheme", this.Scheme.Value);

			if(!string.IsNullOrEmpty(this.UserName))
				this.AddParameter(stringBuilder, "UserName", this.UserName);

			return stringBuilder.ToString();
		}

		#endregion
	}
}