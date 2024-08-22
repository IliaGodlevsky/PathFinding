using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Test.Algorithms.DataProviders;
using Pathfinding.Service.Interface;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.Algorithms.Heuristics
{
    [TestFixture, UnitTest]
    public class ChebyshevDistanceTests : HeuristicsTests
    {
        protected override IHeuristic Heuristic { get; } = new ChebyshevDistance();

        [TestCaseSource(typeof(ChebyshevDistanceDataProvider), nameof(ChebyshevDistanceDataProvider.TestData))]
        public override double Calculate_ValidVertices_ReturnsValidResult(int[] coordinate1, int[] coordinate2)
        {
            return base.Calculate_ValidVertices_ReturnsValidResult(coordinate1, coordinate2);
        }
    }
}
