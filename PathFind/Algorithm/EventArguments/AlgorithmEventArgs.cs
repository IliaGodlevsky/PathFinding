using GraphLib.Interface;
using GraphLib.NullObjects;
using System;

namespace Algorithm.EventArguments
{
    /// <summary>
    /// Represents data for events associated with pathfinding
    /// </summary>
    public class AlgorithmEventArgs : EventArgs
    {
        public AlgorithmEventArgs()
        {
            Vertex = new DefaultVertex();
            Graph = new NullGraph();
        }

        public AlgorithmEventArgs(IGraph graph, IVertex vertex = null)
        {
            Vertex = vertex ?? new DefaultVertex();
            Graph = graph;
        }

        /// <summary>
        /// Provides information about vertex that is being processed
        /// </summary>
        public IVertex Vertex { get; private set; }

        /// <summary>
        /// Provides a information about graph where pathfinding occurs
        /// </summary>
        public IGraph Graph { get; private set; }
    }
}
