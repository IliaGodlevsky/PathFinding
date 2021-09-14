using GraphLib.Base;
using GraphLib.Interfaces;

namespace GraphLib.TestRealizations
{
    public sealed class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes)
            : base(dimensionSizes.Length, dimensionSizes)
        {

        }

        public override IGraph Clone()
        {
            var graph = new TestGraph(DimensionsSizes);
            foreach (var vertex in Vertices)
            {
                var temp = vertex.Clone();
                graph[temp.Position] = temp;
            }
            return graph;
        }
    }
}
