using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using HansKindberg.DirectoryServices.Connections;

namespace HansKindberg.DirectoryServices
{
	public class Directory : IDirectory
	{
		#region Fields

		private readonly ILdapConnectionSettings _connectionSettings;

		#endregion

		#region Constructors

		public Directory(ILdapConnectionSettings connectionSettings)
		{
			if(connectionSettings == null)
				throw new ArgumentNullException("connectionSettings");

			this._connectionSettings = connectionSettings;
		}

		#endregion

		#region Properties

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public virtual IDirectoryEntry Root
		{
			get
			{
				DirectoryEntry directoryEntry = new DirectoryEntry(this._connectionSettings.Path, this._connectionSettings.UserName, this._connectionSettings.Password);

				if(this._connectionSettings.AuthenticationTypes.HasValue)
					directoryEntry.AuthenticationType = this._connectionSettings.AuthenticationTypes.Value;

				return new DirectoryEntryWrapper(directoryEntry);
			}
		}

		#endregion
	}
}