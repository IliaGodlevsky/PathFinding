using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ObstaclesVisualizationLayer : VisualizationLayer
    {
        public ObstaclesVisualizationLayer(RunVisualizationDto algorithm)
            : base(algorithm)
        {
        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            algorithm.GraphState.Obstacles
                .Select(graph.Get)
                .OfType<ITotallyVisualizable>()
                .ForEach(vertex => vertex.VisualizeAsObstacle());
        }
    }
}
