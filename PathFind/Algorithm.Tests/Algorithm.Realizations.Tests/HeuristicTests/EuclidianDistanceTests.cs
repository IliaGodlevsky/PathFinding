using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using NUnit.Framework;

namespace Algorithm.Realizations.Tests.HeuristicTests
{
    [TestFixture]
    public class EuclidianDistanceTests : BaseHeuristicTests
    {
        protected override IHeuristic Heuristic { get; }

        public EuclidianDistanceTests()
        {
            Heuristic = new EuclidianDistance();
        }

        [TestCase(new[] { 3, 15 }, new[] { 4, 6 }, ExpectedResult = 9.1)]
        [TestCase(new[] { 7, 1, 20 }, new[] { 5, 9, 1 }, ExpectedResult = 20.7)]
        [TestCase(new[] { 5 }, new[] { 33 }, ExpectedResult = 28)]
        [TestCase(new[] { 4, 7, 3, 20 }, new[] { 1, 5, 16, 9 }, ExpectedResult = 17.4)]
        public override double Calculate_EqualNumberOfCoordinateValues_ReturnsValidValue(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            return base.Calculate_EqualNumberOfCoordinateValues_ReturnsValidValue(fromVertexCoordinateValues, toVertexCoordinateValues);
        }
    }
}
