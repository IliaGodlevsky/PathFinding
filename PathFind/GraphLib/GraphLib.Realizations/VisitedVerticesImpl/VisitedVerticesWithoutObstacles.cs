using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.VisitedVerticesImpl
{
    public sealed class VisitedVerticesWithoutObstacles : IVisitedVertices
    {
        public VisitedVerticesWithoutObstacles(IVisitedVertices visitedVertices)
        {
            this.visitedVertices = visitedVertices;
        }

        public int Count => visitedVertices.Count;

        public void Add(IVertex vertex)
        {
            visitedVertices.Add(vertex);
        }

        public bool IsNotVisited(IVertex vertex)
        {
            return visitedVertices.IsNotVisited(vertex);
        }

        public IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return visitedVertices
                .GetUnvisitedNeighbours(vertex)
                .Where(IsNotObstacle);
        }

        public void Clear()
        {
            visitedVertices.Clear();
        }

        private bool IsNotObstacle(IVertex vertex)
        {
            return !vertex.IsObstacle;
        }

        private readonly IVisitedVertices visitedVertices;
    }
}
