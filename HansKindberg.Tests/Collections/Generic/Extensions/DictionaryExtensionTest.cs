using System;
using System.Collections.Generic;
using HansKindberg.Collections.Generic.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Tests.Collections.Generic.Extensions
{
	[TestClass]
	public class DictionaryExtensionTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TryGetValueAndRemove_IfTheDictionaryIsNull_ShouldThrowAnArgumentNullException()
		{
			string outValue;
			((IDictionary<string, string>) null).TryGetValueAndRemove("Test", out outValue);
		}

		[TestMethod]
		public void TryGetValueAndRemove_IfTheKeyExists_ShouldRemoveTheKey()
		{
			const string key = "TestKey";
			Dictionary<string, string> dictionary = new Dictionary<string, string> {{key, "TestValue"}};
			Assert.AreEqual(1, dictionary.Count);
			string outValue;
			dictionary.TryGetValueAndRemove(key, out outValue);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		public void TryGetValueAndRemove_IfTheKeyExists_ShouldReturnTrue()
		{
			const string key = "TestKey";
			Dictionary<string, string> dictionary = new Dictionary<string, string> {{key, "TestValue"}};
			string outValue;
			Assert.IsTrue(dictionary.TryGetValueAndRemove(key, out outValue));
		}

		[TestMethod]
		public void TryGetValueAndRemove_IfTheKeyExists_ShouldSetTheOutValue()
		{
			const string key = "TestKey";
			const string value = "TestValue";
			Dictionary<string, string> dictionary = new Dictionary<string, string> {{key, value}};
			string outValue;
			dictionary.TryGetValueAndRemove(key, out outValue);
			Assert.AreEqual(value, outValue);
		}

		#endregion
	}
}