using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace GraphLib.Graphs.Factories.Interface.Abstractions
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(Func<IVertex> vertexFactory);
    }
}
