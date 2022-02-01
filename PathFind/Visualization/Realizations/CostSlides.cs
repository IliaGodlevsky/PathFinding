using GraphLib.Interfaces;
using Visualization.Abstractions;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class CostSlides : GraphSlides<IVertexCost>, IVisualization
    {
        protected override IVertexCost GetActual(IVertex vertex)
        {
            return vertex.Cost;
        }

        protected override IVertexCost GetStored(IVertex vertex)
        {
            return vertex.Cost.Clone();
        }

        protected override void SetStored(IVertex vertex, IVertexCost cost)
        {
            vertex.Cost = cost.Clone();
        }
    }
}
