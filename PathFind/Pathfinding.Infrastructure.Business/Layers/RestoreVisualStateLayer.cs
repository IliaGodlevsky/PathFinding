using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Visualization;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class RestoreVisualStateLayer<T> : ILayer
        where T : IVertex, IGraphVisualizable
    {
        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (T vertex in graph)
            {
                vertex.RestoreDefaultVisualState();
            }
        }
    }
}
