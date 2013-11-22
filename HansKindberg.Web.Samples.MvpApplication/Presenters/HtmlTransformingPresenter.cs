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
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<HtmlTransformingModel>();
		}

		#endregion
	}
}