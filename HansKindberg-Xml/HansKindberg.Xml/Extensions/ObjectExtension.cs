using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HansKindberg.Xml.Extensions
{
	public static class ObjectExtension
	{
		#region Methods

		/// <summary>
		/// Creates an object from xml. Throws an "System.NotSupportedException" exception if the type "T" is not serializable.
		/// </summary>
		/// <typeparam name="T">The type to create.</typeparam>
		/// <param name="xml">The xml to create the object from.</param>
		/// <returns></returns>
		public static T FromXml<T>(string xml) where T : class
		{
			if(xml == null)
				throw new ArgumentNullException("xml");

			try
			{
				T obj;
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

				using(StringReader stringReader = new StringReader(xml))
				{
					obj = (T) xmlSerializer.Deserialize(stringReader);
				}

				return obj;
			}
			catch(Exception exception)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "The xml-string could not be deserialized to an object of type \"{0}\".", typeof(T).FullName), exception);
			}
		}

		/// <summary>
		/// Serializes the object to the XML. Throws an "System.NotSupportedException" exception if the object is not serializable.
		/// </summary>
		/// <param name="anyObject">The object to serialize to xml.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object")]
		public static string ToXml(this object anyObject)
		{
			return anyObject.ToXml(false, true, true);
		}

		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object")]
		public static string ToXml(this object anyObject, bool omitXmlDeclarations, bool newLine, bool indent)
		{
			if(anyObject == null)
				throw new ArgumentNullException("anyObject");

			try
			{
				string xml;

				XmlSerializer xmlSerializer = new XmlSerializer(anyObject.GetType());

				StringBuilder stringBuilder = new StringBuilder();

				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
					{
						Indent = true,
						IndentChars = indent ? "\t" : "",
						NewLineChars = newLine ? Environment.NewLine : "",
						OmitXmlDeclaration = omitXmlDeclarations
					};

				XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
				if(omitXmlDeclarations)
					xmlSerializerNamespaces.Add("", "");

				using(XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
				{
					xmlSerializer.Serialize(xmlWriter, anyObject, xmlSerializerNamespaces);
					xml = stringBuilder.ToString();
				}

				return xml;
			}
			catch(Exception exception)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "The object of type \"{0}\". could not be serialized to xml.", anyObject.GetType()), exception);
			}
		}

		#endregion
	}
}