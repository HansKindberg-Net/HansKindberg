using HansKindberg.Web.Samples.MvpApplication.Business.Web.Mvp.UI.Views;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Presenters;
using HansKindberg.Web.Samples.MvpApplication.Views.Home;
using WebFormsMvp;

namespace HansKindberg.Web.Samples.MvpApplication
{
	[PresenterBinding(typeof(HomePresenter))]
	public partial class Index : Page<HomeModel>, IHomeView {}
}