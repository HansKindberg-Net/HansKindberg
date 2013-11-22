using System;

namespace HansKindberg.IoC.StructureMap.UnitTests.Mocks
{
	public class SomeConcreteClassWithoutParameterlessConstructor
	{
		#region Fields

		private readonly ISomeInterface _someInterface;

		#endregion

		#region Constructors

		public SomeConcreteClassWithoutParameterlessConstructor(ISomeInterface someInterface)
		{
			if(someInterface == null)
				throw new ArgumentNullException("someInterface");

			this._someInterface = someInterface;
		}

		#endregion

		#region Properties

		protected internal virtual ISomeInterface SomeInterface
		{
			get { return this._someInterface; }
		}

		#endregion
	}
}