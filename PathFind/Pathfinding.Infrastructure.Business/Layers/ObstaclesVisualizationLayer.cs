using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Visualization;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class ObstaclesVisualizationLayer : VisualizationLayer
    {
        public ObstaclesVisualizationLayer(RunVisualizationModel algorithm)
            : base(algorithm)
        {
        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            algorithm.GraphState.Obstacles
                .Select(graph.Get)
                .OfType<IGraphVisualizable>()
                .ForEach(vertex => vertex.VisualizeAsObstacle());
        }
    }
}
