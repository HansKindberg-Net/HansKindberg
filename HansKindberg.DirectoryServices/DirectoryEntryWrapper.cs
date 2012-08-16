using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class DirectoryEntryWrapper : IDirectoryEntry
	{
		#region Fields

		private readonly DirectoryEntry _directoryEntry;

		#endregion

		#region Constructors

		public DirectoryEntryWrapper(DirectoryEntry directoryEntry)
		{
			if(directoryEntry == null)
				throw new ArgumentNullException("directoryEntry");

			this._directoryEntry = directoryEntry;
		}

		#endregion

		#region Properties

		public virtual IDirectoryEntries Children
		{
			get { return (DirectoryEntriesWrapper) this._directoryEntry.Children; }
		}

		public virtual IDirectoryEntry Parent
		{
			get { return (DirectoryEntryWrapper) this._directoryEntry.Parent; }
		}

		public virtual string Path
		{
			get { return this._directoryEntry.Path; }
			set { this._directoryEntry.Path = value; }
		}

		public virtual IPropertyCollection Properties
		{
			get { return (PropertyCollectionWrapper) this._directoryEntry.Properties; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			this._directoryEntry.Dispose();
		}

		public static DirectoryEntryWrapper FromDirectoryEntry(DirectoryEntry directoryEntry)
		{
			return directoryEntry;
		}

		#endregion

		#region Implicit operator

		public static implicit operator DirectoryEntryWrapper(DirectoryEntry directoryEntry)
		{
			return directoryEntry == null ? null : new DirectoryEntryWrapper(directoryEntry);
		}

		#endregion
	}
}