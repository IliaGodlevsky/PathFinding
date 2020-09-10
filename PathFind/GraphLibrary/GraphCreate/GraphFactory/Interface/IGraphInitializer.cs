using GraphLibrary.DTO;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.GraphCreate.GraphFactory.Interface
{
    public interface IGraphInitializer
    {
        Graph GetGraph(Func<VertexDto, IVertex> generator);
    }
}
