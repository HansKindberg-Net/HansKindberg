using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Reflection;
using System.Runtime.InteropServices;
using HansKindberg.DirectoryServices.IntegrationTests.Helpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.IntegrationTests
{
	[TestClass]
	public class DirectoryServicesExceptionTest
	{
		#region Fields

		private const int _comExceptionErrorCode = 1000;
		private const string _comExceptionMessage = "COMException-Message";
		private static readonly ConstructorInfo _directoryServicesComExceptionConstructor = typeof(DirectoryServicesCOMException).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] {typeof(string), typeof(int), typeof(COMException)}, null);
		private const int _directoryServicesComExceptionExtendedError = 2000;
		private const string _directoryServicesComExceptionExtendedErrorMessage = "DirectoryServicesCOMException-ExtendedErrorMessage";
		private const string _exptectedStringValue = "HansKindberg.DirectoryServices.DirectoryServicesException (0x000003E8): COMException-Message. DirectoryServicesCOMException-ExtendedErrorMessage (2000). ---> System.DirectoryServices.DirectoryServicesCOMException (0x000003E8): COMException-Message";

		#endregion

		#region Methods

		private static void AssertDirectoryServicesExceptionsAreEqual(DirectoryServicesException expectedDirectoryServicesException, DirectoryServicesException deserializedDirectoryServicesException)
		{
			Assert.IsNotNull(deserializedDirectoryServicesException);

			Assert.AreEqual(_comExceptionErrorCode, deserializedDirectoryServicesException.ErrorCode);
			Assert.AreEqual(typeof(DirectoryServicesCOMException), deserializedDirectoryServicesException.InnerException.GetType());
			Assert.AreEqual(_comExceptionMessage, deserializedDirectoryServicesException.InnerException.Message);

			var innerExceptionAsDirectoryServicesComException = deserializedDirectoryServicesException.InnerException as DirectoryServicesCOMException;

			Assert.IsNotNull(innerExceptionAsDirectoryServicesComException);

			Assert.AreEqual(_directoryServicesComExceptionExtendedError, innerExceptionAsDirectoryServicesComException.ExtendedError);
			Assert.AreEqual(_directoryServicesComExceptionExtendedErrorMessage, innerExceptionAsDirectoryServicesComException.ExtendedErrorMessage);

			Assert.AreEqual(expectedDirectoryServicesException.Data.Count, deserializedDirectoryServicesException.Data.Count);
			Assert.AreEqual(expectedDirectoryServicesException.ErrorCode, deserializedDirectoryServicesException.ErrorCode);
			Assert.AreEqual(expectedDirectoryServicesException.HelpLink, deserializedDirectoryServicesException.HelpLink);
			Assert.AreEqual(expectedDirectoryServicesException.InnerException.GetType(), deserializedDirectoryServicesException.InnerException.GetType());
			Assert.AreEqual(expectedDirectoryServicesException.InnerException.Message, deserializedDirectoryServicesException.InnerException.Message);
			Assert.AreEqual(expectedDirectoryServicesException.Message, deserializedDirectoryServicesException.Message);
			Assert.AreEqual(expectedDirectoryServicesException.Source, deserializedDirectoryServicesException.Source);
			Assert.AreEqual(expectedDirectoryServicesException.StackTrace, deserializedDirectoryServicesException.StackTrace);
			Assert.AreEqual(expectedDirectoryServicesException.TargetSite, deserializedDirectoryServicesException.TargetSite);
			Assert.AreEqual(expectedDirectoryServicesException.ToString(), deserializedDirectoryServicesException.ToString());
		}

		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		private static COMException CreateComException(string message, int errorCode)
		{
			return new COMException(message, errorCode);
		}

		private static DirectoryServicesCOMException CreateDefaultDirectoryServicesComException()
		{
			return CreateDirectoryServicesComException(_directoryServicesComExceptionExtendedErrorMessage, _directoryServicesComExceptionExtendedError, _comExceptionMessage, _comExceptionErrorCode);
		}

		private static DirectoryServicesCOMException CreateDirectoryServicesComException(string extendedMessage, int extendedError, COMException comException)
		{
			return (DirectoryServicesCOMException) _directoryServicesComExceptionConstructor.Invoke(new object[] {extendedMessage, extendedError, comException});
		}

		private static DirectoryServicesCOMException CreateDirectoryServicesComException(string extendedMessage, int extendedError, string comExceptionMessage, int comExceptionErrorCode)
		{
			return CreateDirectoryServicesComException(extendedMessage, extendedError, CreateComException(comExceptionMessage, comExceptionErrorCode));
		}

		private static DirectoryServicesException CreateDirectoryServicesException()
		{
			return new DirectoryServicesException(CreateDefaultDirectoryServicesComException());
		}

		[TestMethod]
		public void Message_Test()
		{
			var directoryServicesException = new DirectoryServicesException(CreateDefaultDirectoryServicesComException());

			Assert.AreEqual(_comExceptionMessage + ". " + _directoryServicesComExceptionExtendedErrorMessage + " (" + _directoryServicesComExceptionExtendedError + ").", directoryServicesException.Message);
		}

		[TestMethod]
		public void Prerequisite_DirectoryServicesComException_ShouldNotFullyRestoreOnDeserializing()
		{
			var directoryServicesComException = CreateDefaultDirectoryServicesComException();

			Assert.AreEqual(_comExceptionErrorCode, directoryServicesComException.ErrorCode);
			Assert.AreEqual(_comExceptionMessage, directoryServicesComException.Message);
			Assert.AreEqual(_directoryServicesComExceptionExtendedError, directoryServicesComException.ExtendedError);
			Assert.AreEqual(_directoryServicesComExceptionExtendedErrorMessage, directoryServicesComException.ExtendedErrorMessage);

			var serialized = directoryServicesComException.SerializeBinary();

			var deserializedDirectoryServicesComException = (DirectoryServicesCOMException) ObjectExtension.DeserializeBinary(serialized);

			Assert.AreEqual(_comExceptionErrorCode, deserializedDirectoryServicesComException.ErrorCode);
			Assert.AreEqual(_comExceptionMessage, deserializedDirectoryServicesComException.Message);
			Assert.AreEqual(0, deserializedDirectoryServicesComException.ExtendedError);
			Assert.AreEqual(string.Empty, deserializedDirectoryServicesComException.ExtendedErrorMessage);
		}

		[TestMethod]
		public void ShouldBeSerializableAndDeserializable()
		{
			var directoryServicesException = CreateDirectoryServicesException();

			var serialized = directoryServicesException.SerializeBinary();

			var deserializedDirectoryServicesException = (DirectoryServicesException) ObjectExtension.DeserializeBinary(serialized);

			AssertDirectoryServicesExceptionsAreEqual(directoryServicesException, deserializedDirectoryServicesException);
		}

		[TestMethod]
		public void ToString_Test()
		{
			var directoryServicesException = new DirectoryServicesException(CreateDefaultDirectoryServicesComException());

			Assert.AreEqual(_exptectedStringValue, directoryServicesException.ToString());
		}

		#endregion
	}
}