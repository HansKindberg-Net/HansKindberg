using System.Web.UI;
using HansKindberg.Web.Samples.MvpApplication.Business.Web.Mvp.UI.Views;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Samples.MvpApplication.Views.HtmlTransforming.Clear
{
	[PresenterBinding(typeof(HtmlTransformingPresenter))]
	public partial class Index : Page<HtmlTransformingModel>, IHtmlTransformingView
	{
		#region Properties

		public virtual Control HtmlTransformersControl
		{
			get { return this.HtmlTransformerRepeater; }
		}

		#endregion
	}
}