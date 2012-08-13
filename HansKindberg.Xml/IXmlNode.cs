namespace HansKindberg.Xml
{
	public interface IXmlNode
	{
		#region Properties

		string InnerXml { get; set; }
		string OuterXml { get; }

		#endregion
	}
}