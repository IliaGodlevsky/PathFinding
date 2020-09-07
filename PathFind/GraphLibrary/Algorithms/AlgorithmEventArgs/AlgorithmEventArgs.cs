using GraphLibrary.Collection;
using System;

namespace GraphLibrary.AlgorithmArgs
{
    public class AlgorithmEventArgs : EventArgs
    {
        public AlgorithmEventArgs()
        {
            HasFoundPath = false;
        }

        public AlgorithmEventArgs(Graph graph)
        {
            HasFoundPath = graph?.End?.IsVisited == true;
        }

        public bool HasFoundPath { get; }
    }
}
