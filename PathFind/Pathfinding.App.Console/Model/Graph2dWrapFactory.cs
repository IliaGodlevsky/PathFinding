using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Graph2DWrapFactory : IGraphFactory<Vertex>
    {
        private readonly IGraphFactory<Vertex> factory;

        public Graph2DWrapFactory(IGraphFactory<Vertex> factory)
        {
            this.factory = factory;
        }

        public IGraph<Vertex> CreateGraph(IReadOnlyCollection<Vertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new GraphWrap(factory.CreateGraph(vertices, dimensionSizes));
        }
    }
}
