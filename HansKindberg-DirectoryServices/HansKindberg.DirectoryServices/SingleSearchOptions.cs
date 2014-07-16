using System;

namespace HansKindberg.DirectoryServices
{
	public class SingleSearchOptions : GeneralSearchOptions, ISingleSearchOptions
	{
		#region Methods

		ISingleSearchOptions ISingleSearchOptions.Copy()
		{
			return this.Copy();
		}

		public virtual SingleSearchOptions Copy()
		{
			var singleSearchOptions = new SingleSearchOptions();

			singleSearchOptions.OverrideWith(this);

			return singleSearchOptions;
		}

		ISingleSearchOptions ISingleSearchOptions.Copy(ISingleSearchOptions singleSearchOptionsToOverrideWith)
		{
			return this.Copy(singleSearchOptionsToOverrideWith);
		}

		public virtual SingleSearchOptions Copy(ISingleSearchOptions singleSearchOptionsToOverrideWith)
		{
			var singleSearchOptions = this.Copy();

			singleSearchOptions.OverrideWith(singleSearchOptionsToOverrideWith);

			return singleSearchOptions;
		}

		protected internal virtual void OverrideWith(ISingleSearchOptions singleSearchOptions)
		{
			if(singleSearchOptions == null)
				throw new ArgumentNullException("singleSearchOptions");

			base.OverrideWith(singleSearchOptions);
		}

		#endregion
	}
}