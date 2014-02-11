using System;
using System.Reflection;

namespace HansKindberg.Reflection
{
	public class ConstructorInfoWrapper : IConstructorInfo
	{
		#region Fields

		private readonly ConstructorInfo _constructorInfo;

		#endregion

		#region Constructors

		public ConstructorInfoWrapper(ConstructorInfo constructorInfo)
		{
			if(constructorInfo == null)
				throw new ArgumentNullException("constructorInfo");

			this._constructorInfo = constructorInfo;
		}

		#endregion

		#region Methods

		public static ConstructorInfoWrapper FromConstructorInfo(ConstructorInfo constructorInfo)
		{
			return constructorInfo;
		}

		public virtual object Invoke(object[] parameters)
		{
			return this._constructorInfo.Invoke(parameters);
		}

		#endregion

		#region Implicit operator

		public static implicit operator ConstructorInfoWrapper(ConstructorInfo constructorInfo)
		{
			return constructorInfo == null ? null : new ConstructorInfoWrapper(constructorInfo);
		}

		#endregion
	}
}