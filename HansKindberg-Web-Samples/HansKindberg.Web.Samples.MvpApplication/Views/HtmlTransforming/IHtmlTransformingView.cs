using System.Web.UI;
using HansKindberg.Web.Samples.MvpApplication.Models;

namespace HansKindberg.Web.Samples.MvpApplication.Views.HtmlTransforming
{
	public interface IHtmlTransformingView : IView<HtmlTransformingModel>
	{
		#region Properties

		Control HtmlTransformersControl { get; }

		#endregion
	}
}