using GraphLibrary.DTO;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.GraphCreate.GraphFactory.Interface
{
    public interface IGraphInitializer
    {
        IGraph GetGraph(Func<VertexDto, IVertex> generator);
    }
}
