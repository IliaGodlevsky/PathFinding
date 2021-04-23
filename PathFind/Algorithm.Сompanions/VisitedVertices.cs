using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using GraphLib.Interfaces;

namespace Algorithm.Сompanions
{
    public sealed class VisitedVertices
    {
        public VisitedVertices()
        {
            visitedVertices = new Dictionary<ICoordinate, IVertex>();
        }

        public int Count => visitedVertices.Count;

        public void Add(IVertex vertex)
        {
            if (!vertex.IsDefault())
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

        private readonly Dictionary<ICoordinate, IVertex> visitedVertices;
    }
}