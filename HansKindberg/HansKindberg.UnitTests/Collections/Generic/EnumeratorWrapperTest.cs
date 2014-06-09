using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HansKindberg.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.UnitTests.Collections.Generic
{
	[TestClass]
	public class EnumeratorWrapperTest
	{
		#region Methods

		[TestMethod]
		public void IterationTest()
		{
			var testValues = new[] {1, 2, 3};

			var enumerator = new List<int>(testValues.ToArray()).GetEnumerator();

			using(var enumeratorWrapper = new EnumeratorWrapper<string>(enumerator, value => value.ToString()))
			{
				foreach(var testValue in testValues)
				{
					enumeratorWrapper.MoveNext();

					Assert.AreEqual(testValue.ToString(CultureInfo.InvariantCulture), enumeratorWrapper.Current);
				}
			}
		}

		#endregion
	}
}