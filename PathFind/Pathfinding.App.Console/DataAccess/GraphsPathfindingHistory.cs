using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class GraphsPathfindingHistory
    {
        private sealed class RangeVertexEqualityComparer : IEqualityComparer<(int, ICoordinate)>
        {
            public bool Equals((int, ICoordinate) x, (int, ICoordinate) y)
            {
                return x.Item2.Equals(y.Item2);
            }

            public int GetHashCode((int, ICoordinate) obj)
            {
                return obj.Item2.GetHashCode();
            }
        }

        private readonly Dictionary<int, IGraph<Vertex>> graphs = new();
        private readonly Dictionary<int, GraphPathfindingHistory> histories = new();
        private readonly Dictionary<int, HashSet<(int, ICoordinate)>> pathfindingRanges = new();

        public IReadOnlyCollection<IGraph<Vertex>> Graphs => graphs.Values;

        public IReadOnlyCollection<int> Ids => graphs.Keys;

        public int Count => histories.Count;

        public IGraph<Vertex> GetGraph(int id)
        {
            return graphs[id];
        }

        public GraphPathfindingHistory GetHistory(int id)
        {
            return histories[id];
        }

        public HashSet<(int, ICoordinate)> GetRange(int id)
        {
            return pathfindingRanges[id];
        }

        public void Add(int id, IGraph<Vertex> graph)
        {
            graphs.Add(id, graph);
            histories[id] = new();
            pathfindingRanges[id] = new(new RangeVertexEqualityComparer());
        }

        public void Add(int id, GraphPathfindingHistory history)
        {
            histories[id] = history;
        }

        public void Clear() => histories.Clear();

        public bool Remove(IGraph<Vertex> graph)
        {
            var pair = graphs.FirstOrDefault(g => g.Value == graph);
            return Remove(pair.Key);
        }

        public bool Remove(int graphsId)
        {
            return graphs.Remove(graphsId)
                && pathfindingRanges.Remove(graphsId)
                && histories.Remove(graphsId);
        }

        public bool TryGetGraph(int id, out IGraph<Vertex> graph)
        {
            return graphs.TryGetValue(id, out graph);
        }

        public bool TryGetHistory(int id, out GraphPathfindingHistory history)
        {
            return histories.TryGetValue(id, out history);
        }
    }
}
