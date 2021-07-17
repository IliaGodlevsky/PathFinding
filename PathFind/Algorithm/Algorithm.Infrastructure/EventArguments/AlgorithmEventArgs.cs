using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;

namespace Algorithm.Infrastructure.EventArguments
{
    /// <summary>
    /// Represents data for events associated with path finding
    /// </summary>
    public class AlgorithmEventArgs : EventArgs
    {
        public static new AlgorithmEventArgs Empty => new AlgorithmEventArgs();

        private AlgorithmEventArgs()
            : this(default)
        {

        }

        public AlgorithmEventArgs(int visitedVertices,
            IEndPoints endPoints = null, IVertex vertex = null)
        {
            Vertex = vertex ?? new NullVertex();
            IsEndPoint = endPoints?.IsEndPoint(Vertex) ?? false;
            VisitedVertices = visitedVertices;
        }

        public IVertex Vertex { get; }

        public int VisitedVertices { get; }

        public bool IsEndPoint { get; }
    }
}
