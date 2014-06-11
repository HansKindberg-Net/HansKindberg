using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.IntegrationTests
{
	[TestClass]
	[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Global")]
	public static class Global
	{
		#region Fields

		private const Scheme _defaultScheme = Scheme.LDAP;
		private const string _domainDistinguishedName = "DC=local,DC=net";
		private const string _domainName = "local.net";
		private const string _netBiosDomainName = "LOCAL";

		#endregion

		#region Properties

		public static Scheme DefaultScheme
		{
			get { return _defaultScheme; }
		}

		public static string DomainDistinguishedName
		{
			get { return _domainDistinguishedName; }
		}

		public static string DomainName
		{
			get { return _domainName; }
		}

		public static string NetBiosDomainName
		{
			get { return _netBiosDomainName; }
		}

		#endregion

		#region Methods

		[AssemblyCleanup]
		public static void AssemblyCleanup() {}

		[AssemblyInitialize]
		public static void AssemblyInitialize(TestContext testContext) {}

		#endregion
	}
}