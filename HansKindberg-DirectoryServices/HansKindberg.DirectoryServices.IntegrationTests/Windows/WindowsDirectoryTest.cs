using System;
using System.DirectoryServices;
using HansKindberg.DirectoryServices.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.IntegrationTests.Windows
{
	[TestClass]
	public class WindowsDirectoryTest
	{
		#region Methods

		private static DirectoryEntry CreateLocalMachineEntry()
		{
			return new DirectoryEntry("WinNT://" + Environment.MachineName);
		}

		private static ILocalPathParser CreateLocalPathParser()
		{
			return new LocalPathParser();
		}

		private static WindowsDirectory CreateWindowsDirectory()
		{
			var localPathParser = CreateLocalPathParser();

			return new WindowsDirectory(localPathParser, CreateWindowsDirectoryUriParser(localPathParser));
		}

		//private static WindowsDirectory CreateWindowsDirectory(IWindowsDirectoryConnection windowsDirectoryConnection)
		//{
		//	var localPathParser = CreateLocalPathParser();

		//	return new WindowsDirectory(windowsDirectoryConnection, localPathParser, CreateWindowsDirectoryUriParser(localPathParser));
		//}
		private static IWindowsDirectoryUriParser CreateWindowsDirectoryUriParser(ILocalPathParser localPathParser)
		{
			return new WindowsDirectoryUriParser(localPathParser);
		}

		[TestMethod]
		public void Get_WithPathParameter_IfConnectingToTheLocalMachine_ShouldReturnTheCorrectProperties()
		{
			using(var directoryEntry = CreateLocalMachineEntry())
			{
				var windowsDirectory = (IGlobalWindowsDirectory) CreateWindowsDirectory();

				var directoryItem = windowsDirectory.Get(directoryEntry.Path);

				GeneralDirectoryTest.AssertDirectoryEntryAndGeneralDirectoryItemAreEqual(directoryEntry, directoryItem);
			}
		}

		#endregion
	}
}