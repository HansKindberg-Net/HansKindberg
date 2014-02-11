using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Web.HtmlTransforming
{
	[CLSCompliant(false)]
	public interface IHtmlTransformingContext
	{
		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		IEnumerable<IHtmlTransformer> GetTransformers();

		#endregion
	}
}