using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ApplyCostsVisualizationUnit : VisualizationUnit
    {
        public ApplyCostsVisualizationUnit(RunVisualizationDto algorithm) 
            : base(algorithm)
        {

        }

        public override void Visualize(IGraph<Vertex> graph)
        {
            graph.ApplyCosts(algorithm.GraphState.Costs);
        }
    }
}
