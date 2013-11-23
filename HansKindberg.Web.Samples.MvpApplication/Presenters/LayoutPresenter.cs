using System;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Models;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Presenters;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Views.Shared;

namespace HansKindberg.Web.Samples.MvpApplication.Presenters
{
	public class LayoutPresenter : Presenter<ILayoutView>
	{
		#region Constructors

		public LayoutPresenter(ILayoutView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<LayoutModel>();
		}

		#endregion
	}
}