namespace HansKindberg.Web.HtmlTransforming
{
	public interface IHtmlTransformingInitializer
	{
		#region Methods

		void Initialize(IHttpApplication httpApplication);

		#endregion
	}
}