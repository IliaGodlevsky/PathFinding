using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using System.Linq;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers
{
    public sealed class NeighborhoodLayer : ILayer<Graph2D<TestVertex>, TestVertex>
    {
        public void Overlay(Graph2D<TestVertex> graph)
        {
            var factory = new MooreNeighborhoodFactory();
            foreach (var vertex in graph)
            {
                var neighborhood = factory.CreateNeighborhood(vertex.Position);
                vertex.Neighbours = neighborhood.GetNeighboursWithinGraph(graph).ToList();
            }
        }
    }
}
