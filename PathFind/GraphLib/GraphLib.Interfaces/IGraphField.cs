using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// Reprsents a field for displaying graph
    /// </summary>
    public interface IGraphField
    {
        /// <summary>
        /// Addes a vertex to the graph field
        /// </summary>
        /// <param name="vertex"></param>
        void Add(IVertex vertex);

        /// <summary>
        /// Clears the graph field
        /// </summary>
        void Clear();

        /// <summary>
        /// A collection of vertices, that are in the field
        /// </summary>
        IReadOnlyCollection<IVertex> Vertices { get; }
    }
}
