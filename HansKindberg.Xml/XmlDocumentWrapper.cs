using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace HansKindberg.Xml
{
	public class XmlDocumentWrapper : XmlNodeWrapper<XmlDocument>, IXmlDocument
	{
		#region Constructors

		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public XmlDocumentWrapper(XmlDocument xmlDocument) : base(xmlDocument) {}

		#endregion

		#region Methods

		public virtual void Load(string fileName)
		{
			this.WrappedXmlNode.Load(fileName);
		}

		public virtual void LoadXml(string xml)
		{
			this.WrappedXmlNode.LoadXml(xml);
		}

		public virtual void Save(string fileName)
		{
			this.WrappedXmlNode.Save(fileName);
		}

		#endregion
	}
}