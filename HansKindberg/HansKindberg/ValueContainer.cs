namespace HansKindberg
{
	public class ValueContainer<T>
	{
		#region Fields

		private T _value;

		#endregion

		#region Constructors

		public ValueContainer() {}

		public ValueContainer(T value)
		{
			this._value = value;
		}

		#endregion

		#region Properties

		public virtual T Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		#endregion
	}
}