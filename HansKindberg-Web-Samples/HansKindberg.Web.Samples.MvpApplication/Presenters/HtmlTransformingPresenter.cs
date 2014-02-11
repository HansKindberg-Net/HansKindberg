using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Models;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Presenters;
using HansKindberg.Web.Samples.MvpApplication.Models;
using HansKindberg.Web.Samples.MvpApplication.Views.HtmlTransforming;

namespace HansKindberg.Web.Samples.MvpApplication.Presenters
{
	public class HtmlTransformingPresenter : Presenter<IHtmlTransformingView>
	{
		#region Constructors

		public HtmlTransformingPresenter(IHtmlTransformingView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
			this.View.PreRender += this.OnViewPreRender;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<HtmlTransformingModel>();
		}

		protected internal virtual void OnViewPreRender(object sender, EventArgs e)
		{
			this.View.HtmlTransformersControl.DataBind();
		}

		#endregion
	}
}