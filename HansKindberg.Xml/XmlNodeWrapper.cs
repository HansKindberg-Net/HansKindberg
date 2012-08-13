using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml;
using HansKindberg.Extensions;

namespace HansKindberg.Xml
{
	public class XmlNodeWrapper : XmlNodeWrapper<XmlNode>
	{
		#region Constructors

		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public XmlNodeWrapper(XmlNode xmlNode) : base(xmlNode) {}

		#endregion
	}

	public class XmlNodeWrapper<TXmlNode> where TXmlNode : XmlNode
	{
		#region Fields

		private readonly TXmlNode _xmlNode;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public XmlNodeWrapper(TXmlNode xmlNode)
		{
			if(xmlNode == null)
				throw new ArgumentNullException(typeof(TXmlNode).Name.FirstCharacterToLower(CultureInfo.InvariantCulture));

			this._xmlNode = xmlNode;
		}

		#endregion

		#region Properties

		public virtual string InnerXml
		{
			get { return this.WrappedXmlNode.InnerXml; }
			set { this.WrappedXmlNode.InnerXml = value; }
		}

		public virtual string OuterXml
		{
			get { return this.WrappedXmlNode.OuterXml; }
		}

		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		protected internal virtual TXmlNode WrappedXmlNode
		{
			get { return this._xmlNode; }
		}

		#endregion
	}
}