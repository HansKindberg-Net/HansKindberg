﻿using System.Linq;
using System.Text;

namespace HansKindberg.DirectoryServices.Connections
{
	public class DirectoryConnection : GeneralDirectoryConnection, IDirectoryConnection
	{
		#region Fields

		private readonly DirectoryUri _url = new DirectoryUri();

		#endregion

		#region Properties

		IDirectoryAuthentication IDirectoryConnection.Authentication
		{
			get { return this.Authentication; }
		}

		IDirectoryUri IDirectoryConnection.Url
		{
			get { return this.Url; }
		}

		public virtual DirectoryUri Url
		{
			get { return this._url; }
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			if(this.Authentication.AuthenticationTypes.HasValue)
				this.AddParameter(stringBuilder, "AuthenticationTypes", this.Authentication.AuthenticationTypes.Value);

			if(this.Url.DistinguishedName != null && this.Url.DistinguishedName.Components.Any())
				this.AddParameter(stringBuilder, "DistinguishedName", this.Url.DistinguishedName);

			if(!string.IsNullOrEmpty(this.Url.Host))
				this.AddParameter(stringBuilder, "Host", this.Url.Host);

			if(!string.IsNullOrEmpty(this.Authentication.Password))
				this.AddParameter(stringBuilder, "Password", this.Authentication.Password);

			if(this.Url.Port.HasValue)
				this.AddParameter(stringBuilder, "Port", this.Url.Port.Value);

			this.AddParameter(stringBuilder, "Scheme", this.Url.Scheme);

			if(!string.IsNullOrEmpty(this.Authentication.UserName))
				this.AddParameter(stringBuilder, "UserName", this.Authentication.UserName);

			return stringBuilder.ToString();
		}

		#endregion
	}
}