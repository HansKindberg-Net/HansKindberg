using System;
using System.Globalization;

namespace HansKindberg.Web.HtmlTransforming
{
	[CLSCompliant(false)]
	public class DefaultHtmlTransformerFactory : IHtmlTransformerFactory
	{
		#region Methods

		public virtual T Create<T>() where T : IHtmlTransformer
		{
			return (T) this.Create(typeof(T));
		}

		public virtual IHtmlTransformer Create(Type type)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			Type htmlTransformerType = typeof(IHtmlTransformer);

			if(!htmlTransformerType.IsAssignableFrom(type))
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The type \"{0}\" must implement \"{1}\".", type, htmlTransformerType));

			return (IHtmlTransformer) Activator.CreateInstance(type);
		}

		#endregion
	}
}