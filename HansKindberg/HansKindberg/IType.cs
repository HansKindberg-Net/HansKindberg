using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HansKindberg.Reflection;

namespace HansKindberg
{
	[Obsolete("Reminder: Maybe this interface is \"overkill\" (see TypeWrapper). Maybe we can handle it with extensions instead, because both Type and ConstructorInfo are abstract classes that should be able to mock. So maybe we should try to remove this bit by bit.")]
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