using System;
using System.DirectoryServices;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.IntegrationTests
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
	public class GeneralDirectoryTest
	{
		#region Methods

		internal static void AssertDirectoryEntryAndGeneralDirectoryItemAreEqual(DirectoryEntry directoryEntry, IGeneralDirectoryItem generalDirectoryItem)
		{
			Assert.AreEqual(directoryEntry.Path, generalDirectoryItem.Path);

			Assert.AreEqual(directoryEntry.Properties.Count, generalDirectoryItem.Properties.Count);

			for(int i = 0; i < directoryEntry.Properties.Count; i++)
			{
				// ReSharper disable AssignNullToNotNullAttribute
				var propertyName = directoryEntry.Properties.PropertyNames.Cast<string>().ElementAt(i);
				// ReSharper restore AssignNullToNotNullAttribute

				Assert.AreEqual(propertyName, generalDirectoryItem.Properties.Keys.ElementAt(i));

				AssertPropertyValuesAreEqual(propertyName, directoryEntry.Properties[propertyName].Value, generalDirectoryItem.Properties[propertyName]);
			}
		}

		internal static void AssertPropertyValuesAreEqual(string propertyName, object expectedPropertyValue, object actualPropertyValue)
		{
			var expectedPropertyValueAsArray = expectedPropertyValue as Array;

			if(expectedPropertyValueAsArray != null)
			{
				var actualPropertyValueAsArray = actualPropertyValue as Array;

				if(actualPropertyValueAsArray != null)
				{
					Assert.AreEqual(expectedPropertyValueAsArray.Length, actualPropertyValueAsArray.Length, "Property-name: \"{0}\". The length of the arrays are not equal.", new object[] {propertyName});

					for(int i = 0; i < expectedPropertyValueAsArray.Length; i++)
					{
						AssertPropertyValuesAreEqual(propertyName, expectedPropertyValueAsArray.GetValue(i), actualPropertyValueAsArray.GetValue(i));
					}
				}
				else
				{
					Assert.AreEqual(expectedPropertyValueAsArray, actualPropertyValue, "Property-name: \"{0}\". The value should be an array.", new object[] {propertyName});
				}
			}
			else
			{
				Assert.AreEqual(expectedPropertyValue, actualPropertyValue, "Property-name: \"{0}\".", new object[] {propertyName});
			}
		}

		#endregion
	}
}