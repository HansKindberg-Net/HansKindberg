using HansKindberg.Configuration;

namespace HansKindberg.Web.HtmlTransforming
{
	public static class HtmlTransformingInitializer
	{
		#region Fields

		private static volatile IHtmlTransformingInitializer _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IHtmlTransformingInitializer Instance
		{
			get
			{
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
							_instance = new DefaultHtmlTransformingInitializer(new DefaultHtmlInvestigator(), new DefaultHtmlDocumentFactory(), new DefaultHtmlTransformingContext(new ConfigurationManagerWrapper(), new DefaultHtmlTransformerFactory()));
					}
				}

				return _instance;
			}
			set
			{
				if(value == _instance)
					return;

				lock(_lockObject)
				{
					_instance = value;
				}
			}
		}

		#endregion
	}
}