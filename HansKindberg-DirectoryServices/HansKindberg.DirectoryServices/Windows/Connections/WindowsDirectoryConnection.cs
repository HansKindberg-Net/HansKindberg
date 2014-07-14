using System.Globalization;
using System.Linq;
using System.Text;
using HansKindberg.DirectoryServices.Connections;

namespace HansKindberg.DirectoryServices.Windows.Connections
{
	public class WindowsDirectoryConnection : GeneralDirectoryConnection, IWindowsDirectoryConnection
	{
		#region Fields

		private readonly WindowsDirectoryUri _url = new WindowsDirectoryUri();

		#endregion

		#region Properties

		IDirectoryAuthentication IWindowsDirectoryConnection.Authentication
		{
			get { return this.Authentication; }
		}

		protected internal virtual string LocalPathDelimiter
		{
			get { return WindowsDirectoryUri.DefaultLocalPathDelimiter.ToString(CultureInfo.InvariantCulture); }
		}

		IWindowsDirectoryUri IWindowsDirectoryConnection.Url
		{
			get { return this.Url; }
		}

		public virtual WindowsDirectoryUri Url
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

			if(!string.IsNullOrEmpty(this.Url.Host))
				this.AddParameter(stringBuilder, "Host", this.Url.Host);

			if(this.Url.LocalPath != null && this.Url.LocalPath.Any())
				this.AddParameter(stringBuilder, "LocalPath", this.LocalPathDelimiter + string.Join(this.LocalPathDelimiter, this.Url.LocalPath.ToArray()));

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