using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HansKindberg.Extensions;
using HansKindberg.Reflection;

namespace HansKindberg
{
	[Obsolete("Reminder: Maybe this class is \"overkill\". Maybe we can handle it with extensions instead, because both Type and ConstructorInfo are abstract classes that should be able to mock. So maybe we should try to remove this bit by bit.")]
	public class TypeWrapper : IType
	{
		#region Fields

		private readonly Type _type;

		#endregion

		#region Constructors

		public TypeWrapper(Type type)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			this._type = type;
		}

		#endregion

		#region Properties

		public virtual string FriendlyFullName
		{
			get { return this._type.FriendlyFullName(); }
		}

		public virtual string FriendlyName
		{
			get { return this._type.FriendlyName(); }
		}

		public virtual string FullName
		{
			get { return this._type.FullName; }
		}

		public virtual bool IsGenericType
		{
			get { return this._type.IsGenericType; }
		}

		public virtual string Name
		{
			get { return this._type.Name; }
		}

		#endregion

		#region Methods

		public static TypeWrapper FromType(Type type)
		{
			return type;
		}

		public virtual IConstructorInfo GetConstructorWithMostParameters(bool excludeParameterlessConstructor)
		{
			return (ConstructorInfoWrapper) this._type.GetConstructorWithMostParameters(excludeParameterlessConstructor);
		}

		public virtual IConstructorInfo GetConstructorWithMostParameters(BindingFlags bindings, bool excludeParameterlessConstructor)
		{
			return (ConstructorInfoWrapper) this._type.GetConstructorWithMostParameters(bindings, excludeParameterlessConstructor);
		}

		public virtual IEnumerable<IConstructorInfo> GetConstructors(bool excludeParameterlessConstructors)
		{
			return this._type.GetConstructors(excludeParameterlessConstructors).Select(constructorInfo => (ConstructorInfoWrapper) constructorInfo).Cast<IConstructorInfo>().ToList();
		}

		public virtual IEnumerable<IConstructorInfo> GetConstructors(BindingFlags bindings, bool excludeParameterlessConstructors)
		{
			return this._type.GetConstructors(bindings, excludeParameterlessConstructors).Select(constructorInfo => (ConstructorInfoWrapper) constructorInfo).Cast<IConstructorInfo>().ToList();
		}

		public virtual IEnumerable<IConstructorInfo> GetConstructorsSortedByMostParametersFirst(bool excludeParameterlessConstructors)
		{
			return this._type.GetConstructorsSortedByMostParametersFirst(excludeParameterlessConstructors).Select(constructorInfo => (ConstructorInfoWrapper) constructorInfo).Cast<IConstructorInfo>().ToList();
		}

		public virtual IEnumerable<IConstructorInfo> GetConstructorsSortedByMostParametersFirst(BindingFlags bindings, bool excludeParameterlessConstructors)
		{
			return this._type.GetConstructorsSortedByMostParametersFirst(bindings, excludeParameterlessConstructors).Select(constructorInfo => (ConstructorInfoWrapper) constructorInfo).Cast<IConstructorInfo>().ToList();
		}

		public virtual IEnumerable<IType> GetGenericArguments()
		{
			return this._type.GetGenericArguments().Select(genericArgument => (TypeWrapper) genericArgument).Cast<IType>().ToList();
		}

		#endregion

		#region Implicit operator

		public static implicit operator TypeWrapper(Type type)
		{
			return type == null ? null : new TypeWrapper(type);
		}

		#endregion
	}
}