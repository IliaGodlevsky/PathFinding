using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.VisitedVerticesImpl
{
    public sealed class VisitedVertices : IVisitedVertices
    {
        public VisitedVertices()
        {
            visitedVertices = new ConcurrentDictionary<ICoordinate, IVertex>();
        }

        public int Count => visitedVertices.Count;

        public void Add(IVertex vertex)
        {
            if (!vertex.IsNullObject())
            {
                visitedVertices[vertex.Position] = vertex;
            }
        }

        public bool IsNotVisited(IVertex vertex)
        {
            return !visitedVertices.TryGetValue(vertex.Position, out _);
        }

        public IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours
                .Where(IsNotVisited);
        }

        public void Clear()
        {
            visitedVertices.Clear();
        }

        private readonly ConcurrentDictionary<ICoordinate, IVertex> visitedVertices;
    }
}
