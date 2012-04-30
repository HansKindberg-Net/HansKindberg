using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HansKindberg.Extensions
{
	public static class TypeExtension
	{
		#region Fields

		private const BindingFlags _defaultConstructorBindingFlags = BindingFlags.Instance | BindingFlags.Public;

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
			return type.GetConstructorWithMostParameters(_defaultConstructorBindingFlags, excludeParameterlessConstructor);
		}

		public static ConstructorInfo GetConstructorWithMostParameters(this Type type, BindingFlags bindingFlags, bool excludeParameterlessConstructor)
		{
			return type.GetConstructorsSortedByMostParametersFirst(bindingFlags, excludeParameterlessConstructor).FirstOrDefault();
		}

		public static ConstructorInfo[] GetConstructors(this Type type, bool excludeParameterlessConstructors)
		{
			return type.GetConstructors(_defaultConstructorBindingFlags, excludeParameterlessConstructors);
		}

		public static ConstructorInfo[] GetConstructors(this Type type, BindingFlags bindingFlags, bool excludeParameterlessConstructors)
		{
			return type.GetConstructors(bindingFlags)
				.Where(constructor => constructor.GetParameters().Length > 0 || !excludeParameterlessConstructors)
				.ToArray();
		}

		public static ConstructorInfo[] GetConstructorsSortedByMostParametersFirst(this Type type, bool excludeParameterlessConstructors)
		{
			return type.GetConstructorsSortedByMostParametersFirst(_defaultConstructorBindingFlags, excludeParameterlessConstructors);
		}

		public static ConstructorInfo[] GetConstructorsSortedByMostParametersFirst(this Type type, BindingFlags bindingFlags, bool excludeParameterlessConstructors)
		{
			return type.GetConstructors(bindingFlags, excludeParameterlessConstructors)
				.OrderByDescending(constructor => constructor.GetParameters().Length)
				.ToArray();
		}

		private static string GetFriendlyName(this Type type, Expression<Func<Type, string>> expression)
		{
			string name = expression.Compile().Invoke(type);

			if(!type.IsGenericType)
				return name;

			name = name.Substring(0, name.IndexOf("`"));

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