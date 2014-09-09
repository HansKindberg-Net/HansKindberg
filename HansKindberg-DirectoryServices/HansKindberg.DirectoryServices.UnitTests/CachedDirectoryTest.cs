using System;
using System.DirectoryServices;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;
using HansKindberg.DirectoryServices.UnitTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class CachedDirectoryTest
	{
		#region Fields

		private static readonly IDirectoryCacheFactory _directoryCacheFactory = new FakedDirectoryCacheFactory();

		#endregion

		#region Properties

		private static IDirectoryCacheFactory DirectoryCacheFactory
		{
			get { return _directoryCacheFactory; }
		}

		#endregion

		#region Methods

		[TestMethod]
		public void ClearCache_ShouldCallClearOnTheCache()
		{
			var cleared = false;

			var directoryCacheMock = new Mock<IDirectoryCache>();
			directoryCacheMock.Setup(directoryCache => directoryCache.Clear()).Callback(() => cleared = true);

			var directoryCacheFactoryMock = new Mock<IDirectoryCacheFactory>();
			directoryCacheFactoryMock.Setup(directoryCacheFactory => directoryCacheFactory.Create(It.IsAny<string>())).Returns(directoryCacheMock.Object);

			var cachedDirectory = CreateCachedDirectoryMock(directoryCacheFactoryMock.Object).Object;

			Assert.IsFalse(cleared);

			cachedDirectory.ClearCache();

			Assert.IsTrue(cleared);
		}

		private static Mock<CachedDirectory> CreateCachedDirectoryMock()
		{
			return CreateCachedDirectoryMock(DirectoryCacheFactory);
		}

		private static Mock<CachedDirectory> CreateCachedDirectoryMock(IDirectoryCacheFactory directoryCacheFactory)
		{
			var directoryConnectionMock = new Mock<IDirectoryConnection>();
			directoryConnectionMock.Setup(directoryConnection => directoryConnection.Authentication).Returns(new DirectoryAuthentication());
			directoryConnectionMock.Setup(directoryConnection => directoryConnection.Url).Returns(new DirectoryUri {Host = "localhost", Scheme = Scheme.LDAP});

			var cachedDirectoryMock = new Mock<CachedDirectory>(new object[] {directoryConnectionMock.Object, Mock.Of<IDirectoryUriParser>(), Mock.Of<IDistinguishedNameParser>(), directoryCacheFactory}) {CallBase = true};

			return cachedDirectoryMock;
		}

		[TestMethod]
		public void Exists_Test()
		{
			const int expectedNumberOfTimesBaseMethodCalled = 1;

			var exists = DateTime.Now.Second%2 == 0;
			var numberOfTimesBaseMethodCalled = 0;
			const string path = "LDAP://localhost";
			var cachedDirectoryMock = CreateCachedDirectoryMock();
			cachedDirectoryMock.Setup(instance => instance.ExistsInternal(It.IsAny<string>(), It.IsAny<IDirectoryAuthentication>())).Callback(() => numberOfTimesBaseMethodCalled++).Returns(exists);

			var cachedDirectory = cachedDirectoryMock.Object;

			Assert.AreEqual(0, numberOfTimesBaseMethodCalled);
			Assert.IsFalse(cachedDirectory.Cache.Items.Any());

			Assert.AreEqual(exists, cachedDirectory.Exists(path, Mock.Of<IDirectoryAuthentication>()));

			Assert.AreEqual(expectedNumberOfTimesBaseMethodCalled, numberOfTimesBaseMethodCalled);
			Assert.AreEqual(1, cachedDirectory.Cache.Items.Count);

			for(int i = 0; i < 10; i++)
			{
				Assert.AreEqual(exists, cachedDirectory.Exists(path, Mock.Of<IDirectoryAuthentication>()));

				Assert.AreEqual(expectedNumberOfTimesBaseMethodCalled, numberOfTimesBaseMethodCalled);
			}

			Assert.AreEqual(1, cachedDirectory.Cache.Items.Count);
			Assert.AreEqual(typeof(CachedDirectory).FullName + "." + "Exists:" + "Path=" + path.ToUpperInvariant(), cachedDirectory.Cache.Items.Keys.First());
		}

		[TestMethod]
		public void Find_Test()
		{
			const int expectedNumberOfTimesBaseMethodCalled = 1;

			var find = Enumerable.Empty<IDirectoryItem>().ToArray();
			var numberOfTimesBaseMethodCalled = 0;
			const string path = "LDAP://localhost";
			var searchOptions = new SearchOptions
			{
				SearchScope = SearchScope.Subtree,
				PropertiesToLoad = new[] {"first", "second", "third"}
			};

			var cachedDirectoryMock = CreateCachedDirectoryMock();
			cachedDirectoryMock.Setup(instance => instance.FindInternal(It.IsAny<string>(), It.IsAny<ISearchOptions>(), It.IsAny<IDirectoryAuthentication>())).Callback(() => numberOfTimesBaseMethodCalled++).Returns(find);

			var cachedDirectory = cachedDirectoryMock.Object;

			Assert.AreEqual(0, numberOfTimesBaseMethodCalled);
			Assert.IsFalse(cachedDirectory.Cache.Items.Any());

			Assert.AreEqual(find, cachedDirectory.Find(path, searchOptions, Mock.Of<IDirectoryAuthentication>()));

			Assert.AreEqual(expectedNumberOfTimesBaseMethodCalled, numberOfTimesBaseMethodCalled);
			Assert.AreEqual(1, cachedDirectory.Cache.Items.Count);

			for(int i = 0; i < 10; i++)
			{
				Assert.AreEqual(find, cachedDirectory.Find(path, searchOptions, Mock.Of<IDirectoryAuthentication>()));

				Assert.AreEqual(expectedNumberOfTimesBaseMethodCalled, numberOfTimesBaseMethodCalled);
			}

			Assert.AreEqual(1, cachedDirectory.Cache.Items.Count);
			Assert.AreEqual(typeof(CachedDirectory).FullName + "." + "Find:" + "Path=" + path.ToUpperInvariant() + "&PropertiesToLoad=FIRST,SECOND,THIRD&SearchScope=Subtree", cachedDirectory.Cache.Items.Keys.First());
		}

		[TestMethod]
		public void Get_Test()
		{
			const int expectedNumberOfTimesBaseMethodCalled = 1;

			var get = new DirectoryItem();
			var numberOfTimesBaseMethodCalled = 0;
			const string path = "LDAP://localhost";
			var searchOptions = new SingleSearchOptions
			{
				PropertiesToLoad = new[] {"first", "second", "third"}
			};

			var cachedDirectoryMock = CreateCachedDirectoryMock();
			cachedDirectoryMock.Setup(instance => instance.GetInternal(It.IsAny<string>(), It.IsAny<ISingleSearchOptions>(), It.IsAny<IDirectoryAuthentication>())).Callback(() => numberOfTimesBaseMethodCalled++).Returns(get);

			var cachedDirectory = cachedDirectoryMock.Object;

			Assert.AreEqual(0, numberOfTimesBaseMethodCalled);
			Assert.IsFalse(cachedDirectory.Cache.Items.Any());

			Assert.AreEqual(get, cachedDirectory.Get(path, searchOptions, Mock.Of<IDirectoryAuthentication>()));

			Assert.AreEqual(expectedNumberOfTimesBaseMethodCalled, numberOfTimesBaseMethodCalled);
			Assert.AreEqual(1, cachedDirectory.Cache.Items.Count);

			for(int i = 0; i < 10; i++)
			{
				Assert.AreEqual(get, cachedDirectory.Get(path, searchOptions, Mock.Of<IDirectoryAuthentication>()));

				Assert.AreEqual(expectedNumberOfTimesBaseMethodCalled, numberOfTimesBaseMethodCalled);
			}

			Assert.AreEqual(1, cachedDirectory.Cache.Items.Count);
			Assert.AreEqual(typeof(CachedDirectory).FullName + "." + "Get:" + "Path=" + path.ToUpperInvariant() + "&PropertiesToLoad=FIRST,SECOND,THIRD&SearchScope=Base", cachedDirectory.Cache.Items.Keys.First());
		}

		#endregion
	}
}