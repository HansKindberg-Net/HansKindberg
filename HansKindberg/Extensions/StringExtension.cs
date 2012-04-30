using System.Globalization;

namespace HansKindberg.Extensions
{
	public static class StringExtension
	{
		#region Methods

		public static string FirstCharacterToLower(this string str)
		{
			return str.FirstCharacterToLower(CultureInfo.CurrentCulture);
		}

		public static string FirstCharacterToLower(this string str, CultureInfo culture)
		{
			if(str == string.Empty)
				return string.Empty;

			return str.Substring(0, 1).ToLower(culture) + str.Remove(0, 1);
		}

		public static string FirstCharacterToUpper(this string str)
		{
			return str.FirstCharacterToUpper(CultureInfo.CurrentCulture);
		}

		public static string FirstCharacterToUpper(this string str, CultureInfo culture)
		{
			if(str == string.Empty)
				return string.Empty;

			return str.Substring(0, 1).ToUpper(culture) + str.Remove(0, 1);
		}

		#endregion
	}
}