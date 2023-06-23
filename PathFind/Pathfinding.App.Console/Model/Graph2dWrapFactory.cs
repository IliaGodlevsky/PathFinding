using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Graph2DWrapFactory : IGraphFactory<Graph2D<Vertex>, Vertex>
    {
        private readonly IGraphFactory<Graph2D<Vertex>, Vertex> factory;

        public Graph2DWrapFactory()
        {
            this.factory = new Graph2DFactory<Vertex>();
        }

        public Graph2D<Vertex> CreateGraph(IReadOnlyCollection<Vertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new Graph2DWrap(factory.CreateGraph(vertices, dimensionSizes));
        }
    }
}
