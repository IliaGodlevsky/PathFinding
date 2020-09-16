using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.GraphCreate.GraphFactory.Interface
{
    public interface IGraphFactory
    {
        IGraph GetGraph(Func<IVertex> generator);
    }
}
