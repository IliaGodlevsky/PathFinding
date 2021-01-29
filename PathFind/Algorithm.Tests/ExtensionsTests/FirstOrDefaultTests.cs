using GraphLib.Vertex.Interface;
using NUnit.Framework;
using Algorithm.Extensions;
using System;
using System.Collections.Generic;

namespace Algorithm.Tests.ExtensionsTests
{
    [TestFixture]
    class FirstOrDefaultTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void FirstOrDefault_NotEmptyCollection_ReturnsValidVertex()
        {
            var collection = new IVertex[] { new TestVertex() };

            var vertex = collection.FirstOrDefault();

            Assert.IsFalse(vertex.IsDefault);
        }

        [Test]
        public void FirstOrDefault_EmptyCollection_ReturnsDefaultVertex()
        {
            var collection = new IVertex[] { };

            var vertex = collection.FirstOrDefault();

            Assert.IsTrue(vertex.IsDefault);
        }

        [Test]
        public void FirstOrDefault_CollectionIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<IVertex> collection = null;

            Assert.Throws<ArgumentNullException>(() => collection.FirstOrDefault());
        }
    }
}
