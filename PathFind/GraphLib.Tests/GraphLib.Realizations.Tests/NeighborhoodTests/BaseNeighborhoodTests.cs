using Autofac;
using Autofac.Extras.Moq;
using GraphLib.Interfaces;
using GraphLib.Realizations.Tests.Extenions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace GraphLib.Realizations.Tests.NeighborhoodTests
{
    public abstract class BaseNeighborhoodTests<TNeighborhood> where TNeighborhood : INeighborhood
    {
        public static IEnumerable CoordinateValues { get; }

        static BaseNeighborhoodTests()
        {
            CoordinateValues = new TestCaseData[]
            {
                new TestCaseData(Array.Empty<int>()),
                new TestCaseData(new[] { 11 }),
                new TestCaseData(new[] { 20, 30 }),
                new TestCaseData(new[] { 20, 10, 3 })
            };
        }

        protected abstract ulong GetExpectedNeighborhoodCount(int dimensionsCount);

        [TestCaseSource(nameof(CoordinateValues))]
        public void Coordinates_CoordinatesWithVariousDimensionsNumber_ReturnValidNumberOfNeighbours(int[] coordinateValues)
        {
            ulong expectedResult = GetExpectedNeighborhoodCount(coordinateValues.Length);
            using (var mock = AutoMock.GetLoose())
            {
                mock.MockCoordinate(coordinateValues);
                var neighboursCoordinate = mock.Create<TNeighborhood>();

                var environment = neighboursCoordinate.Neighbours;

                Assert.AreEqual(expectedResult, environment.LongCount());
            }
        }

        [TestCaseSource(nameof(CoordinateValues))]
        public void Coordinates_CoordinatesWithVariousDimensionsNumber_ReturnNeighboursWithoutSelf(int[] coordinateValues)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.MockCoordinate(coordinateValues);
                var neighboursCoordinate = mock.Create<TNeighborhood>();

                var environment = neighboursCoordinate.Neighbours;
                var coordinate = mock.Container.Resolve<ICoordinate>();
                bool hasSelf = environment.Any(values => values.Equals(coordinate));

                Assert.IsFalse(hasSelf);
            }
        }

        [TestCaseSource(nameof(CoordinateValues))]
        public void Coordinates_CoordinatesWithVariousDimensionsCount_ReturnUniqueCoordinates(int[] coordinateValues)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.MockCoordinate(coordinateValues);
                var neighboursCoordinate = mock.Create<TNeighborhood>();

                var environment = neighboursCoordinate.Neighbours;
                var environmentCount = environment.Distinct().Count();

                Assert.IsTrue(environmentCount == environment.Count);
            }
        }
    }
}
