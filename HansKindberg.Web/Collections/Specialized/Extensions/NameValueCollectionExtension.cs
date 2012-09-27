using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

// ReSharper disable CheckNamespace

namespace HansKindberg.Collections.Specialized.Extensions
// ReSharper restore CheckNamespace
{
	public static class NameValueCollectionExtension
	{
		#region Fields

		private const bool _omitLeadingQuestionMarkDefault = false;

		#endregion

		#region Methods

		public static string ToQueryString(this NameValueCollection nameValueCollection)
		{
			return nameValueCollection.ToQueryString(_omitLeadingQuestionMarkDefault);
		}

		public static string ToQueryString(this NameValueCollection nameValueCollection, bool omitLeadingQuestionMark)
		{
			return nameValueCollection.ToQueryStringInternal(omitLeadingQuestionMark, null);
		}

		public static string ToQueryString(this NameValueCollection nameValueCollection, Encoding encoding)
		{
			return nameValueCollection.ToQueryString(_omitLeadingQuestionMarkDefault, encoding);
		}

		public static string ToQueryString(this NameValueCollection nameValueCollection, bool omitLeadingQuestionMark, Encoding encoding)
		{
			if(nameValueCollection == null)
				throw new ArgumentNullException("nameValueCollection");

			if(encoding == null)
				throw new ArgumentNullException("encoding");

			return nameValueCollection.ToQueryStringInternal(omitLeadingQuestionMark, encoding);
		}

		private static string ToQueryStringInternal(this NameValueCollection nameValueCollection, bool omitLeadingQuestionMark, Encoding encoding)
		{
			if(nameValueCollection == null)
				throw new ArgumentNullException("nameValueCollection");

			string queryString = string.Empty;

			foreach(string key in nameValueCollection.Keys)
			{
				queryString += !string.IsNullOrEmpty(queryString) ? "&" : string.Empty;

				if(key != null)
					queryString += key + "=";

				// ReSharper disable AssignNullToNotNullAttribute
				string currentValue = nameValueCollection[key];
				// ReSharper restore AssignNullToNotNullAttribute

				if(currentValue == null)
					continue;

				string[] currentValueArray = currentValue.Split(",".ToCharArray(), StringSplitOptions.None);
				for(int i = 0; i < currentValueArray.Length; i++)
				{
					if(i > 0)
						queryString += ",";

					queryString += encoding == null ? HttpUtility.UrlEncode(currentValueArray[i]) : HttpUtility.UrlEncode(currentValueArray[i], encoding);
				}
			}

			return string.IsNullOrEmpty(queryString) ? string.Empty : (omitLeadingQuestionMark ? string.Empty : "?") + queryString;
		}

		#endregion
	}
}