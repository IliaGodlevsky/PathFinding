using GraphLib.Base;

namespace Plugins.DijkstraAlgorithm.Tests.Objects.TestObjects
{
    internal sealed class TestVertexCost : BaseVertexCost
    {
        public TestVertexCost(int cost)
            : base(cost)
        {

        }

        public TestVertexCost() : base()
        {

        }
    }
}
