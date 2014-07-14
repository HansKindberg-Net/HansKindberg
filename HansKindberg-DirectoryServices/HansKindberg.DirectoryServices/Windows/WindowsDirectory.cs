using System;
using System.Collections.Generic;
using System.DirectoryServices;
using HansKindberg.DirectoryServices.Windows.Connections;

namespace HansKindberg.DirectoryServices.Windows
{
	public class WindowsDirectory : GeneralDirectory, IGlobalWindowsDirectory, IWindowsDirectory
	{
		#region Fields

		private readonly WindowsDirectoryConnection _connection;
		private readonly ILocalPathParser _localPathParser;
		private readonly IWindowsDirectoryUriParser _windowsDirectoryUriParser;

		#endregion

		#region Constructors

		public WindowsDirectory(ILocalPathParser localPathParser, IWindowsDirectoryUriParser windowsDirectoryUriParser) : this(new WindowsDirectoryConnection(), localPathParser, windowsDirectoryUriParser) {}

		public WindowsDirectory(IWindowsDirectoryConnection connection, ILocalPathParser localPathParser, IWindowsDirectoryUriParser windowsDirectoryUriParser)
		{
			if(connection == null)
				throw new ArgumentNullException("connection");

			if(localPathParser == null)
				throw new ArgumentNullException("localPathParser");

			if(windowsDirectoryUriParser == null)
				throw new ArgumentNullException("windowsDirectoryUriParser");

			this._connection = new WindowsDirectoryConnection();
			this._connection.Authentication.AuthenticationTypes = connection.Authentication.AuthenticationTypes;
			this._connection.Authentication.Password = connection.Authentication.Password;
			this._connection.Authentication.UserName = connection.Authentication.UserName;
			this._connection.Url.Host = connection.Url.Host;

			if(connection.Url.LocalPath != null)
			{
				foreach(var part in connection.Url.LocalPath)
				{
					this._connection.Url.LocalPath.Add(part);
				}
			}

			this._connection.Url.Port = connection.Url.Port;
			this._connection.Url.Scheme = connection.Url.Scheme;
			this._localPathParser = localPathParser;
			this._windowsDirectoryUriParser = windowsDirectoryUriParser;
		}

		#endregion

		#region Properties

		protected internal virtual IDirectoryAuthentication Authentication
		{
			get { return this.Connection.Authentication; }
		}

		public virtual AuthenticationTypes? AuthenticationTypes
		{
			get { return this.Connection.Authentication.AuthenticationTypes; }
			set { this.Connection.Authentication.AuthenticationTypes = value; }
		}

		protected internal virtual WindowsDirectoryConnection Connection
		{
			get { return this._connection; }
		}

		public virtual string Host
		{
			get { return this.Connection.Url.Host; }
			set { this.Connection.Url.Host = value; }
		}

		public virtual IList<string> LocalPath
		{
			get { return this.Connection.Url.LocalPath; }
		}

		protected internal virtual ILocalPathParser LocalPathParser
		{
			get { return this._localPathParser; }
		}

		public virtual string Password
		{
			get { return this.Connection.Authentication.Password; }
			set { this.Connection.Authentication.Password = value; }
		}

		public virtual int? Port
		{
			get { return this.Connection.Url.Port; }
			set { this.Connection.Url.Port = value; }
		}

		public virtual IWindowsDirectoryItem Root
		{
			get { return this.Get(this.Url, this.Authentication); }
		}

		public virtual WindowsScheme Scheme
		{
			get { return this.Connection.Url.Scheme; }
			set { this.Connection.Url.Scheme = value; }
		}

		protected internal virtual IWindowsDirectoryUri Url
		{
			get { return this.Connection.Url; }
		}

		public virtual string UserName
		{
			get { return this.Connection.Authentication.UserName; }
			set { this.Connection.Authentication.UserName = value; }
		}

		protected internal virtual IWindowsDirectoryUriParser WindowsDirectoryUriParser
		{
			get { return this._windowsDirectoryUriParser; }
		}

