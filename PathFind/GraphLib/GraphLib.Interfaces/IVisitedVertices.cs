using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that provides methods 
    /// and properties for storing vertices 
    /// that have been visited by pathfinding 
    /// algorithms
    /// </summary>
    public interface IVisitedVertices
    {
        /// <summary>
        /// Adds vertex to visited
        /// </summary>
        /// <param name="vertex"></param>
        void Add(IVertex vertex);

        /// <summary>
        /// Checks, whether vertex is visited or not
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        bool IsNotVisited(IVertex vertex);

        /// <summary>
        /// Gets vertex's unvisited neighbours
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex);

        void Clear();
    }
}
