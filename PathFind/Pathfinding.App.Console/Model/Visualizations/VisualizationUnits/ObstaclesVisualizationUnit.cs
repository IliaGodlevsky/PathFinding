using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ObstaclesVisualizationUnit : VisualizationUnit
    {
        public ObstaclesVisualizationUnit(RunVisualizationDto algorithm) 
            : base(algorithm)
        {
        }

        public override void Visualize(IGraph<Vertex> graph)
        {
            algorithm.GraphState.Obstacles
                .Select(graph.Get)
                .ForEach(vertex => vertex.VisualizeAsObstacle());
        }
    }
}
