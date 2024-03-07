using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ApplyCostsVisualizationLayer : VisualizationLayer
    {
        public ApplyCostsVisualizationLayer(RunVisualizationDto algorithm)
            : base(algorithm)
        {

        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            graph.ApplyCosts(algorithm.GraphState.Costs);
        }
    }
}
