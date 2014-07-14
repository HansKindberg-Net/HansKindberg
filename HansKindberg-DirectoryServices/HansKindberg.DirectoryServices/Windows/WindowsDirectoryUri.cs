using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace HansKindberg.DirectoryServices.Windows
{
	public class WindowsDirectoryUri : IWindowsDirectoryUri
	{
		#region Fields

		public const char DefaultLocalPathDelimiter = '/';
		private readonly List<string> _localPath = new List<string>();

		#endregion

		#region Properties

		public virtual string Host { get; set; }

		IList<string> IWindowsDirectoryUri.LocalPath
		{
			get { return this.LocalPath; }
		}

		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public virtual List<string> LocalPath
		{
			get { return this._localPath; }
		}

		protected internal virtual char LocalPathDelimiter
		{
			get { return DefaultLocalPathDelimiter; }
		}

		public virtual int? Port { get; set; }
		public virtual WindowsScheme Scheme { get; set; }

		#endregion

		#region Methods

		public override string ToString()
		{
			var windowsDirectoryUri = this.Host;

			if(this.Port.HasValue)
				windowsDirectoryUri += (!string.IsNullOrEmpty(windowsDirectoryUri) ? ":" : string.Empty) + this.Port.Value;

			if(this.LocalPath != null && this.LocalPath.Any())
			{
				var localPathDelimiter = this.LocalPathDelimiter.ToString(CultureInfo.InvariantCulture);

				windowsDirectoryUri += (!string.IsNullOrEmpty(windowsDirectoryUri) ? localPathDelimiter : string.Empty) + string.Join(localPathDelimiter, this.LocalPath.ToArray());
			}

			windowsDirectoryUri = this.Scheme + "://" + windowsDirectoryUri;

			return windowsDirectoryUri;
		}

		#endregion
	}
}