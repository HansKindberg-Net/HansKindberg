using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryUriParserTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void Parse_IfTheValueParameterIsEmpty_ShouldThrowAFormatException()
		{
			try
			{
				new DirectoryUriParser().Parse(string.Empty);
			}
			catch(FormatException formatException)
			{
				if(formatException.Message == "The directory-uri \"\" is invalid." && formatException.InnerException is UriFormatException)
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Parse_IfTheValueParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new DirectoryUriParser().Parse(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "value")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void Parse_IfTheValueParameterStartsWithTheHttpScheme_ShouldThrowAFormatException()
		{
			try
			{
				new DirectoryUriParser().Parse("http://localhost");
			}
			catch(FormatException formatException)
			{
				if(formatException.Message == "The directory-uri \"http://localhost\" is invalid." && formatException.InnerException is ArgumentException)
					throw;
			}
		}

		#endregion
	}
}