		#endregion

		#region Methods

		protected internal virtual IWindowsDirectoryItem CreateWindowsDirectoryItem(DirectoryEntry directoryEntry)
		{
			if(directoryEntry == null)
				return null;

			var windowsDirectoryItem = new WindowsDirectoryItem
			{
				Url = this.WindowsDirectoryUriParser.Parse(directoryEntry.Path)
			};

			foreach(string propertyName in directoryEntry.Properties.PropertyNames)
			{
				windowsDirectoryItem.Properties.Add(propertyName, directoryEntry.Properties[propertyName].Value);
			}

			return windowsDirectoryItem;
		}

		protected internal virtual IWindowsDirectoryUri CreateWindowsDirectoryUri(IList<string> localPath)
		{
			if(localPath == null)
				throw new ArgumentNullException("localPath");

			var windowsDirectoryUri = new WindowsDirectoryUri
			{
				Host = this.Host,
				Port = this.Port,
				Scheme = this.Scheme
			};

			windowsDirectoryUri.LocalPath.AddRange(localPath);

			return windowsDirectoryUri;
		}

		IWindowsDirectoryItem IWindowsDirectory.Get(string localPath)
		{
			return this.Get(this.LocalPathParser.Parse(localPath));
		}

		public virtual IWindowsDirectoryItem Get(IList<string> localPath)
		{
			return this.Get(this.CreateWindowsDirectoryUri(localPath), this.Authentication);
		}

		IWindowsDirectoryItem IGlobalWindowsDirectory.Get(string path)
		{
			return this.Get(path, null);
		}

		public virtual IWindowsDirectoryItem Get(IWindowsDirectoryUri url)
		{
			return this.Get(url, null);
		}

		public virtual IWindowsDirectoryItem Get(string path, IDirectoryAuthentication authentication)
		{
			using(var directoryEntry = this.GetDirectoryEntry(path, authentication))
			{
				return this.CreateWindowsDirectoryItem(directoryEntry);
			}
		}

		public virtual IWindowsDirectoryItem Get(IWindowsDirectoryUri url, IDirectoryAuthentication authentication)
		{
			if(url == null)
				throw new ArgumentNullException("url");

			return this.Get(url.ToString(), authentication);
		}

		public virtual IEnumerable<IWindowsDirectoryItem> GetChildren()
		{
			return this.GetChildren(this.Url, this.Authentication);
		}

		IEnumerable<IWindowsDirectoryItem> IWindowsDirectory.GetChildren(string localPath)
		{
			return this.GetChildren(this.LocalPathParser.Parse(localPath));
		}

		public virtual IEnumerable<IWindowsDirectoryItem> GetChildren(IList<string> localPath)
		{
			return this.GetChildren(this.CreateWindowsDirectoryUri(localPath), this.Authentication);
		}

		IEnumerable<IWindowsDirectoryItem> IGlobalWindowsDirectory.GetChildren(string path)
		{
			return this.GetChildren(path, null);
		}

		public virtual IEnumerable<IWindowsDirectoryItem> GetChildren(IWindowsDirectoryUri url)
		{
			return this.GetChildren(url, null);
		}

		public virtual IEnumerable<IWindowsDirectoryItem> GetChildren(string path, IDirectoryAuthentication authentication)
		{
			var children = new List<IWindowsDirectoryItem>();

			using(var directoryEntry = this.GetDirectoryEntry(path, authentication))
			{
				foreach(DirectoryEntry child in directoryEntry.Children)
				{
					using(child)
					{
						children.Add(this.CreateWindowsDirectoryItem(child));
					}
				}
			}

			return children.ToArray();
		}

		public virtual IEnumerable<IWindowsDirectoryItem> GetChildren(IWindowsDirectoryUri url, IDirectoryAuthentication authentication)
		{
			if(url == null)
				throw new ArgumentNullException("url");

			return this.GetChildren(url.ToString(), authentication);
		}

		#endregion
	}
}