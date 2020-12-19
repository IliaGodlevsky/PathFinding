using Common;
using Common.Extensions;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.ObjectActivator;

namespace GraphLib.Graphs.Factories
{
    public sealed class GraphFactory<TGraph> : IGraphFactory
        where TGraph : IGraph
    {
        public event Action<string> OnExceptionCaught;

        public GraphFactory(int obstacleChance, params int[] dimensionSizes)
        {
            this.obstacleChance = obstacleChance;
            this.dimensionSizes = dimensionSizes.ToArray();
        }

        static GraphFactory()
        {
            rand = new Random();
            var ctor = typeof(TGraph).GetConstructor(typeof(int[]));
            RegisterConstructor<IGraph>(ctor);
        }

        public IGraph CreateGraph(Func<IVertex> vertexCreateMethod,
            Func<IEnumerable<int>, ICoordinate> coordinateCreateMethod)
        {
            try
            {
                var activator = (Activator<IGraph>)GetConstructor(typeof(TGraph));
                var graph = activator(dimensionSizes);

                for (int index = 0; index < graph.Size; index++)
                {
                    var coordinates = ToCoordinates(index, dimensionSizes);
                    var coordinate = coordinateCreateMethod?.Invoke(coordinates);

                    graph[coordinate] = vertexCreateMethod?.Invoke();

                    graph[coordinate].Cost = rand.GetRandomValueCost();
                    if (rand.IsObstacleChance(obstacleChance))
                    {
                        graph[coordinate].MarkAsObstacle();
                    }

                    graph[coordinate].Position = coordinate;
                }

                graph.ConnectVertices();

                return graph;
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new NullGraph();
            }
        }

        private IEnumerable<int> ToCoordinates(int index,
                 params int[] dimensions)
        {
            if (index >= dimensions.Aggregate((x, y) => x * y) || index < 0)
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }

            for (int i = 0; i < dimensions.Length; i++)
            {
                int coordinate = index % dimensions[i];
                index /= dimensions[i];

                yield return coordinate;
            }
        }

        private readonly int obstacleChance;
        private readonly int[] dimensionSizes;

        private static readonly Random rand;
    }
}
