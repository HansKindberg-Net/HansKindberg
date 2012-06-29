using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Tests
{
	[TestClass]
	public class TypeWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.TypeWrapper")]
		public void Constructor_ShouldThrowAnArgumentNullException_IfTheTypeParameterIsNull()
		{
			new TypeWrapper(null);
		}

		[TestMethod]
		public void FromType_ShouldNotReturnNull_IfTheTypeIsNotNull()
		{
			Assert.IsNotNull(TypeWrapper.FromType(typeof(object)));
		}

		[TestMethod]
		public void FromType_ShouldReturnNull_IfTheTypeIsNull()
		{
			Assert.IsNull(TypeWrapper.FromType(null));
		}

		[TestMethod]
		public void ImplicitOperator_ShouldNotReturnNull_IfTheTypeIsNotNull()
		{
			Assert.IsNotNull((TypeWrapper) typeof(object));
		}

		[TestMethod]
		public void ImplicitOperator_ShouldReturnNull_IfTheTypeIsNull()
		{
			Assert.IsNull((TypeWrapper) (Type) null);
		}

		[TestMethod]
		public void ImplicitOperator_Test()
		{
			Assert.IsNull(null as TypeWrapper);
			Assert.IsNull((object) typeof(object) as TypeWrapper);
		}

		[TestMethod]
		public void TypeIsNotATypeWrapper()
		{
#pragma warning disable 184
			Assert.IsFalse(typeof(object) is TypeWrapper);
#pragma warning restore 184
		}

		#endregion

		//private Dictionary<Type, KeyValuePair<string, string>> _typesToTestDictionary;
		//[TestMethod]
		//public void FriendlyFullName_Test()
		//{
		//    foreach (KeyValuePair<Type, KeyValuePair<string, string>> keyValuePair in this._typesToTestDictionary)
		//    {
		//        Assert.AreEqual(keyValuePair.Value.Value, new TypeWrapper(keyValuePair.Key).FriendlyFullName);
		//    }
		//}
		//[TestMethod]
		//public void FriendlyName_Test()
		//{
		//    foreach (KeyValuePair<Type, KeyValuePair<string, string>> keyValuePair in this._typesToTestDictionary)
		//    {
		//        Assert.AreEqual(keyValuePair.Value.Key, new TypeWrapper(keyValuePair.Key).FriendlyName);
		//    }
		//}
		//[TestMethod]
		//public void GetConstructorWithMostParameters_ShouldConsiderStaticConstructorAsNonpublic()
		//{
		//    Assert.IsNull(typeof(TypeWrapperTestGetConstructorsStaticConstructorTestClass).GetConstructorWithMostParameters(BindingFlags.Public | BindingFlags.Static, false));
		//    Assert.IsNotNull(typeof(TypeWrapperTestGetConstructorsStaticConstructorTestClass).GetConstructorWithMostParameters(BindingFlags.NonPublic | BindingFlags.Static, false));
		//}
		//[TestMethod]
		//public void GetConstructorWithMostParameters_ShouldExcludeNonpublicConstructor_ByDefault()
		//{
		//    Assert.IsNull(typeof(TypeExtensionTestGetConstructorsNoPublicConstructorsTestClass).GetConstructorWithMostParameters(false));
		//}
		//[TestMethod]
		//public void GetConstructorWithMostParameters_ShouldExcludeParameterlessConstructor_IfExcludeParameterlessConstructorIsSetToTrue()
		//{
		//    Assert.IsNull(typeof(TypeExtensionTestGetConstructorsParameterlessConstructorsTestClass).GetConstructorWithMostParameters(true));
		//}
		//[TestMethod]
		//public void GetConstructorWithMostParameters_ShouldExcludeStaticConstructor_ByDefault()
		//{
		//    Assert.IsNull(typeof(TypeExtensionTestGetConstructorsStaticConstructorTestClass).GetConstructorWithMostParameters(false));
		//}
		//[TestMethod]
		//public void GetConstructorWithMostParameters_ShouldIncludeParameterlessConstructor_IfExcludeParameterlessConstructorIsSetToFalse()
		//{
		//    Assert.IsNotNull(typeof(TypeExtensionTestGetConstructorsParameterlessConstructorsTestClass).GetConstructorWithMostParameters(false));
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsParameterlessConstructorsTestClass).GetConstructorWithMostParameters(false).GetParameters().Length);
		//}
		//[TestMethod]
		//public void GetConstructorWithMostParameters_ShouldReturnNull_IfNoConstructorFound()
		//{
		//    Assert.IsNull(typeof(TypeExtensionTestGetConstructorsNoPublicConstructorsTestClass).GetConstructorWithMostParameters(false));
		//}
		//[TestMethod]
		//public void GetConstructorWithMostParameters_ShouldReturnTheCorrectConstructor()
		//{
		//    Assert.AreEqual(4, typeof(TypeExtensionTestGetConstructorsTestClass).GetConstructorWithMostParameters(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, false).GetParameters().Length);
		//}
		//[TestMethod]
		//public void GetConstructorsSortedByMostParametersFirst_ShouldConsiderStaticConstructorsAsNonpublic()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsStaticConstructorTestClass).GetConstructorsSortedByMostParametersFirst(BindingFlags.Public | BindingFlags.Static, false).Length);
		//    Assert.AreEqual(1, typeof(TypeExtensionTestGetConstructorsStaticConstructorTestClass).GetConstructorsSortedByMostParametersFirst(BindingFlags.NonPublic | BindingFlags.Static, false).Length);
		//}
		//[TestMethod]
		//public void GetConstructorsSortedByMostParametersFirst_ShouldExcludeNonpublicConstructors_ByDefault()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsNoPublicConstructorsTestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		//}
		//[TestMethod]
		//public void GetConstructorsSortedByMostParametersFirst_ShouldExcludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToTrue()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsParameterlessConstructorsTestClass).GetConstructorsSortedByMostParametersFirst(true).Length);
		//}
		//[TestMethod]
		//public void GetConstructorsSortedByMostParametersFirst_ShouldExcludeStaticConstructors_ByDefault()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsStaticConstructorTestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		//}
		//[TestMethod]
		//public void GetConstructorsSortedByMostParametersFirst_ShouldIncludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToFalse()
		//{
		//    Assert.AreEqual(1, typeof(TypeExtensionTestGetConstructorsParameterlessConstructorsTestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		//}
		//[TestMethod]
		//public void GetConstructorsSortedByMostParametersFirst_ShouldReturnAnEmptyArray_IfNoConstructorsFound()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsNoPublicConstructorsTestClass).GetConstructorsSortedByMostParametersFirst(false).Length);
		//}
		//[TestMethod]
		//public void GetConstructorsSortedByMostParametersFirst_ShouldSortCorrectly()
		//{
		//    ConstructorInfo[] constructors = typeof(TypeExtensionTestGetConstructorsTestClass).GetConstructorsSortedByMostParametersFirst(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, false);
		//    Assert.AreEqual(4, constructors[0].GetParameters().Length);
		//    Assert.AreEqual(4, constructors[1].GetParameters().Length);
		//    Assert.AreEqual(3, constructors[2].GetParameters().Length);
		//    Assert.AreEqual(3, constructors[3].GetParameters().Length);
		//    Assert.AreEqual(2, constructors[4].GetParameters().Length);
		//    Assert.AreEqual(2, constructors[5].GetParameters().Length);
		//    Assert.AreEqual(1, constructors[6].GetParameters().Length);
		//    Assert.AreEqual(1, constructors[7].GetParameters().Length);
		//    Assert.AreEqual(0, constructors[8].GetParameters().Length);
		//    Assert.AreEqual(0, constructors[9].GetParameters().Length);
		//}
		//[TestMethod]
		//public void GetConstructors_ShouldConsiderStaticConstructorsAsNonpublic()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsStaticConstructorTestClass).GetConstructors(BindingFlags.Public | BindingFlags.Static, false).Length);
		//    Assert.AreEqual(1, typeof(TypeExtensionTestGetConstructorsStaticConstructorTestClass).GetConstructors(BindingFlags.NonPublic | BindingFlags.Static, false).Length);
		//}
		//[TestMethod]
		//public void GetConstructors_ShouldExcludeNonpublicConstructors_ByDefault()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsNoPublicConstructorsTestClass).GetConstructors(false).Length);
		//}
		//[TestMethod]
		//public void GetConstructors_ShouldExcludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToTrue()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsParameterlessConstructorsTestClass).GetConstructors(true).Length);
		//}
		//[TestMethod]
		//public void GetConstructors_ShouldExcludeStaticConstructors_ByDefault()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsStaticConstructorTestClass).GetConstructors(false).Length);
		//}
		//[TestMethod]
		//public void GetConstructors_ShouldIncludeParameterlessConstructors_IfExcludeParameterlessConstructorIsSetToFalse()
		//{
		//    Assert.AreEqual(1, typeof(TypeExtensionTestGetConstructorsParameterlessConstructorsTestClass).GetConstructors(false).Length);
		//}
		//[TestMethod]
		//public void GetConstructors_ShouldReturnAnEmptyArray_IfNoConstructorsFound()
		//{
		//    Assert.AreEqual(0, typeof(TypeExtensionTestGetConstructorsNoPublicConstructorsTestClass).GetConstructors(false).Length);
		//}
		//[TestMethod]
		//[ExpectedException(typeof(ArgumentNullException))]
		//public void GetConstructors_ShouldThrowAnArgumentNullException_IfTheTypeIsNull()
		//{
		//    ((Type)null).GetConstructors(BindingFlags.Instance | BindingFlags.Public, false);
		//}
		//[TestInitialize]
		//public void Initialize()
		//{
		//    this._typesToTestDictionary = new Dictionary<Type, KeyValuePair<string, string>>();
		//    this._typesToTestDictionary.Add(typeof(List<string>), new KeyValuePair<string, string>("List<String>", "System.Collections.Generic.List<System.String>"));
		//    this._typesToTestDictionary.Add(typeof(List<List<List<string>>>), new KeyValuePair<string, string>("List<List<List<String>>>", "System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<System.String>>>"));
		//    this._typesToTestDictionary.Add(typeof(Dictionary<int, List<int>>), new KeyValuePair<string, string>("Dictionary<Int32, List<Int32>>", "System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.Int32>>"));
		//}
	}

	//[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Scope = "type")]
	//internal class TypeWrapperTestGetConstructorsTestClass
	//{
	//    #region Fields
	//    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
	//    private readonly object _firstParameter;
	//    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
	//    private readonly object _fourthParameter;
	//    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
	//    private readonly object _secondParameter;
	//    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
	//    private readonly object _thirdParameter;
	//    #endregion
	//    #region Constructors
	//    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
	//    static TypeWrapperTestGetConstructorsTestClass() { }
	//    public TypeWrapperTestGetConstructorsTestClass() { }
	//    public TypeWrapperTestGetConstructorsTestClass(object firstParameter)
	//    {
	//        this._firstParameter = firstParameter;
	//    }
	//    public TypeWrapperTestGetConstructorsTestClass(string firstParameter)
	//    {
	//        this._firstParameter = firstParameter;
	//    }
	//    public TypeWrapperTestGetConstructorsTestClass(object firstParameter, object secondParameter)
	//        : this(firstParameter)
	//    {
	//        this._secondParameter = secondParameter;
	//    }
	//    public TypeWrapperTestGetConstructorsTestClass(string firstParameter, string secondParameter)
	//        : this(firstParameter)
	//    {
	//        this._secondParameter = secondParameter;
	//    }
	//    public TypeWrapperTestGetConstructorsTestClass(object firstParameter, object secondParameter, object thirdParameter)
	//        : this(firstParameter, secondParameter)
	//    {
	//        this._thirdParameter = thirdParameter;
	//    }
	//    public TypeWrapperTestGetConstructorsTestClass(string firstParameter, string secondParameter, string thirdParameter)
	//        : this(firstParameter, secondParameter)
	//    {
	//        this._thirdParameter = thirdParameter;
	//    }
	//    private TypeWrapperTestGetConstructorsTestClass(object firstParameter, object secondParameter, object thirdParameter, object fourthParameter)
	//        : this(firstParameter, secondParameter, thirdParameter)
	//    {
	//        this._fourthParameter = fourthParameter;
	//    }
	//    protected TypeWrapperTestGetConstructorsTestClass(string firstParameter, string secondParameter, string thirdParameter, string fourthParameter)
	//        : this(firstParameter, secondParameter, thirdParameter)
	//    {
	//        this._fourthParameter = fourthParameter;
	//    }
	//    #endregion
	//}
	//internal class TypeWrapperTestGetConstructorsNoPublicConstructorsTestClass
	//{
	//    #region Constructors
	//    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
	//    static TypeWrapperTestGetConstructorsNoPublicConstructorsTestClass() { }
	//    private TypeWrapperTestGetConstructorsNoPublicConstructorsTestClass() { }
	//    #endregion
	//}
	//internal class TypeWrapperTestGetConstructorsParameterlessConstructorsTestClass
	//{
	//    #region Constructors
	//    public TypeWrapperTestGetConstructorsParameterlessConstructorsTestClass() { }
	//    #endregion
	//}
	//internal class TypeWrapperTestGetConstructorsStaticConstructorTestClass
	//{
	//    #region Constructors
	//    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
	//    static TypeWrapperTestGetConstructorsStaticConstructorTestClass() { }
	//    // To hide the public constructor.
	//    private TypeWrapperTestGetConstructorsStaticConstructorTestClass() { }
	//    #endregion
	//}
}