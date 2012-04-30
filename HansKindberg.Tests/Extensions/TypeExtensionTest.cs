using System;
using System.Collections.Generic;
using System.Reflection;
using HansKindberg.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Tests.Extensions
{
	[TestClass]
	public class TypeExtensionTest
	{
		#region Fields

		private Dictionary<Type, KeyValuePair<string, string>> _typesToTestDictionary;

		#endregion

		#region Methods

		private ParameterInfo CreateParameterInfo()
		{
			return (ParameterInfo) Activator.CreateInstance(typeof(ParameterInfo), true);
		}

		private ParameterInfo[] CreateParameterInfoArray(byte length)
		{
			ParameterInfo[] parameterInfoArray = new ParameterInfo[length];

			for(int i = 0; i < length; i++)
			{
				parameterInfoArray[i] = this.CreateParameterInfo();
			}

			return parameterInfoArray;
		}

		[TestMethod]
		public void FriendlyFullName_Test()
		{
			foreach(KeyValuePair<Type, KeyValuePair<string, string>> keyValuePair in this._typesToTestDictionary)
			{
				Assert.AreEqual(keyValuePair.Value.Value, keyValuePair.Key.FriendlyFullName());
			}
		}

		[TestMethod]
		public void FriendlyName_Test()
		{
			foreach (KeyValuePair<Type, KeyValuePair<string, string>> keyValuePair in this._typesToTestDictionary)
			{
				Assert.AreEqual(keyValuePair.Value.Key, keyValuePair.Key.FriendlyName());
			}
		}

		[TestMethod]
		public void GetConstructorWithMostParameters_ShouldConsiderStaticConstructorAsNonPublic()
		{
			Assert.IsNull(typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructorWithMostParameters(BindingFlags.Public | BindingFlags.Static, false));
			Assert.IsNotNull(typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructorWithMostParameters(BindingFlags.NonPublic | BindingFlags.Static, false));
		}

		[TestMethod]
		public void GetConstructorWithMostParameters_ShouldExcludeNonPublicConstructor_ByDefault()
		{
			Assert.IsNull(typeof(TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass).GetConstructorWithMostParameters(false));
		}

		[TestMethod]
		public void GetConstructorWithMostParameters_ShouldExcludeParameterlessConstructor_IfExcludeParameterlessConstructorIsSetToTrue()
		{
			Assert.IsNull(typeof(TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass).GetConstructorWithMostParameters(true));
		}

		[TestMethod]
		public void GetConstructorWithMostParameters_ShouldExcludeStaticConstructor_ByDefault()
		{
			Assert.IsNull(typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructorWithMostParameters(false));
		}

		[TestMethod]
		public void GetConstructorWithMostParameters_ShouldIncludeParameterlessConstructor_IfExcludeParameterlessConstructorIsSetToFalse()
		{
			Assert.IsNotNull(typeof(TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass).GetConstructorWithMostParameters(false));
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass).GetConstructorWithMostParameters(false).GetParameters().Length);
		}

		[TestMethod]
		public void GetConstructorWithMostParameters_ShouldReturnNull_IfNoConstructorFound()
		{
			Assert.IsNull(typeof(TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass).GetConstructorWithMostParameters(false));
		}

		[TestMethod]
		public void GetConstructorWithMostParameters_ShouldReturnTheCorrectConstructor()
		{
			Assert.AreEqual(4, typeof(TypeExtensionTest_GetConstructors_TestClass).GetConstructorWithMostParameters(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, false).GetParameters().Length);
		}

		[TestMethod]
		public void GetConstructorsSortedByMostParametersFirst_ShouldConsiderStaticConstructorsAsNonPublic()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructorsSortedByMostParametersFirst(BindingFlags.Public | BindingFlags.Static, false).Length);
			Assert.AreEqual(1, typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructorsSortedByMostParametersFirst(BindingFlags.NonPublic | BindingFlags.Static, false).Length);
		}

		[TestMethod]
		public void GetConstructorsSortedByMostParametersFirst_ShouldExcludeNonPublicConstructors_ByDefault()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		}

		[TestMethod]
		public void GetConstructorsSortedByMostParametersFirst_ShouldExcludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToTrue()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass).GetConstructorsSortedByMostParametersFirst(true).Length);
		}

		[TestMethod]
		public void GetConstructorsSortedByMostParametersFirst_ShouldExcludeStaticConstructors_ByDefault()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		}

		[TestMethod]
		public void GetConstructorsSortedByMostParametersFirst_ShouldIncludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToFalse()
		{
			Assert.AreEqual(1, typeof(TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		}

		[TestMethod]
		public void GetConstructorsSortedByMostParametersFirst_ShouldReturnAnEmptyArray_IfNoConstructorsFound()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		}

		[TestMethod]
		public void GetConstructorsSortedByMostParametersFirst_ShouldSortCorrectly()
		{
			ConstructorInfo[] constructors = typeof(TypeExtensionTest_GetConstructors_TestClass).GetConstructorsSortedByMostParametersFirst(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, false);

			Assert.AreEqual(4, constructors[0].GetParameters().Length);
			Assert.AreEqual(4, constructors[1].GetParameters().Length);
			Assert.AreEqual(3, constructors[2].GetParameters().Length);
			Assert.AreEqual(3, constructors[3].GetParameters().Length);
			Assert.AreEqual(2, constructors[4].GetParameters().Length);
			Assert.AreEqual(2, constructors[5].GetParameters().Length);
			Assert.AreEqual(1, constructors[6].GetParameters().Length);
			Assert.AreEqual(1, constructors[7].GetParameters().Length);
			Assert.AreEqual(0, constructors[8].GetParameters().Length);
			Assert.AreEqual(0, constructors[9].GetParameters().Length);
		}

		[TestMethod]
		public void GetConstructors_ShouldConsiderStaticConstructorsAsNonPublic()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructors(BindingFlags.Public | BindingFlags.Static, false).Length);
			Assert.AreEqual(1, typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructors(BindingFlags.NonPublic | BindingFlags.Static, false).Length);
		}

		[TestMethod]
		public void GetConstructors_ShouldExcludeNonPublicConstructors_ByDefault()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass).GetConstructors(false).Length);
		}

		[TestMethod]
		public void GetConstructors_ShouldExcludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToTrue()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass).GetConstructors(true).Length);
		}

		[TestMethod]
		public void GetConstructors_ShouldExcludeStaticConstructors_ByDefault()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_StaticConstructor_TestClass).GetConstructors(false).Length);
		}

		[TestMethod]
		public void GetConstructors_ShouldIncludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToFalse()
		{
			Assert.AreEqual(1, typeof(TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass).GetConstructors(false).Length);
		}

		[TestMethod]
		public void GetConstructors_ShouldReturnAnEmptyArray_IfNoConstructorsFound()
		{
			Assert.AreEqual(0, typeof(TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass).GetConstructors(false).Length);
		}

		[TestInitialize]
		public void Initialize()
		{
			this._typesToTestDictionary = new Dictionary<Type, KeyValuePair<string, string>>();
			this._typesToTestDictionary.Add(typeof(List<string>), new KeyValuePair<string, string>("List<String>", "System.Collections.Generic.List<System.String>"));
			this._typesToTestDictionary.Add(typeof(List<List<List<string>>>), new KeyValuePair<string, string>("List<List<List<String>>>", "System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<System.String>>>"));
			this._typesToTestDictionary.Add(typeof(Dictionary<int, List<int>>), new KeyValuePair<string, string>("Dictionary<Int32, List<Int32>>", "System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.Int32>>"));
		}

		#endregion
	}

	internal class TypeExtensionTest_GetConstructors_TestClass
	{
		#region Constructors

		static TypeExtensionTest_GetConstructors_TestClass() {}
		public TypeExtensionTest_GetConstructors_TestClass() {}
		public TypeExtensionTest_GetConstructors_TestClass(object firstParameter) {}
		public TypeExtensionTest_GetConstructors_TestClass(string firstParameter) {}
		public TypeExtensionTest_GetConstructors_TestClass(object firstParameter, object secondParameter) {}
		public TypeExtensionTest_GetConstructors_TestClass(string firstParameter, string secondParameter) {}
		public TypeExtensionTest_GetConstructors_TestClass(object firstParameter, object secondParameter, object thirdParameter) {}
		public TypeExtensionTest_GetConstructors_TestClass(string firstParameter, string secondParameter, string thirdParameter) {}
		private TypeExtensionTest_GetConstructors_TestClass(object firstParameter, object secondParameter, object thirdParameter, object fourthParameter) {}
		protected TypeExtensionTest_GetConstructors_TestClass(string firstParameter, string secondParameter, string thirdParameter, string fourthParameter) {}

		#endregion
	}

	internal class TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass
	{
		#region Constructors

		static TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass() {}
		private TypeExtensionTest_GetConstructors_NoPublicConstructors_TestClass() {}

		#endregion
	}

	internal class TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass
	{
		#region Constructors

		public TypeExtensionTest_GetConstructors_ParameterlessConstructors_TestClass() {}

		#endregion
	}

	internal class TypeExtensionTest_GetConstructors_StaticConstructor_TestClass
	{
		#region Constructors

		static TypeExtensionTest_GetConstructors_StaticConstructor_TestClass() {}
		private TypeExtensionTest_GetConstructors_StaticConstructor_TestClass() {}

		#endregion

		// To hide the public constructor.
	}
}