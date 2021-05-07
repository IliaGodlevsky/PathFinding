using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Сompanions
{
    public sealed class VisitedVertices
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
            return vertex.Neighbours.Where(IsNotObstacleAndNotVisited);
        }

        public void Clear()
        {
            visitedVertices.Clear();
        }

        private bool IsNotObstacleAndNotVisited(IVertex vertex)
        {
            return IsNotVisited(vertex) && !vertex.IsObstacle;
        }

        private readonly ConcurrentDictionary<ICoordinate, IVertex> visitedVertices;
    }
}