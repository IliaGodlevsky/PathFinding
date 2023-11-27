using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class GraphsPathfindingHistory
    {
        private readonly Dictionary<int, IGraph<Vertex>> graphs = new();
        private readonly Dictionary<int, GraphPathfindingHistory> histories = new();
        private readonly Dictionary<int, List<ICoordinate>> pathfindingRange = new();
        private readonly Dictionary<int, Stack<IReadOnlyList<int>>> smoothHistory = new();

        public IReadOnlyCollection<IGraph<Vertex>> Graphs => graphs.Values;

        public IReadOnlyCollection<int> Ids => graphs.Keys;

        public int Count => histories.Count;

        public Stack<IReadOnlyList<int>> GetSmoothHistory(int key)
        {
            return smoothHistory[key];
        }

        public GraphPathfindingHistory GetHistory(int key)
        {
            return histories[key];
        }

        public IGraph<Vertex> GetGraph(int key)
        {
            return graphs[key];
        }

        public List<ICoordinate> GetRange(int key)
        {
            return pathfindingRange[key];
        }

        public int Add(IGraph<Vertex> graph)
        {
            int key = graph.GetHashCode();
            graphs.Add(key, graph);
            if (!histories.ContainsKey(key))
            {
                histories.Add(key, new());
            }
            if (!pathfindingRange.ContainsKey(key))
            {
                pathfindingRange.Add(key, new());
            }
            if (!smoothHistory.ContainsKey(key))
            {
                smoothHistory.Add(key, new());
            }

            return key;
        }

        public void Add(int key, GraphPathfindingHistory history)
        {
            histories[key] = history;
        }

        public bool Remove(IGraph<Vertex> key)
        {
            return Remove(key.GetHashCode());
        }

        public bool Remove(int hashCode)
        {
            return graphs.Remove(hashCode)
                && pathfindingRange.Remove(hashCode)
                && histories.Remove(hashCode);
        }
    }
}
