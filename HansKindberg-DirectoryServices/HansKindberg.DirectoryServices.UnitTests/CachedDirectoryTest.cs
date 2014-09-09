using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using HansKindberg.DirectoryServices.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.DirectoryServices.UnitTests
{
	[TestClass]
	public class CachedDirectoryTest
	{
		#region Methods

		[TestMethod]
		public void ClearCache_ShouldCallClearOnTheCache()
		{
			var cleared = false;

			var directoryCacheMock = new Mock<IDirectoryCache>();
			directoryCacheMock.Setup(directoryCache => directoryCache.Clear()).Callback(() => cleared = true);

			var cachedDirectory = CreateCachedDirectoryMock(directoryCacheMock.Object).Object;

			Assert.IsFalse(cleared);

			cachedDirectory.ClearCache();

			Assert.IsTrue(cleared);
		}

		private static Mock<CachedDirectory> CreateCachedDirectoryMock()
		{
			return CreateCachedDirectoryMock(CreateDirectoryCache());
		}

		private static Mock<CachedDirectory> CreateCachedDirectoryMock(IDirectoryCache directoryCache)
		{
			var directoryConnectionMock = new Mock<IDirectoryConnection>();
			directoryConnectionMock.Setup(directoryConnection => directoryConnection.Authentication).Returns(new DirectoryAuthentication());
			directoryConnectionMock.Setup(directoryConnection => directoryConnection.Url).Returns(new DirectoryUri {Host = "localhost", Scheme = Scheme.LDAP});

			var cachedDirectoryMock = new Mock<CachedDirectory>(new object[] {directoryConnectionMock.Object, Mock.Of<IDirectoryUriParser>(), Mock.Of<IDistinguishedNameParser>(), directoryCache}) {CallBase = true};

			return cachedDirectoryMock;
		}

		private static IDirectoryCache CreateDirectoryCache()
		{
			return CreateDirectoryCacheMock().Object;
		}

		private static Mock<IDirectoryCache> CreateDirectoryCacheMock()
		{
			var items = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

			var directoryCacheMock = new Mock<IDirectoryCache>();

			directoryCacheMock.Setup(directoryCache => directoryCache.Items).Returns(items);

			directoryCacheMock.Setup(directoryCache => directoryCache.Clear()).Callback(items.Clear);
			directoryCacheMock.Setup(directoryCache => directoryCache.Get(It.IsAny<string>())).Returns((string key) => items.ContainsKey(key) ? items[key] : null);
			directoryCacheMock.Setup(directoryCache => directoryCache.Remove(It.IsAny<string>())).Returns((string key) =>
			{
				if(items.ContainsKey(key))
				{
					items.Remove(key);
					return true;
				}

				return false;
			});
			directoryCacheMock.Setup(directoryCache => directoryCache.Set(It.IsAny<string>(), It.IsAny<object>())).Callback((string key, object value) =>
			{
				if(value == null)
					throw new ArgumentNullException("value");

				if(items.ContainsKey(key))
					items[key] = value;

				items.Add(key, value);
			});

			return directoryCacheMock;
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