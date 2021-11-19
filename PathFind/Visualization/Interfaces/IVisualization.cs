using Algorithm.Interfaces;

namespace Visualization.Interfaces
{
    /// <summary>
    /// An interface for classes, that visualize vertices,
    /// that were processed by algorithms
    /// </summary>
    internal interface IVisualization
    {
        /// <summary>
        /// Visualizes vertices that <paramref name="algorithm"/>
        /// has processed
        /// </summary>
        /// <param name="algorithm">An algorithm
        /// which vertices should be visualized</param>
        void Visualize(IAlgorithm algorithm);
    }
}
