using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryUriParserTest
	{
		#region Methods

		private static DirectoryUriParser CreateDirectoryUriParser()
		{
			return new DirectoryUriParser(Mock.Of<IDistinguishedNameParser>());
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void Parse_IfTheValueParameterIsEmpty_ShouldThrowAFormatException()
		{
			try
			{
				CreateDirectoryUriParser().Parse(string.Empty);
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
				CreateDirectoryUriParser().Parse(null);
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
				CreateDirectoryUriParser().Parse("http://localhost");
			}
			catch(FormatException formatException)
			{
				if(formatException.Message == "The directory-uri \"http://localhost\" is invalid." && formatException.InnerException is ArgumentException)
					throw;
			}
		}

		[TestMethod]
		public void Parse_ShouldKeepHostCase()
		{
			const string host = "TeSt";
			Assert.AreNotEqual(host, "test");
			Assert.AreEqual(host, CreateDirectoryUriParser().Parse("LDAP://" + host).Host);
		}

		#endregion
	}
}