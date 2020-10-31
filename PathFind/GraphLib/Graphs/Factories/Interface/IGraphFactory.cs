using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace GraphLib.Graphs.Factories.Interface
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(Func<IVertex> vertexFactory);
    }
}
