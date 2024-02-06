using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ApplyCostsVisualizationUnit : VisualizationUnit
    {
        public ApplyCostsVisualizationUnit(AlgorithmReadDto algorithm) 
            : base(algorithm)
        {

        }

        public override void Visualize(IGraph<Vertex> graph)
        {
            graph.ApplyCosts(algorithm.Costs);
        }
    }
}
