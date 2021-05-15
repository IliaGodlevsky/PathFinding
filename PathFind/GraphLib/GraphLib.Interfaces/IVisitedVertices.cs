using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IVisitedVertices
    {
        int Count { get; }

        void Add(IVertex vertex);

        bool IsNotVisited(IVertex vertex);

        IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex);

        void Clear();
    }
}
