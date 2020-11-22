using GraphLib.Graphs.Abstractions;
using System;

namespace Algorithm.EventArguments
{
    public class AlgorithmEventArgs : EventArgs
    {
        public AlgorithmEventArgs()
        {
            HasFoundPath = false;
        }

        public AlgorithmEventArgs(IGraph graph)
        {
            HasFoundPath = graph.End.IsVisited && !graph.End.IsDefault;
        }

        public bool HasFoundPath { get; private set; }
    }
}
