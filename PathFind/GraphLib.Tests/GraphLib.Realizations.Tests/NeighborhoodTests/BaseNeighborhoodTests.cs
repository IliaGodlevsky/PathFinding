using Autofac;
using Autofac.Extras.Moq;
using GraphLib.Interfaces;
using NUnit.Framework;
using System.Linq;

namespace GraphLib.Realizations.Tests.NeighborhoodTests
{
    public abstract class BaseNeighborhoodTests<TNeighborhood> where TNeighborhood : INeighborhood
    {
        protected abstract ulong GetExpectedNeighborhoodCount(int dimensionsCount);

        [TestCase(new int[] { })]
        [TestCase(new[] { 2 })]
        [TestCase(new[] { 2, 3 })]
        [TestCase(new[] { 2, 3, 4 })]
        [TestCase(new[] { 2, 3, 4, 5 })]
        [TestCase(new[] { 2, 3, 4, 5, 6 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void Coordinates_CoordinatesWithVariousDimensionsNumber_ReturnValidNumberOfNeighbours(int[] coordinateValues)
        {
            ulong expectedResult = GetExpectedNeighborhoodCount(coordinateValues.Length);
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICoordinate>().Setup(c => c.CoordinatesValues).Returns(coordinateValues);
                var neighboursCoordinate = mock.Create<TNeighborhood>();

                var environment = neighboursCoordinate.Neighbours;

                Assert.AreEqual(expectedResult, environment.LongCount());
            }
        }

        [TestCase(new int[] { })]
        [TestCase(new[] { 2 })]
        [TestCase(new[] { 2, 3 })]
        [TestCase(new[] { 2, 3, 4 })]
        [TestCase(new[] { 2, 3, 4, 5 })]
        [TestCase(new[] { 2, 3, 4, 5, 6 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void Coordinates_CoordinatesWithVariousDimensionsNumber_ReturnNeighboursWithoutSelf(int[] coordinateValues)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICoordinate>().Setup(c => c.CoordinatesValues).Returns(coordinateValues);
                var neighboursCoordinate = mock.Create<TNeighborhood>();

                var environment = neighboursCoordinate.Neighbours;
                var coordinate = mock.Container.Resolve<ICoordinate>();
                bool hasSelf = environment.Any(values => values.Equals(coordinate));

                Assert.IsFalse(hasSelf);
            }
        }

        [TestCase(new int[] { })]
        [TestCase(new[] { 3 })]
        [TestCase(new[] { 1, 7 })]
        [TestCase(new[] { 100, 25, 33 })]
        [TestCase(new[] { 20, 13, 34, 58 })]
        [TestCase(new[] { 12, 32, 44, 51, 69 })]
        [TestCase(new[] { 23, 34, 45, 56, 67, 78 })]
        [TestCase(new[] { 201, 33, 84, 15, 16, 73, 81 })]
        [TestCase(new[] { 201, 33, 84, 15, 16, 73, 81, 11 })]
        public void Coordinates_CoordinatesWithVariousDimensionsCount_ReturnUniqueCoordinates(int[] coordinateValues)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICoordinate>().Setup(c => c.CoordinatesValues).Returns(coordinateValues);
                var neighboursCoordinate = mock.Create<TNeighborhood>();

                var environment = neighboursCoordinate.Neighbours;

                Assert.IsTrue(environment.Distinct().Count() == environment.Count);
            }
        }
    }
}
