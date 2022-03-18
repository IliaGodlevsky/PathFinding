using GraphLib.Interfaces;
using System;

namespace Algorithm.Infrastructure.EventArguments
{
    public class AlgorithmEventArgs : EventArgs
    {
        public IVertex Current { get; }

        public AlgorithmEventArgs(IVertex current)
        {
            Current = current;
        }
    }
}