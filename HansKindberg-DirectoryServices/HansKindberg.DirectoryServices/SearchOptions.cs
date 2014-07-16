using System;
using System.DirectoryServices;

namespace HansKindberg.DirectoryServices
{
	public class SearchOptions : GeneralSearchOptions, ISearchOptions
	{
		#region Properties

		public virtual int? PageSize { get; set; }
		public virtual SearchScope? SearchScope { get; set; }

		#endregion

		#region Methods

		ISearchOptions ISearchOptions.Copy()
		{
			return this.Copy();
		}

		public virtual SearchOptions Copy()
		{
			var searchOptions = new SearchOptions();

			searchOptions.OverrideWith(this);

			return searchOptions;
		}

		ISearchOptions ISearchOptions.Copy(ISearchOptions searchOptionsToOverrideWith)
		{
			return this.Copy(searchOptionsToOverrideWith);
		}

		public virtual SearchOptions Copy(ISearchOptions searchOptionsToOverrideWith)
		{
			var searchOptions = this.Copy();

			searchOptions.OverrideWith(searchOptionsToOverrideWith);

			return searchOptions;
		}

		protected internal virtual void OverrideWith(ISearchOptions searchOptions)
		{
			if(searchOptions == null)
				throw new ArgumentNullException("searchOptions");

			base.OverrideWith(searchOptions);

			if(searchOptions.PageSize != null)
				this.PageSize = searchOptions.PageSize;

			if(searchOptions.SearchScope != null)
				this.SearchScope = searchOptions.SearchScope;
		}

		#endregion
	}
}