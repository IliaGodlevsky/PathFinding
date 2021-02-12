using Algorithm.Extensions;
using Algorithm.Tests.TestsInfrastructure.Objects;
using GraphLib.Interface;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Algorithm.Tests.Tests.ExtensionsTests
{
    [TestFixture]
    internal class FirstOrDefaultTests
    {
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
