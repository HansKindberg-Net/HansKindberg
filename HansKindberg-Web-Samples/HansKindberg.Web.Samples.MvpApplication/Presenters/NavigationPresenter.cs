using System;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Models;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Presenters;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Views.Shared.Navigation;

namespace HansKindberg.Web.Samples.MvpApplication.Presenters
{
	public class NavigationPresenter : Presenter<INavigationView>
	{
		#region Constructors

		public NavigationPresenter(INavigationView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
			this.View.PreRender += this.OnViewPreRender;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<NavigationModel>();
		}

		protected internal virtual void OnViewPreRender(object sender, EventArgs e) {}

		#endregion
	}
}