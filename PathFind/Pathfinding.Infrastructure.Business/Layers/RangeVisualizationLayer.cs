using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Visualization;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class RangeVisualizationLayer : VisualizationLayer
    {
        public RangeVisualizationLayer(RunVisualizationModel algorithm)
            : base(algorithm)
        {

        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            algorithm.GraphState.Range
                .Select(graph.Get)
                .OfType<IRangeVisualizable>()
                .VisualizeAsRange();
        }
    }
}
