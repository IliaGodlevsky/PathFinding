using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public abstract class VisualizationLayer : ILayer
    {
        protected readonly RunVisualizationModel algorithm;

        protected VisualizationLayer(RunVisualizationModel algorithm)
        {
            this.algorithm = algorithm;
        }

        public abstract void Overlay(IGraph<IVertex> graph);
    }
}
