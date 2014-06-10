using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices
{
	public interface IDirectoryUriParser {
		IDirectoryUri Parse(string value);

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		bool TryParse(string value, out IDirectoryUri directoryUri);
	}
}