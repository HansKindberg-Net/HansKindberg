namespace HansKindberg
{
	public class ValueContainer<TValue>
	{
		#region Fields

		private TValue _value;

		#endregion

		#region Constructors

		public ValueContainer(TValue value)
		{
			this._value = value;
		}

		#endregion

		#region Properties

		public virtual TValue Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		#endregion
	}
}