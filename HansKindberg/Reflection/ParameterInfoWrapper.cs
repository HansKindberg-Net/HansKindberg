using System;
using System.Reflection;

namespace HansKindberg.Reflection
{
	public class ParameterInfoWrapper : IParameterInfo
	{
		#region Fields

		private readonly ParameterInfo _parameterInfo;

		#endregion

		#region Constructors

		public ParameterInfoWrapper(ParameterInfo parameterInfo)
		{
			if(parameterInfo == null)
				throw new ArgumentNullException("parameterInfo");

			this._parameterInfo = parameterInfo;
		}

		#endregion

		#region Properties

		public virtual IType ParameterType
		{
			get { return (TypeWrapper) this._parameterInfo.ParameterType; }
		}

		#endregion

		#region Methods

		public static ParameterInfoWrapper FromParameterInfo(ParameterInfo parameterInfo)
		{
			return parameterInfo;
		}

		#endregion

		#region Implicit operator

		public static implicit operator ParameterInfoWrapper(ParameterInfo parameterInfo)
		{
			return parameterInfo == null ? null : new ParameterInfoWrapper(parameterInfo);
		}

		#endregion
	}
}