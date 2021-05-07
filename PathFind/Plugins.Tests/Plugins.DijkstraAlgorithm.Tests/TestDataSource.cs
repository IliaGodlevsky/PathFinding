using System;
using System.IO;

namespace Plugins.DijkstraAlgorithm.Tests
{
    internal static class TestDataSource
    {
        public static object[] TestData { get; }
        public static string[] TestGraphPaths { get; }

        static TestDataSource()
        {
            string testGraph2D = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestGraph2D");

            TestData = new[] { new object[] { testGraph2D, 195, 78 } };
            TestGraphPaths = new[] { testGraph2D };
        }
    }
}
