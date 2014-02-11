namespace HansKindberg.Xml
{
	public interface IXmlDocument : IXmlNode
	{
		#region Methods

		void Load(string fileName);
		void LoadXml(string xml);
		void Save(string fileName);

		#endregion
	}
}