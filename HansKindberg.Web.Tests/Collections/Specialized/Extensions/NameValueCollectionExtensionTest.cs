using System;
using System.Collections.Specialized;
using System.Web;
using HansKindberg.Collections.Specialized.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.Tests.Collections.Specialized.Extensions
{
	[TestClass]
	public class NameValueCollectionExtensionTest
	{
		#region Methods

		private static void ToQueryStringTest(string expectedQueryString, NameValueCollection nameValueCollection)
		{
			Assert.AreEqual(expectedQueryString, nameValueCollection.ToQueryString());
		}

		[TestMethod]
		public void ToQueryString_IfTheNameValueCollectionIsEmpty_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new NameValueCollection().ToQueryString());
		}

		[TestMethod]
		public void ToQueryString_IfTheNameValueCollectionIsNotEmpty_ShouldReturnAQueryString()
		{
			ToQueryStringTest(string.Empty, new NameValueCollection {{null, null}, {null, null}});

			ToQueryStringTest("?,First,Second", new NameValueCollection {{null, null}, {null, string.Empty}, {null, "First"}, {null, "Second"}});

			ToQueryStringTest("?Test=+First,+Second+,++Third+++", new NameValueCollection {{"Test", " First"}, {"Test", " Second "}, {"Test", "  Third   "}});

			ToQueryStringTest("?FirstName=FirstValue&SecondName=SecondValue&ThirdName=ThirdValue", new NameValueCollection {{"FirstName", "FirstValue"}, {"SecondName", "SecondValue"}, {"ThirdName", "ThirdValue"}});

			ToQueryStringTest("?Test", new NameValueCollection {{null, "Test"}});

			ToQueryStringTest("?FirstValue&=SecondValue", new NameValueCollection {{null, "FirstValue"}, {string.Empty, "SecondValue"}});
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