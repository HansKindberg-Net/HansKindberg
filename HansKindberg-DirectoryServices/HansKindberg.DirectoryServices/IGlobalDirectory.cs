﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface IGlobalDirectory
	{
		#region Methods

		bool Exists(string path);
		bool Exists(IDirectoryUri url);
		bool Exists(string path, IDirectoryAuthentication authentication);
		bool Exists(IDirectoryUri url, IDirectoryAuthentication authentication);
		IEnumerable<IDirectoryItem> Find(string searchRootPath);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl);
		IEnumerable<IDirectoryItem> Find(string searchRootPath, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, ISearchOptions searchOptions);
		IEnumerable<IDirectoryItem> Find(string searchRootPath, IDirectoryAuthentication authentication);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, IDirectoryAuthentication authentication);
		IEnumerable<IDirectoryItem> Find(string searchRootPath, ISearchOptions searchOptions, IDirectoryAuthentication authentication);
		IEnumerable<IDirectoryItem> Find(IDirectoryUri searchRootUrl, ISearchOptions searchOptions, IDirectoryAuthentication authentication);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path, ISingleSearchOptions singleSearchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url, ISingleSearchOptions singleSearchOptions);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path, IDirectoryAuthentication authentication);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url, IDirectoryAuthentication authentication);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(string path, ISingleSearchOptions singleSearchOptions, IDirectoryAuthentication authentication);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryItem Get(IDirectoryUri url, ISingleSearchOptions singleSearchOptions, IDirectoryAuthentication authentication);

		#endregion
	}
}