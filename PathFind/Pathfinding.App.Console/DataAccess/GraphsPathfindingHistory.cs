using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess
{
    internal class GraphsPathfindingHistory : IEnumerable<(IGraph<Vertex> Graph, GraphPathfindingHistory History)>
    {
        private readonly Dictionary<int, IGraph<Vertex>> graphs = new();
        private readonly Dictionary<int, GraphPathfindingHistory> history = new();

        public IReadOnlyCollection<int> Keys => graphs.Keys;

        public GraphPathfindingHistory GetFor(IGraph<Vertex> key)
        {
            int id = key.GetHashCode();
            return history[id];
        }

        public GraphPathfindingHistory GetFor(int hashCode)
        {
            return history[hashCode];
        }

        public IReadOnlyCollection<IGraph<Vertex>> Graphs => graphs.Values;

        public int Count => history.Count;

        public void Add(IGraph<Vertex> key, GraphPathfindingHistory value)
        {
            int id = key.GetHashCode();
            history.Add(id, value);
            graphs.Add(id, key);
        }

        public void Add((IGraph<Vertex> Graph, GraphPathfindingHistory History) note) 
            => Add(note.Graph, note.History);

        public void Add(IGraph<Vertex> key) => Add(key, new());

        public void Clear() => history.Clear();

        public bool Remove(IGraph<Vertex> key)
        {
            return Remove(key.GetHashCode());
        }

        public bool Remove(int hashCode)
        {
            graphs.Remove(hashCode);
            return history.Remove(hashCode);
        }

        public IEnumerator<(IGraph<Vertex> Graph, GraphPathfindingHistory History)> GetEnumerator()
        {
            return graphs.Zip(history, (g, h) => (graphs[g.Key], history[g.Key]))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
