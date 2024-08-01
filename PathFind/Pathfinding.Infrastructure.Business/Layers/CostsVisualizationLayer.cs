using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class CostsVisualizationLayer : VisualizationLayer
    {
        public CostsVisualizationLayer(RunVisualizationModel algorithm)
            : base(algorithm)
        {

        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            graph.ApplyCosts(algorithm.GraphState.Costs);
        }
    }
}
