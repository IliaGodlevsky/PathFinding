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
            HasFoundPath = graph.End.IsVisited;
        }

        public bool HasFoundPath { get; }
    }
}
