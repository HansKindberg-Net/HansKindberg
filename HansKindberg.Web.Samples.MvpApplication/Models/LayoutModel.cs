using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace HansKindberg.Web.Samples.MvpApplication.Models
{
	public class LayoutModel
	{
		#region Fields

		private readonly string _currentFilePath;

		#endregion

		#region Constructors

		public LayoutModel(HttpRequestBase httpRequest)
		{
			if(httpRequest == null)
				throw new ArgumentNullException("httpRequest");

			this._currentFilePath = httpRequest.FilePath;
		}

		#endregion

		#region Properties

		public virtual string CurrentFilePath
		{
			get { return this._currentFilePath; }
		}

		public virtual CultureInfo UiCulture
		{
			get { return Thread.CurrentThread.CurrentUICulture; }
		}

		#endregion
	}
}