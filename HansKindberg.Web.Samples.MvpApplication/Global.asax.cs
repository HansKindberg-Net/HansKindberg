﻿using System;
using HansKindberg.Web.Samples.MvpApplication.Business;

namespace HansKindberg.Web.Samples.MvpApplication
{
	public class Global : System.Web.HttpApplication
	{
		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			Bootstrapper.Bootstrap();
		}

		#endregion
	}
}