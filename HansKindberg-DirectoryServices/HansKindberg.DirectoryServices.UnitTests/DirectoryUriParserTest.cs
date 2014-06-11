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

		[TestMethod]
		public void Parse_ShouldKeepDistinguishedNameCase()
		{
			const string distinguishedName = "Dc=Test,dC=neT";
			Assert.AreNotEqual(distinguishedName, "dc=test,dc=net");
			Assert.AreEqual(distinguishedName, new DirectoryUriParser().Parse("LDAP://test/" + distinguishedName).DistinguishedName);
		}

		[TestMethod]
		public void Parse_ShouldKeepHostCase()
		{
			const string host = "TeSt";
			Assert.AreNotEqual(host, "test");
			Assert.AreEqual(host, new DirectoryUriParser().Parse("LDAP://" + host).Host);
		}

		#endregion
	}
}