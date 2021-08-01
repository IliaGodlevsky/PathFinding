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

        public AlgorithmEventArgs(int visitedVertices, IVertex vertex = null)
        {
            CurrentVertex = vertex ?? new NullVertex();
            VisitedVertices = visitedVertices;
        }

        public IVertex CurrentVertex { get; }

        public int VisitedVertices { get; }
    }
}
