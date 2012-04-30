using System;
using System.Globalization;

namespace HansKindberg.Extensions
{
	public static class StringExtension
	{
		#region Methods

		public static string FirstCharacterToLower(this string value)
		{
			return value.FirstCharacterToLower(CultureInfo.CurrentCulture);
		}

		public static string FirstCharacterToLower(this string value, CultureInfo culture)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(value.Length == 0)
				return value;

			return value.Substring(0, 1).ToLower(culture) + value.Remove(0, 1);
		}

		public static string FirstCharacterToUpper(this string value)
		{
			return value.FirstCharacterToUpper(CultureInfo.CurrentCulture);
		}

		public static string FirstCharacterToUpper(this string value, CultureInfo culture)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			if(value.Length == 0)
				return value;

			return value.Substring(0, 1).ToUpper(culture) + value.Remove(0, 1);
		}

		#endregion
	}
}