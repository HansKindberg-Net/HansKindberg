using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class DirectoryEntryWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.DirectoryServices.DirectoryEntryWrapper")]
		public void Constructor_IfTheDirectoryEntryParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(new DirectoryEntryWrapper(null)) {}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.DirectoryServices.DirectoryEntryWrapper")]
		public void Constructor_IfTheDirectoryEntryParameterIsNull_ShouldThrowAnArgumentNullExceptionWithParameterNameSetToDirectoryEntryWithTheFirstCharacterToLower()
		{
			string parameterName = null;

			try
			{
				using(new DirectoryEntryWrapper(null)) {}
			}
			catch(ArgumentNullException argumentNullException)
			{
				parameterName = argumentNullException.ParamName;
			}

			Assert.AreEqual("directoryEntry", parameterName);
		}

		#endregion
	}
}