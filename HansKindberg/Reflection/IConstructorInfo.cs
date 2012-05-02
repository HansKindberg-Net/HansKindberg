namespace HansKindberg.Reflection
{
	public interface IConstructorInfo
	{
		#region Methods

		object Invoke(object[] parameters);

		#endregion
	}
}