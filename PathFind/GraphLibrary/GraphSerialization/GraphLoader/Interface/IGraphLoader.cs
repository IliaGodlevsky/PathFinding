using GraphLibrary.DTO;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.GraphSerialization.GraphLoader.Interface
{
    public interface IGraphLoader
    {
        event Action<string> OnBadLoad;
        Graph GetGraph(string path, Func<VertexDto, IVertex> generator);
    }
}
