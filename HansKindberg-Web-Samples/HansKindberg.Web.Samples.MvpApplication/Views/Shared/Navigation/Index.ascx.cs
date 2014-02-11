using HansKindberg.Web.Samples.MvpApplication.Business.Web.Mvp.UI.Views;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Samples.MvpApplication.Views.Shared.Navigation
{
	[PresenterBinding(typeof(NavigationPresenter))]
	public partial class Index : UserControl<NavigationModel>, INavigationView {}
}