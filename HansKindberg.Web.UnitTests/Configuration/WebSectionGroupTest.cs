using System;
using HansKindberg.Web.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.UnitTests.Configuration
{
	[TestClass]
	public class WebSectionGroupTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void HtmlTransformers_IfNotRetrievedThroughTheConfigurationClass_ShouldThrowAnInvalidOperationException()
		{
			Assert.IsNotNull(new WebSectionGroup().HtmlTransformers);
		}

		#endregion
	}
}