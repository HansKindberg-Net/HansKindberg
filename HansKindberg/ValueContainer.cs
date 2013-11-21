using System;

namespace HansKindberg
{
	[Obsolete("This class has been renamed. Use HansKindberg.LazyInitialization<T> instead.", false)]
	public class ValueContainer<TValue> : LazyInitialization<TValue>
	{
		#region Constructors

		public ValueContainer() {}
		public ValueContainer(TValue value) : base(value) {}

		#endregion
	}
}