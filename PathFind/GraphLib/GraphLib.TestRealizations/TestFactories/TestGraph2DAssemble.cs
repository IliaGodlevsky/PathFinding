using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.Realizations.Neighbourhoods;
using GraphLib.TestRealizations.TestFactories.Matrix;
using GraphLib.TestRealizations.TestObjects;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestGraph2DAssemble : IGraphAssemble<Graph2D<TestVertex>, TestVertex>
    {
        public Graph2D<TestVertex> AssembleGraph(int obstaclePercent, IReadOnlyList<int> dimensionSizes)
        {
            int size = Constants.DimensionSizes2D.AggregateOrDefault((x, y) => x * y);
            var vertices = new TestVertex[size];
            for (int index = 0; index < size; index++)
            {
                var coordinates = Constants.DimensionSizes2D.ToCoordinates(index);
                var coordinate = new TestCoordinate(coordinates);
                var neighborhood = new MooreNeighborhood(coordinate);
                vertices[index] = new TestVertex(coordinate);
            }
            var graph = new Graph2D<TestVertex>(vertices, Constants.DimensionSizes2D);
            var matrices = new Matrices(new CostMatrix(graph), new ObstacleMatrix(graph));
            matrices.Overlay();
            FillNeighbourhood(graph);
            return graph;
        }

        private void FillNeighbourhood(Graph2D<TestVertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.Neighbours = new MooreNeighborhood(vertex.Position)
                    .GetNeighboursWithinGraph(graph);
            }
        }
    }
}
