using GraphLib.Interfaces;
using System;

namespace ConsoleVersion.EventArguments
{
    internal class NewGraphCreatedEventArgs : EventArgs
    {
        public NewGraphCreatedEventArgs(IGraph newGraph)
        {
            NewGraph = newGraph;
        }

        public IGraph NewGraph { get; }
    }
}
