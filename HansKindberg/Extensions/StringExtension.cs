using System;
using System.Globalization;
using System.Text.RegularExpressions;

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
			if(value == null)
				throw new ArgumentNullException("value");

			if(value.Length == 0)
				return value;

			return value.Substring(0, 1).ToUpper(culture) + value.Remove(0, 1);
		}

		public static bool Like(this string value, string pattern)
		{
			return value.Like(pattern, '*');
		}

		public static bool Like(this string value, string pattern, char wildcard)
		{
			return value.Like(pattern, wildcard, true);
		}

		public static bool Like(this string value, string pattern, bool caseInsensitive)
		{
			return value.Like(pattern, '*', caseInsensitive);
		}

		public static bool Like(this string value, string pattern, char wildcard, bool caseInsensitive)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(pattern == null)
				throw new ArgumentNullException("pattern");

			RegexOptions regexOptions = RegexOptions.Compiled;

			if(caseInsensitive)
				regexOptions |= RegexOptions.IgnoreCase;

			string regexPattern = pattern.Replace(wildcard.ToString(CultureInfo.InvariantCulture), "*");
			regexPattern = "^" + Regex.Escape(regexPattern).Replace("\\*", ".*") + "$";

			return Regex.IsMatch(value, regexPattern, regexOptions);
		}

		#endregion
	}
}