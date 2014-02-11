using System;
using HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Views;
using WebFormsMvp.Web;

namespace HansKindberg.Web.Samples.MvpApplication.Business.Web.Mvp.UI.Views
{
	public class MasterPage : System.Web.UI.MasterPage, IView
	{
		#region Properties

		public virtual bool AutoDataBind
		{
			get { return false; }
		}

		public virtual bool ThrowExceptionIfNoPresenterBound
		{
			get { return true; }
		}

		#endregion

		#region Methods

		protected override void OnInit(EventArgs e)
		{
			PageViewHost.Register(this, this.Context, this.AutoDataBind);

			base.OnInit(e);
		}

		#endregion
	}

	public class MasterPage<TModel> : MasterPage, IView<TModel>
	{
		#region Properties

		public virtual TModel Model { get; set; }

		#endregion
	}
}