using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Linq;

namespace HansKindberg.DirectoryServices
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a wrapper.")]
	public class DirectoryEntriesWrapper : IDirectoryEntries
	{
		#region Fields

		private readonly DirectoryEntries _directoryEntries;

		#endregion

		#region Constructors

		public DirectoryEntriesWrapper(DirectoryEntries directoryEntries)
		{
			if(directoryEntries == null)
				throw new ArgumentNullException("directoryEntries");

			this._directoryEntries = directoryEntries;
		}

		#endregion

		#region Methods

		public virtual IDirectoryEntry Add(string name, string schemaClassName)
		{
			return (DirectoryEntryWrapper) this._directoryEntries.Add(name, schemaClassName);
		}

		public virtual IDirectoryEntry Find(string name)
		{
			return (DirectoryEntryWrapper) this._directoryEntries.Find(name);
		}

		public virtual IDirectoryEntry Find(string name, string schemaClassName)
		{
			return (DirectoryEntryWrapper) this._directoryEntries.Find(name, schemaClassName);
		}

		public static DirectoryEntriesWrapper FromDirectoryEntries(DirectoryEntries directoryEntries)
		{
			return directoryEntries;
		}

		public virtual IEnumerator<IDirectoryEntry> GetEnumerator()
		{
			return this.ToList().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual void Remove(IDirectoryEntry entry)
		{
			throw new NotImplementedException("Don't know how to implement this yet.");
		}

		protected internal virtual IList<IDirectoryEntry> ToList()
		{
			return (from DirectoryEntry directoryEntry in this._directoryEntries select (DirectoryEntryWrapper) directoryEntry).Cast<IDirectoryEntry>().ToList();
		}

		#endregion

		#region Implicit operator

		public static implicit operator DirectoryEntriesWrapper(DirectoryEntries directoryEntries)
		{
			return directoryEntries == null ? null : new DirectoryEntriesWrapper(directoryEntries);
		}

		#endregion
	}
}