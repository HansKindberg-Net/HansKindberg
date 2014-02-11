using System;

namespace HansKindberg.Web.HtmlTransforming
{
	[CLSCompliant(false)]
	public interface IHtmlTransformerFactory
	{
		#region Methods

		T Create<T>() where T : IHtmlTransformer;
		IHtmlTransformer Create(Type type);

		#endregion
	}
}