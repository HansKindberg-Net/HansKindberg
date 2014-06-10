namespace HansKindberg.DirectoryServices
{
	public class DirectoryUri : IDirectoryUri
	{
		#region Properties

		public virtual string DistinguishedName { get; set; }
		public virtual string Host { get; set; }
		public virtual int? Port { get; set; }
		public virtual Scheme Scheme { get; set; }

		#endregion

		#region Methods

		public override string ToString()
		{
			var directoryUri = this.Host;

			if(this.Port.HasValue)
				directoryUri += (!string.IsNullOrEmpty(directoryUri) ? ":" : string.Empty) + this.Port.Value;

			if(!string.IsNullOrEmpty(this.DistinguishedName))
				directoryUri += (!string.IsNullOrEmpty(directoryUri) ? "/" : string.Empty) + this.DistinguishedName;

			directoryUri = this.Scheme + "://" + directoryUri;

			return directoryUri;
		}

		#endregion
	}
}