namespace HansKindberg
{
	public class LazyInitialization<T>
	{
		#region Fields

		private T _value;

		#endregion

		#region Constructors

		public LazyInitialization() {}

		public LazyInitialization(T value)
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