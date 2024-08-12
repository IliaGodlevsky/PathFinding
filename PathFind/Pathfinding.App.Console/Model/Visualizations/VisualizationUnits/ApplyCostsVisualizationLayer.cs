using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class ApplyCostsVisualizationLayer : VisualizationLayer
    {
        public ApplyCostsVisualizationLayer(RunVisualizationModel algorithm)
            : base(algorithm)
        {

        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            graph.ApplyCosts(algorithm.GraphState.Costs);
        }
    }
}
