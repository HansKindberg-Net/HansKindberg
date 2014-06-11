﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DistinguishedNameParserTest
	{
		#region Methods

		[TestMethod]
		public void Parse_ShouldHandleEscapedCommaCharacters()
		{
			var distinguishedName = new DistinguishedNameParser().Parse("AA=FirstValue,BB=SecondValue \\, and something else,CC=ThirdValue");

			Assert.AreEqual(3, distinguishedName.Components.Count);
			Assert.AreEqual("AA", distinguishedName.Components[0].Name);
			Assert.AreEqual("FirstValue", distinguishedName.Components[0].Value);
			Assert.AreEqual("BB", distinguishedName.Components[1].Name);
			Assert.AreEqual("SecondValue \\, and something else", distinguishedName.Components[1].Value);
			Assert.AreEqual("CC", distinguishedName.Components[2].Name);
			Assert.AreEqual("ThirdValue", distinguishedName.Components[2].Value);
		}

		[TestMethod]
		public void Split_Test()
		{
			var distinguishedNameParser = new DistinguishedNameParser();

			var parts = distinguishedNameParser.Split(@"A\,B,C\,D,E,F,G,H,I", ',', 3).ToArray();

			Assert.AreEqual(3, parts.Length);
			Assert.AreEqual(@"A\,B", parts[0]);
			Assert.AreEqual(@"C\,D", parts[1]);
			Assert.AreEqual("E,F,G,H,I", parts[2]);

			parts = distinguishedNameParser.Split("A\\,B,C\\,D,E,F,G,H,I", ',', 3).ToArray();

			Assert.AreEqual(3, parts.Length);
			Assert.AreEqual("A\\,B", parts[0]);
			Assert.AreEqual("C\\,D", parts[1]);
			Assert.AreEqual("E,F,G,H,I", parts[2]);

			parts = distinguishedNameParser.Split(@"A\,B,C\,D,E,F,G,H,I", ',', 4).ToArray();

			Assert.AreEqual(4, parts.Length);
			Assert.AreEqual(@"A\,B", parts[0]);
			Assert.AreEqual(@"C\,D", parts[1]);
			Assert.AreEqual("E", parts[2]);
			Assert.AreEqual("F,G,H,I", parts[3]);

			parts = distinguishedNameParser.Split("A\\,B,C\\,D,E,F,G,H,I", ',', 4).ToArray();

			Assert.AreEqual(4, parts.Length);
			Assert.AreEqual("A\\,B", parts[0]);
			Assert.AreEqual("C\\,D", parts[1]);
			Assert.AreEqual("E", parts[2]);
			Assert.AreEqual("F,G,H,I", parts[3]);
		}

		#endregion
	}
}