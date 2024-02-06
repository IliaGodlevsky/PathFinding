using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ObstaclesVisualizationUnit : VisualizationUnit
    {
        public ObstaclesVisualizationUnit(AlgorithmReadDto algorithm) 
            : base(algorithm)
        {
        }

        public override void Visualize(IGraph<Vertex> graph)
        {
            algorithm.Obstacles
                .Select(graph.Get)
                .ForEach(vertex => vertex.VisualizeAsObstacle());
        }
    }
}
