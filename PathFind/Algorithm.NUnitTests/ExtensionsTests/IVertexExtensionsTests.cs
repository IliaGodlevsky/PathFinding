using Algorithm.Extensions;
using GraphLib.Coordinates;
using NUnit.Framework;
using System;

namespace Algorithm.NUnitTests.ExtensionsTests
{
    [TestFixture]
    public class IVertexExtensionsTests
    {
        private TestVertex first;
        private TestVertex second;

        [SetUp]
        public void SetUp()
        {
            first = new TestVertex();
            second = new TestVertex();
        }

        [TestCase(4, 5, 12, 8, ExpectedResult = 8)]
        [TestCase(4, 9, 25, 11, ExpectedResult = 21)]
        [TestCase(0, 35, 25, 1, ExpectedResult = 34)]
        public double CalculateChebyshevDistanceTo_VertexWithCoordinate2D(int x, int y, int x1, int y1)
        {
            first.Position = new Coordinate2D(x, y);
            second.Position = new Coordinate2D(x1, y1);

            return first.CalculateChebyshevDistanceTo(second);
        }

        [TestCase(4, 5, 12, 8, 17, 35, ExpectedResult = 23)]
        [TestCase(4, 9, 25, 11, 0, 1, ExpectedResult = 24)]
        [TestCase(0, 35, 25, 1, 70, 14,  ExpectedResult = 35)]
        public double CalculateChebyshevDistanceTo_VertexWithCoordinate3D(int x, int y, int z,  int x1, int y1, int z1)
        {
            first.Position = new Coordinate3D(x, y, z);
            second.Position = new Coordinate3D(x1, y1, z1);

            return first.CalculateChebyshevDistanceTo(second);
        }

        [Test]
        public void CalculateChebyshevDistanceTo_ArgumentIsNull_ThrowsArgumentNullException()
        {
            first.Position = new Coordinate3D(0, 0, 0);

            Assert.Throws<ArgumentNullException>(() => first.CalculateChebyshevDistanceTo(null));
        }

        [Test]
        public void CalculateChebyshevDistanceTo_CallerIsNull_ThrowsArgumentNullException()
        {
            second.Position = new Coordinate3D(0, 0, 0);
            first = null;

            Assert.Throws<ArgumentNullException>(() => first.CalculateChebyshevDistanceTo(second));
        }

        [Test]
        public void CalculateChebyshevDistanceTo_CallerPositionIsNull_ThrowsArgumentException()
        {
            second.Position = new Coordinate3D(0, 0, 0);

            Assert.Throws<ArgumentException>(() => first.CalculateChebyshevDistanceTo(second));
        }

        [Test]
        public void CalculateChebyshevDistanceTo_ArgumentPositionIsNull_ThrowsArgumentException()
        {
            first.Position = new Coordinate3D(0, 0, 0);

            Assert.Throws<ArgumentException>(() => first.CalculateChebyshevDistanceTo(second));
        }

        [Test]
        public void CalculateChebyshevDistanceTo_DifferentPositionTypes_ThrowsArgumentException()
        {
            first.Position = new Coordinate3D(0, 0, 0);
            second.Position = new Coordinate2D(1, 1);

            Assert.Throws<ArgumentException>(() => first.CalculateChebyshevDistanceTo(second));
        }
    }
}
