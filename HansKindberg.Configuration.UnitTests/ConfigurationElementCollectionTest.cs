using System;
using System.Collections.Generic;
using System.Configuration;
using HansKindberg.Configuration.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Configuration.UnitTests
{
	[TestClass]
	public class ConfigurationElementCollectionTest
	{
		#region Methods

		[TestMethod]
		public void Add_ShouldAddTheItemToTheCollection()
		{
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock();
			Assert.IsTrue(configurationElementCollection.Count == 0);
			configurationElementCollection.Add(new ConfigurationElementMock());
			Assert.IsTrue(configurationElementCollection.Count == 1);
		}

		[TestMethod]
		public void Clear_ShouldClearTheCollection()
		{
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock
				{
					new ConfigurationElementMock {Name = "1"},
					new ConfigurationElementMock {Name = "2"},
					new ConfigurationElementMock {Name = "3"}
				};
			Assert.AreEqual(3, configurationElementCollection.Count);
			configurationElementCollection.Clear();
			Assert.AreEqual(0, configurationElementCollection.Count);
		}

		[TestMethod]
		public void Contains_IfTheCollectionContainsTheItem_ShouldReturnTrue()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementMock();
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock
				{
					configurationElement
				};
			Assert.IsTrue(configurationElementCollection.Contains(configurationElement));
		}

		[TestMethod]
		public void Contains_IfTheCollectionDoesNotContainTheItem_ShouldReturnFalse()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementMock();
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock();
			Assert.IsFalse(configurationElementCollection.Contains(configurationElement));
		}

		[TestMethod]
		public void CopyTo_ShouldCopyTheCollectionToTheArrayParameter()
		{
			ConfigurationElementMock firstConfigurationElement = new ConfigurationElementMock {Name = "1"};
			ConfigurationElementMock secondConfigurationElement = new ConfigurationElementMock {Name = "2"};
			ConfigurationElementMock thirdConfigurationElement = new ConfigurationElementMock {Name = "3"};

			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock
				{
					firstConfigurationElement,
					secondConfigurationElement,
					thirdConfigurationElement
				};

			ConfigurationElementMock[] array = new ConfigurationElementMock[10];
			configurationElementCollection.CopyTo(array, 0);
			Assert.AreEqual(firstConfigurationElement, array[0]);
			Assert.AreEqual(secondConfigurationElement, array[1]);
			Assert.AreEqual(thirdConfigurationElement, array[2]);
			for(int i = 3; i < 10; i++)
			{
				Assert.IsNull(array[i]);
			}

			array = new ConfigurationElementMock[10];
			configurationElementCollection.CopyTo(array, 7);
			Assert.AreEqual(firstConfigurationElement, array[7]);
			Assert.AreEqual(secondConfigurationElement, array[8]);
			Assert.AreEqual(thirdConfigurationElement, array[9]);
			for(int i = 0; i < 7; i++)
			{
				Assert.IsNull(array[i]);
			}
		}

		[TestMethod]
		public void CreateNewElement_ShouldReturnAnObjectOfTheGenericType()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementCollectionMock().CreateNewElement() as ConfigurationElementMock;
			Assert.IsNotNull(configurationElement);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidCastException))]
		public void GetEnumerator_IfTheConfigurationElementCollectionContainsItemsThatIsNotOfTheGenericType_ShouldThrowAnInvalidCastException()
		{
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock();
			configurationElementCollection.BaseAdd(new ConfigurationElementMock());
			configurationElementCollection.BaseAdd(Mock.Of<ConfigurationElement>());
			configurationElementCollection.BaseAdd(new ConfigurationElementMock());

			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			// ReSharper disable IteratorMethodResultIsIgnored
			configurationElementCollection.GetEnumerator();
			// ReSharper restore IteratorMethodResultIsIgnored
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		public void GetEnumerator_ShouldReturnAGenericEnumerator()
		{
			List<ConfigurationElementMock> list = new List<ConfigurationElementMock>
				{
					new ConfigurationElementMock(),
					new ConfigurationElementMock(),
					new ConfigurationElementMock()
				};

			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock();

			foreach(ConfigurationElementMock configurationElement in list)
			{
				configurationElementCollection.BaseAdd(configurationElement);
			}

			IEnumerator<ConfigurationElementMock> listEnumerator = list.GetEnumerator();
			IEnumerator<ConfigurationElementMock> enumerator = configurationElementCollection.GetEnumerator();

			while(listEnumerator.MoveNext())
			{
				enumerator.MoveNext();
				Assert.AreEqual(listEnumerator.Current, enumerator.Current);
			}

			Assert.IsFalse(listEnumerator.MoveNext());
			Assert.IsFalse(enumerator.MoveNext());
		}

		[TestMethod]
		public void IndexOf_ShouldReturnTheIndexOfTheItem()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementMock {Name = "1"};

			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock
				{
					configurationElement
				};

			Assert.AreEqual(0, configurationElementCollection.IndexOf(configurationElement));
			Assert.AreEqual(-1, configurationElementCollection.IndexOf(new ConfigurationElementMock {Name = "2"}));
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Insert_IfTheIndexIsOutOfRange_ShouldThrowAConfigurationErrorsException()
		{
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock();
			configurationElementCollection.Insert(10, new ConfigurationElementMock());
		}

		[TestMethod]
		public void Insert_ShouldInsertTheItemAtTheGivenIndex()
		{
			ConfigurationElementMock firstConfigurationElement = new ConfigurationElementMock {Name = "1"};
			ConfigurationElementMock secondConfigurationElement = new ConfigurationElementMock {Name = "2"};
			ConfigurationElementMock thirdConfigurationElement = new ConfigurationElementMock {Name = "3"};

			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock
				{
					firstConfigurationElement,
					secondConfigurationElement,
					thirdConfigurationElement
				};

			Assert.AreEqual(firstConfigurationElement, configurationElementCollection[0]);
			Assert.AreEqual(secondConfigurationElement, configurationElementCollection[1]);
			Assert.AreEqual(thirdConfigurationElement, configurationElementCollection[2]);

			configurationElementCollection.Clear();

			configurationElementCollection.Insert(0, firstConfigurationElement);
			configurationElementCollection.Insert(0, secondConfigurationElement);
			configurationElementCollection.Insert(0, thirdConfigurationElement);

			Assert.AreEqual(thirdConfigurationElement, configurationElementCollection[0]);
			Assert.AreEqual(secondConfigurationElement, configurationElementCollection[1]);
			Assert.AreEqual(firstConfigurationElement, configurationElementCollection[2]);
		}

		[TestMethod]
		public void IsReadOnly_IfTheCollectionIsNotReadOnly_ShouldReturnFalse()
		{
			Assert.IsFalse(new ConfigurationElementCollectionMock().IsReadOnly);
		}

		[TestMethod]
		public void IsReadOnly_IfTheCollectionIsReadOnly_ShouldReturnTrue()
		{
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock();
			configurationElementCollection.SetReadOnly();
			Assert.IsTrue(configurationElementCollection.IsReadOnly);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void RemoveAt_IfTheIndexIsOutOfRange_ShouldThrowAConfigurationErrorsException()
		{
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock();
			configurationElementCollection.RemoveAt(0);
		}

		[TestMethod]
		public void RemoveAt_ShouldRemoveTheItemAtTheGivenIndex()
		{
			ConfigurationElementMock firstConfigurationElement = new ConfigurationElementMock {Name = "1"};
			ConfigurationElementMock secondConfigurationElement = new ConfigurationElementMock {Name = "2"};
			ConfigurationElementMock thirdConfigurationElement = new ConfigurationElementMock {Name = "3"};

			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock
				{
					firstConfigurationElement,
					secondConfigurationElement,
					thirdConfigurationElement
				};

			Assert.AreEqual(3, configurationElementCollection.Count);

			configurationElementCollection.RemoveAt(1);

			Assert.AreEqual(2, configurationElementCollection.Count);
			Assert.AreEqual(firstConfigurationElement, configurationElementCollection[0]);
			Assert.AreEqual(thirdConfigurationElement, configurationElementCollection[1]);

			configurationElementCollection.RemoveAt(0);

			Assert.AreEqual(1, configurationElementCollection.Count);
			Assert.AreEqual(thirdConfigurationElement, configurationElementCollection[0]);

			configurationElementCollection.RemoveAt(0);

			Assert.AreEqual(0, configurationElementCollection.Count);
		}

		[TestMethod]
		public void Remove_IfTheItemDoesNotExist_ShouldReturnFalse()
		{
			ConfigurationElementMock firstConfigurationElement = new ConfigurationElementMock {Name = "1"};

			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock {firstConfigurationElement};

			ConfigurationElementMock secondConfigurationElement = new ConfigurationElementMock {Name = "2"};

			Assert.AreEqual(1, configurationElementCollection.Count);
			Assert.IsFalse(configurationElementCollection.Remove(secondConfigurationElement));
			Assert.AreEqual(1, configurationElementCollection.Count);
		}

		[TestMethod]
		public void Remove_IfTheItemExists_ShouldRemoveTheItemFromTheCollectionAndReturnTrue()
		{
			ConfigurationElementMock firstConfigurationElement = new ConfigurationElementMock {Name = "1"};

			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock {firstConfigurationElement};

			Assert.IsTrue(configurationElementCollection.Remove(firstConfigurationElement));
			Assert.AreEqual(0, configurationElementCollection.Count);

			configurationElementCollection.Add(firstConfigurationElement);
			Assert.AreEqual(1, configurationElementCollection.Count);

			ConfigurationElementMock secondConfigurationElement = new ConfigurationElementMock {Name = "1"};

			Assert.IsTrue(configurationElementCollection.Remove(secondConfigurationElement));
			Assert.AreEqual(0, configurationElementCollection.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void This_Get_IfTheIndexIsOutOfRange_ShouldThrowAConfigurationErrorsException()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementMock();
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock {configurationElement};
			configurationElement = configurationElementCollection[1];
		}

		[TestMethod]
		public void This_Get_ShouldReturnTheItemAtTheGivenIndex()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementMock();
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock {configurationElement};
			Assert.AreEqual(configurationElement, configurationElementCollection[0]);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void This_Set_IfTheIndexIsOutOfRange_ShouldThrowAConfigurationErrorsException()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementMock();
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock {configurationElement};
			configurationElementCollection[1] = configurationElement;
		}

		[TestMethod]
		public void This_Set_ShouldSetTheItemAtTheGivenIndex()
		{
			ConfigurationElementMock configurationElement = new ConfigurationElementMock();
			ConfigurationElementCollectionMock configurationElementCollection = new ConfigurationElementCollectionMock {configurationElement};
			Assert.AreEqual(configurationElement, configurationElementCollection[0]);
			ConfigurationElementMock anotherConfigurationElement = new ConfigurationElementMock();
			configurationElementCollection[0] = anotherConfigurationElement;
			Assert.AreEqual(anotherConfigurationElement, configurationElementCollection[0]);
			Assert.AreEqual(1, configurationElementCollection.Count);
		}

		#endregion
	}
}