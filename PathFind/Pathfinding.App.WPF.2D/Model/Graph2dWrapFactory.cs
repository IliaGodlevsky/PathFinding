using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Graph2dWrapFactory : IGraphFactory<Vertex>
    {
        private readonly IGraphFactory<Vertex> factory;

        public Graph2dWrapFactory(IGraphFactory<Vertex> factory)
        {
            this.factory = factory;
        }

        public IGraph<Vertex> CreateGraph(IReadOnlyCollection<Vertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new Graph2dWrap(factory.CreateGraph(vertices, dimensionSizes));
        }
    }
}
