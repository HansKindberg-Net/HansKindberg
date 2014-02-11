using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Models;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Presenters;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Views.Home;

namespace HansKindberg.Web.Samples.MvpApplication.Presenters
{
	public class HomePresenter : Presenter<IHomeView>
	{
		#region Constructors

		public HomePresenter(IHomeView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<HomeModel>();
		}

		#endregion
	}
}