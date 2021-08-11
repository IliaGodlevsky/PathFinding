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

        private AlgorithmEventArgs() : this(default, new NullVertex())
        {

        }

        public AlgorithmEventArgs(int visited, IVertex current)
        {
            Current = current;
            Visited = visited;
        }

        public IVertex Current { get; }

        public int Visited { get; }
    }
}
