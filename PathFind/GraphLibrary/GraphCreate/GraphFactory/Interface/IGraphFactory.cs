using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.GraphCreate.GraphFactory.Interface
{
    public interface IGraphFactory
    {
        Graph GetGraph(Func<IVertex> generator);
    }
}
