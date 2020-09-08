using GraphLibrary.Graphs.Interface;
using System;

namespace GraphLibrary.EventArguments
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
