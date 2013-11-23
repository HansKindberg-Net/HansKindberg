using System;
using System.Web;

namespace HansKindberg.Web.Samples.MvpApplication.Models
{
	// You must develop this presenter further. Maybe you should use a sitemap, http://msdn.microsoft.com/en-us/library/yy2ykkab(v=vs.100).aspx.
	public class NavigationModel
	{
		#region Fields

		private readonly string _currentFilePath;

		#endregion

		#region Constructors

		public NavigationModel(HttpRequestBase httpRequest)
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

		#endregion
	}
}