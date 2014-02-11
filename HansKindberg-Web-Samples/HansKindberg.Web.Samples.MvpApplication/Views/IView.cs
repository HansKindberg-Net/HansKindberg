using System;

namespace HansKindberg.Web.Samples.MvpApplication.Views
{
	public interface IView<TModel> : HansKindberg.Web.Samples.MvpApplication.Business.Mvp.Views.IView<TModel>
	{
		#region Events

		event EventHandler PreRender;

		#endregion
	}
}