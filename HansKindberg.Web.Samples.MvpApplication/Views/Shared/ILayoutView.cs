using System;
using System.Web.UI;
using HansKindberg.Web.Samples.MvpApplication.Models;

namespace HansKindberg.Web.Samples.MvpApplication.Views.Shared
{
	public interface ILayoutView : IView<LayoutModel>
	{
		#region Events

		event EventHandler PreRender;

		#endregion

		#region Properties

		Control HtmlControl { get; }
		//Control NavigationControl { get; }

		#endregion
	}
}