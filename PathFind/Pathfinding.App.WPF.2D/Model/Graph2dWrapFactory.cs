using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Graph2dWrapFactory : IGraphFactory<Graph2D<Vertex>, Vertex>
    {
        private readonly IGraphFactory<Graph2D<Vertex>, Vertex> factory;

        public Graph2dWrapFactory(IGraphFactory<Graph2D<Vertex>, Vertex> factory)
        {
            this.factory = factory;
        }

        public Graph2D<Vertex> CreateGraph(IReadOnlyCollection<Vertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new Graph2dWrap(factory.CreateGraph(vertices, dimensionSizes));
        }
    }
}
