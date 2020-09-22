using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.GraphCreating.Interface
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(Func<IVertex> generator);
    }
}
