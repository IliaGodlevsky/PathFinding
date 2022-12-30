using Pathfinding.GraphLib.Core.Realizations;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestObjects
{
    public sealed class TestVertexCost : VertexCost
    {
        public TestVertexCost(int cost)
            : base(cost, default)
        {

        }
    }
}
