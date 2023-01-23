using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using System.Linq;

namespace Pathfinding.GraphLib.Factory.Realizations.Layers
{
    public sealed class NeighborhoodLayer<TGraph, TVertex> : ILayer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly INeighborhoodFactory factory;

        public NeighborhoodLayer(INeighborhoodFactory factory)
        {
            this.factory = factory;
        }

        public void Overlay(TGraph graph)
        {
            foreach (var vertex in graph)
            {
                var neighborhood = factory.CreateNeighborhood(vertex.Position);
                vertex.Neighbours = neighborhood.GetNeighboursWithinGraph(graph).ToList();
            }
        }
    }
}
