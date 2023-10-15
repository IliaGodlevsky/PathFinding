using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess
{
    internal class GraphsPathfindingHistory : IEnumerable<(IGraph<Vertex> Graph, GraphPathfindingHistory History)>
    {
        private readonly Dictionary<IGraph<Vertex>, GraphPathfindingHistory> history = new();

        public GraphPathfindingHistory GetFor(IGraph<Vertex> key) => history[key];

        public IReadOnlyCollection<IGraph<Vertex>> Graphs => history.Keys;

        public int Count => history.Count;

        public void Add(IGraph<Vertex> key, GraphPathfindingHistory value) => history.Add(key, value);

        public void Add((IGraph<Vertex> Graph, GraphPathfindingHistory History) note) => Add(note.Graph, note.History);

        public void Add(IGraph<Vertex> key) => history.Add(key, new());

        public void Clear() => history.Clear();

        public bool Remove(IGraph<Vertex> key) => history.Remove(key);

        public IEnumerator<(IGraph<Vertex> Graph, GraphPathfindingHistory History)> GetEnumerator()
        {
            return history
                .Select(pair => (Graph: pair.Key, History: pair.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
