using System;
using GraphLibrary.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.GraphTests
{
    [TestClass]
    public class NullGraphTests
    {
        [TestMethod]
        public void Instance_NullGraph_ReturnsSameInstance()
        {
            var instance = NullGraph.Instance;

            Assert.AreSame(instance, NullGraph.Instance);
        }
    }
}
