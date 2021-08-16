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

        private AlgorithmEventArgs() : this(new NullVertex())
        {

        }

        public AlgorithmEventArgs(IVertex current)
        {
            Current = current;
        }

        public IVertex Current { get; }
    }
}
