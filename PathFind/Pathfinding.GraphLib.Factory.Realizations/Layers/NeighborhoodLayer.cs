using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.GraphLib.Factory.Realizations.Layers
{
    public sealed class NeighborhoodLayer<TGraph, TVertex> : ILayer<TGraph, TVertex>
        where TGraph: IGraph<TVertex>
        where TVertex : IVertex
    {
        private INeighborhoodFactory NeighborhoodFactory { get; }

        public NeighborhoodLayer(INeighborhoodFactory factory)
        {
            this.NeighborhoodFactory = factory;
        }

        public void Overlay(TGraph graph)
        {
            foreach (var vertex in graph)
            {
                var neighborhood = NeighborhoodFactory.CreateNeighborhood(vertex.Position);
                vertex.Neighbours = neighborhood.GetNeighboursWithinGraph(graph);
            }
        }
    }
}
