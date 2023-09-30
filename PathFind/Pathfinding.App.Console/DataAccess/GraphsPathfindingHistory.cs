using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess
{
    internal class GraphsPathfindingHistory : IEnumerable<(Graph2D<Vertex> Graph, GraphPathfindingHistory History)>
    {
        private readonly Dictionary<Graph2D<Vertex>, GraphPathfindingHistory> history = new();

        public GraphPathfindingHistory GetFor(Graph2D<Vertex> key) => history[key];

        public IReadOnlyCollection<Graph2D<Vertex>> Graphs => history.Keys;

        public int Count => history.Count;

        public void Add(Graph2D<Vertex> key, GraphPathfindingHistory value) => history.Add(key, value);

        public void Add((Graph2D<Vertex> Graph, GraphPathfindingHistory History) note) => Add(note.Graph, note.History);

        public void Add(Graph2D<Vertex> key) => history.Add(key, new());

        public void Clear() => history.Clear();

        public bool Remove(Graph2D<Vertex> key) => history.Remove(key);

        public IEnumerator<(Graph2D<Vertex> Graph, GraphPathfindingHistory History)> GetEnumerator()
        {
            return history
                .Select(pair => (Graph: pair.Key, History: pair.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
