using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices.Windows
{
	public class LocalPathParser : ILocalPathParser
	{
		#region Methods

		public virtual IList<string> Parse(string value)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			var localPath = new List<string>();

			if(value.Length > 0)
				localPath.AddRange(value.Split(new[] {WindowsDirectoryUri.DefaultLocalPathDelimiter}));

			return localPath;
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool TryParse(string value, out IList<string> localPath)
		{
			localPath = null;

			if(value == null)
				return false;

			try
			{
				localPath = this.Parse(value);
				return true;
			}
			catch
			{
				localPath = null;
				return false;
			}
		}

		#endregion
	}
}