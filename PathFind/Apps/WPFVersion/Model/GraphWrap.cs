using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WPFVersion.Model
{
    internal sealed class GraphWrap : IGraph
    {
        private const string ParamsFormat = "Obstacle percent: {0} ({1}/{2})";
        private static readonly string[] DimensionNames = new[] { "Width", "Length" };

        private readonly IGraph graph;

        public IReadOnlyList<int> DimensionsSizes => graph.DimensionsSizes;

        public int Count => graph.Count;

        public GraphWrap(IGraph graph)
        {
            this.graph = graph;
        }

        public IVertex Get(ICoordinate coordinate)
        {
            return graph.Get(coordinate);
        }

        public override string ToString()
        {
            int obstacles = this.GetObstaclesCount();
            int obstaclesPercent = this.GetObstaclePercent();
            string Zip(string name, int size) => $"{name}: {size}";
            var zipped = DimensionNames.Zip(DimensionsSizes, Zip);
            string joined = string.Join("   ", zipped);
            string graphParams = string.Format(ParamsFormat,
                obstaclesPercent, obstacles, Count);
            return string.Join("   ", joined, graphParams);
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return graph.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}