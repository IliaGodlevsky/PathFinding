using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Infrastructure.EventArguments
{
    /// <summary>
    /// Represents data for events associated with path finding
    /// </summary>
    public class AlgorithmEventArgs : EventArgs
    {
        public AlgorithmEventArgs() 
            : this(default)
        {
            
        }

        public AlgorithmEventArgs(int visitedVertices,
            IEndPoints endPoints = null, IVertex vertex = null)
        {
            if (vertex != null)
            {
                IsEndPoint = endPoints?.IsEndPoint(vertex) ?? false;
            }

            Vertex = vertex ?? new DefaultVertex();
            VisitedVertices = visitedVertices;
        }

        /// <summary>
        /// Provides information about vertex that is being processed
        /// </summary>
        public IVertex Vertex { get; }

        public int VisitedVertices { get; }

        public bool IsEndPoint { get; }
    }
}
