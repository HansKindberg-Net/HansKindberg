using System;
using System.Collections.Specialized;
using System.Web;
using HansKindberg.Collections.Specialized.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.UnitTests.Collections.Specialized.Extensions
{
	[TestClass]
	public class NameValueCollectionExtensionTest
	{
		#region Methods

		private static void ToQueryStringTest(string expectedQueryString, NameValueCollection nameValueCollection, bool reverseTest = false)
		{
			Assert.AreEqual(expectedQueryString, nameValueCollection.ToQueryString());

			if(!reverseTest)
				return;

			NameValueCollection parsedNameValueCollection = HttpUtility.ParseQueryString(expectedQueryString);
			Assert.AreEqual(nameValueCollection.Count, parsedNameValueCollection.Count);
			for(int i = 0; i < nameValueCollection.Count; i++)
			{
				Assert.AreEqual(nameValueCollection[i], parsedNameValueCollection[i]);
				Assert.AreEqual(nameValueCollection.Keys[i], parsedNameValueCollection.Keys[i]);
			}
		}

		[TestMethod]
		public void ToQueryString_IfTheNameValueCollectionIsEmpty_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new NameValueCollection().ToQueryString());
		}

		[TestMethod]
		public void ToQueryString_IfTheNameValueCollectionIsNotEmptyAndIfTheOmitLeadingQuestionMarkParameterIsFalse_ShouldReturnAStringWithoutALeadingQuestionMark()
		{
			NameValueCollection nameValueCollection = new NameValueCollection {{"Test", "Test"}};
			Assert.IsTrue(nameValueCollection.ToQueryString(false).StartsWith("?", StringComparison.OrdinalIgnoreCase));
		}

		[TestMethod]
		public void ToQueryString_IfTheNameValueCollectionIsNotEmptyAndIfTheOmitLeadingQuestionMarkParameterIsTrue_ShouldReturnAStringWithoutALeadingQuestionMark()
		{
			NameValueCollection nameValueCollection = new NameValueCollection {{"Test", "Test"}};
			Assert.IsTrue(nameValueCollection.ToQueryString(true).StartsWith("T", StringComparison.OrdinalIgnoreCase));
		}

		[TestMethod]
		public void ToQueryString_IfTheNameValueCollectionIsNotEmpty_ShouldIncludeALeadingQuestionMarkByDefault()
		{
			NameValueCollection nameValueCollection = new NameValueCollection {{"Test", "Test"}};
			Assert.IsTrue(nameValueCollection.ToQueryString().StartsWith("?", StringComparison.OrdinalIgnoreCase));
		}

		[TestMethod]
		public void ToQueryString_IfTheNameValueCollectionIsNotEmpty_ShouldReturnAQueryString()
		{
			ToQueryStringTest(string.Empty, new NameValueCollection {{null, null}, {null, null}});

			ToQueryStringTest("?,First,Second", new NameValueCollection {{null, null}, {null, string.Empty}, {null, "First"}, {null, "Second"}}, true);

			ToQueryStringTest("?Test=+First,+Second+,++Third+++", new NameValueCollection {{"Test", " First"}, {"Test", " Second "}, {"Test", "  Third   "}}, true);

			ToQueryStringTest("?FirstName=FirstValue&SecondName=SecondValue&ThirdName=ThirdValue", new NameValueCollection {{"FirstName", "FirstValue"}, {"SecondName", "SecondValue"}, {"ThirdName", "ThirdValue"}}, true);

			ToQueryStringTest("?Test", new NameValueCollection {{null, "Test"}}, true);

			ToQueryStringTest("?FirstValue&=SecondValue", new NameValueCollection {{null, "FirstValue"}, {string.Empty, "SecondValue"}}, true);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ToQueryString_IfTheNameValueCollectionIsNull_ShouldThrowAnArgumentNullException()
		{
			((NameValueCollection) null).ToQueryString();
		}

		[TestMethod]
		public void ToQueryString_PrerequisiteTest()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			const string expectedQuery = "?NameOne=ValueOne&NameTwo=ValueTwo";
			Assert.AreEqual(expectedQuery, new Uri("http://localhost/Default.html" + expectedQuery).Query);

			NameValueCollection nameValueCollection = new NameValueCollection {{null, null}};
			Assert.AreEqual(1, nameValueCollection.Count);

			nameValueCollection = new NameValueCollection {{null, null}, {null, null}};
			Assert.AreEqual(1, nameValueCollection.Count);

			nameValueCollection = new NameValueCollection {{null, null}, {null, string.Empty}, {null, "First"}, {null, "Second"}};
			Assert.AreEqual(nameValueCollection[null], ",First,Second");

			nameValueCollection = HttpUtility.ParseQueryString("?Test");
			Assert.AreEqual(1, nameValueCollection.Count);
			Assert.IsNull(nameValueCollection.Keys[0]);
			Assert.AreEqual("Test", nameValueCollection[0]);
			Assert.AreEqual("Test", nameValueCollection[null]);

			nameValueCollection = HttpUtility.ParseQueryString("?Test&&Test");
			Assert.AreEqual(1, nameValueCollection.Count);
			Assert.IsNull(nameValueCollection.Keys[0]);
			Assert.AreEqual("Test,,Test", nameValueCollection[0]);
			Assert.AreEqual("Test,,Test", nameValueCollection[null]);

			Assert.AreEqual(1, string.Empty.Split(",".ToCharArray(), StringSplitOptions.None).Length);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ToQueryString_WithOneParameter_IfTheNameValueCollectionIsNotNullAndIfTheEncodingParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new NameValueCollection().ToQueryString(null);
		}

		#endregion
	}
}