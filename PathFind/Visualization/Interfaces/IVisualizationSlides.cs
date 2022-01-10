using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Visualization.Interfaces
{
    /// <summary>
    /// An interface for class, that contains vertices
    /// processed by algorithms
    /// </summary>
    internal interface IVisualizationSlides
    {
        /// <summary>
        /// Returns all vertices, that stored 
        /// by a particular <paramref name="algorithm"/>
        /// </summary>
        /// <param name="algorithm">An algorithm, that stores
        /// processed vertices</param>
        /// <returns></returns>
        IReadOnlyCollection<IVertex> GetVertices(IAlgorithm algorithm);
        /// <summary>
        /// Adds processed by <paramref name="algorithm"/> vertex
        /// to already processed by the algorithm vertices
        /// </summary>
        /// <param name="algorithm">An algorithm, with which
        /// the adding vertex will be associated</param>
        /// <param name="vertex">A vertex to add</param>
        void Add(IAlgorithm algorithm, IVertex vertex);
        /// <summary>
        /// Removes all vertices stored by all algorithms
        /// </summary>
        void Clear();
        /// <summary>
        /// Removes all vertices stored by a particular algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        void Remove(IAlgorithm algorithm);
    }
}
