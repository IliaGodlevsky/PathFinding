using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations
{
    public sealed class VisitedVertices : IVisitedVertices
    {
        public VisitedVertices()
        {
            visitedVertices = new Dictionary<ICoordinate, IVertex>();
        }

        public int Count => visitedVertices.Count;

        public void Add(IVertex vertex)
        {
            if (!vertex.IsNull())
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
            return vertex.Neighbours.Where(IsNotVisited);
        }

        public void Clear()
        {
            visitedVertices.Clear();
        }

        private readonly IDictionary<ICoordinate, IVertex> visitedVertices;
    }
}
