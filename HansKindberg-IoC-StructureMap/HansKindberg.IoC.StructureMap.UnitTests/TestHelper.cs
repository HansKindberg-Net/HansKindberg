using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace HansKindberg.IoC.StructureMap.UnitTests
{
	public static class TestHelper
	{
		#region Methods

		public static void AssertStructureMapIsCleared()
		{
			Assert.AreEqual(15, ObjectFactory.WhatDoIHave().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length);
		}

		public static void ClearStructureMap()
		{
			ObjectFactory.Initialize(initializer => { });
		}

		#endregion
	}
}