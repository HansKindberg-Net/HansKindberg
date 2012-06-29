using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Xml.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Tests.Extensions
{
	[TestClass]
	public class ObjectExtensionTest
	{
		#region Fields

		private static readonly string _testXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + Environment.NewLine +
		                                          "<ObjectExtensionTestSerializableClass xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + Environment.NewLine +
		                                          "\t" + "<PublicField>PublicField</PublicField>" + Environment.NewLine +
		                                          "\t" + "<PublicProperty>PublicField</PublicProperty>" + Environment.NewLine +
		                                          "</ObjectExtensionTestSerializableClass>";

		#endregion

		#region Methods

		[TestMethod]
		public void FromXml_IfTheObjectIsSerializable_ShouldDeserializeFromXml()
		{
			Assert.AreEqual(typeof(ObjectExtensionTestSerializableClass), ObjectExtension.FromXml<ObjectExtensionTestSerializableClass>(_testXml).GetType());
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void FromXml_IfTheStringIsEmpty_ShouldThrowANotSupportedException()
		{
			ObjectExtension.FromXml<object>(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void FromXml_IfTheStringIsNull_ShouldThrowAnArgumentNullException()
		{
			ObjectExtension.FromXml<object>(null);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ToXml_IfTheObjectIsNotSerializable_ShouldThrowANotSupportedException()
		{
			new Dictionary<string, string>().ToXml();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ToXml_IfTheObjectIsNull_ShouldThrowAnArgumentNullException()
		{
			((object) null).ToXml();
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ToXml_IfTheObjectIsSerializableButInternal_ShouldThrowANotSupportedException()
		{
			new ObjectExtensionTestInternalSerializableClass().ToXml();
		}

		[TestMethod]
		public void ToXml_IfTheObjectIsSerializable_ShouldSerializeToXml()
		{
			Assert.AreEqual(_testXml, new ObjectExtensionTestSerializableClass().ToXml());
		}

		#endregion
	}

	[Serializable]
	public class ObjectExtensionTestSerializableClass
	{
		#region Fields

		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected string ProtectedField = "ProtectedField";
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] public string PublicField = "PublicField";
		private string _privateField = "PrivateField";

		#endregion

		#region Properties

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		private string PrivateProperty
		{
			get { return this._privateField; }
			set { this._privateField = value; }
		}

		protected virtual string ProtectedProperty
		{
			get { return this.ProtectedField; }
			set { this.ProtectedField = value; }
		}

		public virtual string PublicProperty
		{
			get { return this.PublicField; }
			set { this.PublicField = value; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private string PrivateMethod()
		{
			return "PrivateMethod";
		}

		protected virtual string ProtectedMethod()
		{
			return "ProtectedMethod";
		}

		public virtual string PublicMethod()
		{
			return "PublicMethod";
		}

		#endregion
	}

	[Serializable]
	internal class ObjectExtensionTestInternalSerializableClass
	{
		#region Fields

		protected string ProtectedField = "ProtectedField";
		public string PublicField = "PublicField";
		private string _privateField = "PrivateField";

		#endregion

		#region Properties

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		private string PrivateProperty
		{
			get { return this._privateField; }
			set { this._privateField = value; }
		}

		protected virtual string ProtectedProperty
		{
			get { return this.ProtectedField; }
			set { this.ProtectedField = value; }
		}

		public virtual string PublicProperty
		{
			get { return this.PublicField; }
			set { this.PublicField = value; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private string PrivateMethod()
		{
			return "PrivateMethod";
		}

		protected virtual string ProtectedMethod()
		{
			return "ProtectedMethod";
		}

		public virtual string PublicMethod()
		{
			return "PublicMethod";
		}

		#endregion
	}
}