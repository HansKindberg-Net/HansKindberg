namespace HansKindberg.DirectoryServices.Windows
{
	public class WindowsDirectoryItem : GeneralDirectoryItem, IWindowsDirectoryItem
	{
		#region Properties

		public override string Path
		{
			get { return this.Url != null ? this.Url.ToString() : null; }
		}

		public virtual IWindowsDirectoryUri Url { get; set; }

		#endregion
	}
}