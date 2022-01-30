using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using NUnit.Framework;

namespace Algorithm.Realizations.Tests.HeuristicTests
{
    [TestFixture]
    public class ManhattanDistanceTest : BaseHeuristicTests
    {
        protected override IHeuristic Heuristic { get; }

        public ManhattanDistanceTest() : base()
        {
            Heuristic = new ManhattanDistance();
        }

        [TestCase(new[] { 3, 15 }, new[] { 4, 6 }, ExpectedResult = 10)]
        [TestCase(new[] { 7, 1, 20 }, new[] { 5, 9, 1 }, ExpectedResult = 29)]
        [TestCase(new[] { 5 }, new[] { 33 }, ExpectedResult = 28)]
        [TestCase(new[] { 4, 7, 3, 20 }, new[] { 1, 5, 16, 9 }, ExpectedResult = 29)]
        public override double Calculate_EqualNumberOfCoordinateValues_ReturnsValidValue(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            return base.Calculate_EqualNumberOfCoordinateValues_ReturnsValidValue(fromVertexCoordinateValues, toVertexCoordinateValues);
        }
    }
}