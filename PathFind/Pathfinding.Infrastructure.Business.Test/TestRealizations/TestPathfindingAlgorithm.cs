using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Data.Pathfinding;
using System.Collections;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal sealed class TestPathfindingAlgorithm : PathfindingAlgorithm<ArrayList>
    {
        public TestPathfindingAlgorithm()
            : base(Enumerable.Empty<IVertex>())
        {
        }

        protected override void MoveNextVertex()
        {

        }

        protected override void InspectCurrentVertex()
        {

        }

        protected override void VisitCurrentVertex()
        {

        }
    }
}
