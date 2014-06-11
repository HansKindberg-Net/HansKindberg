using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace HansKindberg.DirectoryServices
{
	public class DistinguishedNameParser : IDistinguishedNameParser
	{
		#region Methods

		public virtual IDistinguishedName Parse(string value)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(value.Length == 0)
				throw new ArgumentException("The value can not be empty.", "value");

			var distinguishedName = new DistinguishedName();

			try
			{
				foreach(var component in value.Split(new[] {DistinguishedName.DefaultComponentDelimiter}))
				{
					var componentParts = component.Split(new[] {DistinguishedNameComponent.DefaultNameValueDelimiter});

					if(componentParts.Length != 2)
						throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Each component in the distinguished name must consist of a name and a value separated by \"{0}\".", DistinguishedNameComponent.DefaultNameValueDelimiter));

					distinguishedName.Components.Add(new DistinguishedNameComponent(componentParts[0], componentParts[1]));
				}
			}
			catch(Exception exception)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The distinguished name \"{0}\" is invalid.", value), exception);
			}

			return distinguishedName;
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool TryParse(string value, out IDistinguishedName distinguishedName)
		{
			distinguishedName = null;

			if(string.IsNullOrEmpty(value))
				return false;

			try
			{
				distinguishedName = this.Parse(value);
				return true;
			}
			catch
			{
				distinguishedName = null;
				return false;
			}
		}

		#endregion
	}
}