using GraphLib.Interfaces;
using GraphLib.TestRealizations.TestObjects;
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

            var coordinate = new TestCoordinate(coordinateValues);
            var neighborhood = (TNeighborhood)Activator.CreateInstance(typeof(TNeighborhood), coordinate);

            Assert.AreEqual(expectedResult, neighborhood.LongCount());
        }

        [TestCaseSource(nameof(CoordinateValues))]
        public void Coordinates_CoordinatesWithVariousDimensionsNumber_ReturnNeighboursWithoutSelf(int[] coordinateValues)
        {
            var coordinate = new TestCoordinate(coordinateValues);
            var neighborhood = (TNeighborhood)Activator.CreateInstance(typeof(TNeighborhood), coordinate);

            bool hasSelf = neighborhood.Any(values => values.Equals(coordinate));

            Assert.IsFalse(hasSelf);
        }

        [TestCaseSource(nameof(CoordinateValues))]
        public void Coordinates_CoordinatesWithVariousDimensionsCount_ReturnUniqueCoordinates(int[] coordinateValues)
        {
            var coordinate = new TestCoordinate(coordinateValues);
            var neighborhood = (TNeighborhood)Activator.CreateInstance(typeof(TNeighborhood), coordinate);

            var environmentCount = neighborhood.Distinct().Count();

            Assert.IsTrue(environmentCount == neighborhood.Count);
        }
    }
}
