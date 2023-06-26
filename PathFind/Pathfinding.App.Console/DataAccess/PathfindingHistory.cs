using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess
{
    internal class PathfindingHistory : IEnumerable<(Graph2D<Vertex> Graph, GraphPathfindingHistory History)>
    {
        private readonly Dictionary<Graph2D<Vertex>, GraphPathfindingHistory> history = new();

        public virtual GraphPathfindingHistory GetFor(Graph2D<Vertex> key)
        {
            return history[key];
        }

        public virtual IReadOnlyCollection<Graph2D<Vertex>> Graphs => history.Keys;

        public virtual int Count => history.Count;

        public virtual void Add(Graph2D<Vertex> key, GraphPathfindingHistory value)
        {
            history.Add(key, value);
        }

        public virtual void Add((Graph2D<Vertex> Graph, GraphPathfindingHistory History) note)
        {
            history.Add(note.Graph, note.History);
        }

        public virtual void Clear()
        {
            history.Clear();
        }

        public virtual bool Remove(Graph2D<Vertex> key)
        {
            return history.Remove(key);
        }

        public virtual IEnumerator<(Graph2D<Vertex> Graph, GraphPathfindingHistory History)> GetEnumerator()
        {
            return history
                .Select(pair => (Graph: pair.Key, History: pair.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
