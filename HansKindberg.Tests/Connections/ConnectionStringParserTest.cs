using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using HansKindberg.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Tests.Connections
{
	[TestClass]
	public class ConnectionStringParserTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithNoParameters_ShouldSetKeyValuePairDelimiterToSemicolon()
		{
			Assert.AreEqual(';', new ConnectionStringParser().KeyValuePairDelimiter);
		}

		[TestMethod]
		public void Constructor_WithNoParameters_ShouldSetKeyValueSeparatorToEqualsSign()
		{
			Assert.AreEqual('=', new ConnectionStringParser().KeyValueSeparator);
		}

		[TestMethod]
		public void Constructor_WithNoParameters_ShouldSetStringComparerToOrdinalIgnoreCase()
		{
			Assert.AreEqual(StringComparer.OrdinalIgnoreCase, new ConnectionStringParser().StringComparer);
		}

		[TestMethod]
		public void Constructor_WithNoParameters_ShouldSetTrimToTrue()
		{
			Assert.IsTrue(new ConnectionStringParser().Trim);
		}

		[TestMethod]
		public void GetKeyValuePairStrings_ShouldRemoveEmptyEntries()
		{
			Assert.AreEqual(4, new ConnectionStringParser(DateTime.Now.Second%2 == 0, ';', '=', StringComparer.OrdinalIgnoreCase).GetKeyValuePairStrings(" ;;;; ;;;; ;;;; ").Count());
		}

		private static void ToDictionaryTest(ConnectionStringParser connectionStringParser, string connectionString, IDictionary<string, string> expectedDictionary)
		{
			IDictionary<string, string> actualDictionary = connectionStringParser.ToDictionary(connectionString);
			Assert.AreEqual(expectedDictionary.Count, actualDictionary.Count);
			for(int i = 0; i < expectedDictionary.Count; i++)
			{
				Assert.AreEqual(expectedDictionary.Keys.ElementAt(i), actualDictionary.Keys.ElementAt(i));
				Assert.AreEqual(expectedDictionary.Values.ElementAt(i), actualDictionary.Values.ElementAt(i));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ToDictionary_IfTheStringComparerIsCaseInsensitiveAndTheConnectionStringContainsDuplicateKeys_ShouldThrowAnArgumentException()
		{
			new ConnectionStringParser(DateTime.Now.Second%2 == 0, ';', '=', StringComparer.OrdinalIgnoreCase).ToDictionary("FirstKey=FirstValue;firstkey=SecondValue");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ToDictionary_IfTheStringComparerIsCaseSensitiveAndTheConnectionStringContainsDuplicateKeys_ShouldThrowAnArgumentException()
		{
			new ConnectionStringParser(DateTime.Now.Second%2 == 0, ';', '=', StringComparer.OrdinalIgnoreCase).ToDictionary("FirstKey=FirstValue;FirstKey=SecondValue");
		}

		[TestMethod]
		public void ToDictionary_IfTrimIsFalseAndTheConnectionStringContainsJustAKeyWithSpaces_ShouldReturnADictionaryWithOneItemThatHasAKeyOfSpaces()
		{
			IDictionary<string, string> dictionary = new ConnectionStringParser(false).ToDictionary("  =  ");
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("  "));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ToDictionary_IfTrimIsFalseAndTheConnectionStringContainsJustAnEmptyKey_ShouldThrowAnArgumentException()
		{
			new ConnectionStringParser(false).ToDictionary("=");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ToDictionary_IfTrimIsSetToFalseAndTheConnectionStringOnlyContainsSpaces_ShouldThrowAnArgumentException()
		{
			Assert.AreEqual(0, new ConnectionStringParser(false).ToDictionary("    ").Count);
		}

		[TestMethod]
		public void ToDictionary_IfTrimIsSetToFalseAndTheConnectionStringOnlyContainsSpaces_ShouldThrowAnArgumentExceptionWithAnExplanatoryMessage()
		{
			CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

			string exceptionMessage = string.Empty;

			try
			{
				new ConnectionStringParser(false).ToDictionary("    ");
			}
			catch(ArgumentException argumentException)
			{
				exceptionMessage = argumentException.Message;
			}

			Assert.AreEqual("Each keyvaluepair must contain exactly one separator, '='." + Environment.NewLine + "Parameter name: keyValuePairString", exceptionMessage);
			Thread.CurrentThread.CurrentUICulture = currentUiCulture;
		}

		[TestMethod]
		public void ToDictionary_IfTrimIsSetToTrueAndTheConnectionStringOnlyContainsSpaces_ShouldReturnAnEmptyDictionary()
		{
			Assert.AreEqual(0, new ConnectionStringParser(true).ToDictionary("    ").Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ToDictionary_IfTrimIsTrueAndTheConnectionStringContainsAKeyWithSpaces_ShouldThrowAnArgumentException()
		{
			new ConnectionStringParser(true).ToDictionary("  =  ");
		}

		[TestMethod]
		public void ToDictionary_IfTrimIsTrueAndTheConnectionStringContainsAKeyWithSpaces_ShouldThrowAnArgumentExceptionWithAnExplanatoryMessage()
		{
			CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

			string exceptionMessage = string.Empty;

			try
			{
				new ConnectionStringParser(true).ToDictionary("  =  ");
			}
			catch(ArgumentException argumentException)
			{
				exceptionMessage = argumentException.Message;
			}

			Assert.AreEqual("A key in a keyvaluepair can not be empty." + Environment.NewLine + "Parameter name: keyValuePairString", exceptionMessage);
			Thread.CurrentThread.CurrentUICulture = currentUiCulture;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ToDictionary_IfTrimIsTrueAndTheConnectionStringContainsAnEmptyKey_ShouldThrowAnArgumentException()
		{
			new ConnectionStringParser(true).ToDictionary("=");
		}

		[TestMethod]
		public void ToDictionary_IfTrimIsTrueAndTheConnectionStringContainsAnEmptyKey_ShouldThrowAnArgumentExceptionWithAnExplanatoryMessage()
		{
			CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

			string exceptionMessage = string.Empty;

			try
			{
				new ConnectionStringParser(true).ToDictionary("=");
			}
			catch(ArgumentException argumentException)
			{
				exceptionMessage = argumentException.Message;
			}

			Assert.AreEqual("A key in a keyvaluepair can not be empty." + Environment.NewLine + "Parameter name: keyValuePairString", exceptionMessage);
			Thread.CurrentThread.CurrentUICulture = currentUiCulture;
		}

		[TestMethod]
		public void ToDictionary_Test()
		{
			StringComparer stringComparer = StringComparer.Ordinal;
			Dictionary<string, string> expectedDictionary = new Dictionary<string, string>(stringComparer) {{"FirstKey", "FirstValue"}, {"firstkey", "SecondValue"}};
			ToDictionaryTest(new ConnectionStringParser(false, ';', '=', stringComparer), "FirstKey=FirstValue;firstkey=SecondValue", expectedDictionary);

			stringComparer = StringComparer.Ordinal;
			expectedDictionary = new Dictionary<string, string>(stringComparer) {{"  FirstKey  ", "  FirstValue  "}, {"  firstkey  ", "  SecondValue  "}};
			ToDictionaryTest(new ConnectionStringParser(false, ';', '=', stringComparer), "  FirstKey  =  FirstValue  ;  firstkey  =  SecondValue  ", expectedDictionary);

			stringComparer = StringComparer.OrdinalIgnoreCase;
			expectedDictionary = new Dictionary<string, string>(stringComparer) {{"FirstKey", "FirstValue"}, {"SecondKey", "SecondValue"}, {"ThirdKey", "ThirdValue"}};
			ToDictionaryTest(new ConnectionStringParser(true), "FirstKey=FirstValue;SecondKey=SecondValue;ThirdKey=ThirdValue", expectedDictionary);

			stringComparer = StringComparer.OrdinalIgnoreCase;
			expectedDictionary = new Dictionary<string, string>(stringComparer) {{"FirstKey", "FirstValue"}, {"SecondKey", "SecondValue"}, {"ThirdKey", "ThirdValue"}};
			ToDictionaryTest(new ConnectionStringParser(true), "  FirstKey  =  FirstValue  ;  SecondKey  =  SecondValue  ;  ThirdKey  =  ThirdValue  ", expectedDictionary);
		}

		#endregion
	}
}