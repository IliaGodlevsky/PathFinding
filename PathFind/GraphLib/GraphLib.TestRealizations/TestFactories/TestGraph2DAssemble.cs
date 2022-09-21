using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.Realizations.Neighbourhoods;
using GraphLib.TestRealizations.TestFactories.Matrix;
using GraphLib.TestRealizations.TestObjects;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestGraph2DAssemble : IGraphAssemble
    {
        public IGraph AssembleGraph(int obstaclePercent, IReadOnlyList<int> dimensionSizes)
        {
            int size = Constants.DimensionSizes2D.AggregateOrDefault((x, y) => x * y);
            var vertices = new TestVertex[size];
            for (int index = 0; index < size; index++)
            {
                var coordinates = Constants.DimensionSizes2D.ToCoordinates(index);
                var coordinate = new TestCoordinate(coordinates);
                var neighborhood = new MooreNeighborhood(coordinate);
                vertices[index] = new TestVertex(neighborhood, coordinate);
            }
            var graph = new Graph2D(vertices, Constants.DimensionSizes2D);
            var matrices = new Matrices(new CostMatrix(graph), new ObstacleMatrix(graph));
            matrices.Overlay();
            return graph;
        }
    }
}
