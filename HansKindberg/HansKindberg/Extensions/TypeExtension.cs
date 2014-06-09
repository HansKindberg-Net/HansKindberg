using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HansKindberg.Extensions
{
	public static class TypeExtension
	{
		#region Fields

		private const BindingFlags _defaultConstructorBindings = BindingFlags.Instance | BindingFlags.Public;

		#endregion

		#region Methods

		public static string FriendlyFullName(this Type type)
		{
			return type.GetFriendlyName(t => t.FullName);
		}

		public static string FriendlyName(this Type type)
		{
			return type.GetFriendlyName(t => t.Name);
		}

		public static ConstructorInfo GetConstructorWithMostParameters(this Type type, bool excludeParameterlessConstructor)
		{
			return type.GetConstructorWithMostParameters(_defaultConstructorBindings, excludeParameterlessConstructor);
		}

		public static ConstructorInfo GetConstructorWithMostParameters(this Type type, BindingFlags bindings, bool excludeParameterlessConstructor)
		{
			return type.GetConstructorsSortedByMostParametersFirst(bindings, excludeParameterlessConstructor).FirstOrDefault();
		}

		public static ConstructorInfo[] GetConstructors(this Type type, bool excludeParameterlessConstructors)
		{
			return type.GetConstructors(_defaultConstructorBindings, excludeParameterlessConstructors);
		}

		public static ConstructorInfo[] GetConstructors(this Type type, BindingFlags bindings, bool excludeParameterlessConstructors)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			return type.GetConstructors(bindings)
				.Where(constructor => constructor.GetParameters().Length > 0 || !excludeParameterlessConstructors)
				.ToArray();
		}

		public static ConstructorInfo[] GetConstructorsSortedByMostParametersFirst(this Type type, bool excludeParameterlessConstructors)
		{
			return type.GetConstructorsSortedByMostParametersFirst(_defaultConstructorBindings, excludeParameterlessConstructors);
		}

		public static ConstructorInfo[] GetConstructorsSortedByMostParametersFirst(this Type type, BindingFlags bindings, bool excludeParameterlessConstructors)
		{
			return type.GetConstructors(bindings, excludeParameterlessConstructors)
				.OrderByDescending(constructor => constructor.GetParameters().Length)
				.ToArray();
		}

		private static string GetFriendlyName(this Type type, Expression<Func<Type, string>> expression)
		{
			string name = expression.Compile().Invoke(type);

			if(!type.IsGenericType)
				return name;

			name = name.Substring(0, name.IndexOf("`", StringComparison.Ordinal));

			string genericArgumentValue = string.Empty;

			foreach(Type genericArgument in type.GetGenericArguments())
			{
				if(!string.IsNullOrEmpty(genericArgumentValue))
					genericArgumentValue += ", ";

				genericArgumentValue += genericArgument.GetFriendlyName(expression);
			}

			return name + "<" + genericArgumentValue + ">";
		}

		#endregion
	}
}