using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.Windows
{
	public interface ILocalPathParser
	{
		#region Methods

		IList<string> Parse(string value);
		bool TryParse(string value, out IList<string> localPath);

		#endregion
	}
}