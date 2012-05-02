using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HansKindberg.Reflection;

namespace HansKindberg
{
	public interface IType
	{
		#region Properties

		string FriendlyFullName { get; }
		string FriendlyName { get; }
		string FullName { get; }
		bool IsGenericType { get; }
		string Name { get; }

		#endregion

		#region Methods

		IConstructorInfo GetConstructorWithMostParameters(bool excludeParameterlessConstructor);
		IConstructorInfo GetConstructorWithMostParameters(BindingFlags bindings, bool excludeParameterlessConstructor);
		IEnumerable<IConstructorInfo> GetConstructors(bool excludeParameterlessConstructors);
		IEnumerable<IConstructorInfo> GetConstructors(BindingFlags bindings, bool excludeParameterlessConstructors);
		IEnumerable<IConstructorInfo> GetConstructorsSortedByMostParametersFirst(bool excludeParameterlessConstructors);
		IEnumerable<IConstructorInfo> GetConstructorsSortedByMostParametersFirst(BindingFlags bindings, bool excludeParameterlessConstructors);

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		IEnumerable<IType> GetGenericArguments();

		#endregion
	}
}