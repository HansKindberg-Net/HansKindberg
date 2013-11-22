using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using HansKindberg.Web.Samples.MvpApplication.Business.Web.Mvp.UI.Views;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Samples.MvpApplication.Views.Shared
{
	[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	[PresenterBinding(typeof(LayoutPresenter))]
	public partial class Layout : MasterPage<LayoutModel>, ILayoutView
	{
		#region Properties

		public virtual Control HtmlControl
		{
			get { return this.htmlPlaceHolder; }
		}

		//public virtual Control NavigationControl
		//{
		//	get { return this.NavigationPlaceHolder; }
		//}

		#endregion
	}
}