using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace GraphLib.Graphs.Factories.Abstractions
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(Func<IVertex> vertexFactory);
    }
}
