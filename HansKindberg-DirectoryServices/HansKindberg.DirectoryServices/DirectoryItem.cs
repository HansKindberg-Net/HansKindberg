namespace HansKindberg.DirectoryServices
{
	public class DirectoryItem : GeneralDirectoryItem, IDirectoryItem
	{
		#region Properties

		public override string Path
		{
			get { return this.Url != null ? this.Url.ToString() : null; }
		}

		public virtual IDirectoryUri Url { get; set; }

		#endregion
	}
}