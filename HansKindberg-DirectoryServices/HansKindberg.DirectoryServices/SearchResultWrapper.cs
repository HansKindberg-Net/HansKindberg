using System;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public class SearchResultWrapper : ISearchResult
	{
		#region Fields

		private readonly SearchResult _searchResult;

		#endregion

		#region Constructors

		public SearchResultWrapper(SearchResult searchResult)
		{
			if(searchResult == null)
				throw new ArgumentNullException("searchResult");

			this._searchResult = searchResult;
		}

		#endregion

		#region Properties

		public virtual string Path
		{
			get { return this._searchResult.Path; }
		}

		public virtual IResultPropertyCollection Properties
		{
			get { return (ResultPropertyCollectionWrapper) this._searchResult.Properties; }
		}

		#endregion

		#region Methods

		public static SearchResultWrapper FromSearchResult(SearchResult searchResult)
		{
			return searchResult;
		}

		public virtual IDirectoryEntry GetDirectoryEntry()
		{
			return (DirectoryEntryWrapper) this._searchResult.GetDirectoryEntry();
		}

		#endregion

		#region Implicit operator

		public static implicit operator SearchResultWrapper(SearchResult searchResult)
		{
			return searchResult == null ? null : new SearchResultWrapper(searchResult);
		}

		#endregion
	}
}