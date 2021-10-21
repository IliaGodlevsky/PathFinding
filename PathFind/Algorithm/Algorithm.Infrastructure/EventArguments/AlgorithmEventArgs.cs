using GraphLib.Interfaces;
using System;

namespace Algorithm.Infrastructure.EventArguments
{
    /// <summary>
    /// Represents data for events associated with pathfinding
    /// </summary>
    public class AlgorithmEventArgs : EventArgs
    {
        /// <summary>
        /// A vertex, that has been just processed by pathfinding algorithm
        /// </summary>
        public IVertex Current { get; }

        public AlgorithmEventArgs(IVertex current)
        {
            Current = current;
        }
    }
}