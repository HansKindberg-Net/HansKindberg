using System.Diagnostics.CodeAnalysis;
using HansKindberg.Web.Samples.MvpApplication.Business.Web.Mvp.UI.Views;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Samples.MvpApplication.Views.Shared
{
	[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	[PresenterBinding(typeof(LayoutPresenter))]
	public partial class Layout : MasterPage<LayoutModel>, ILayoutView {}
}