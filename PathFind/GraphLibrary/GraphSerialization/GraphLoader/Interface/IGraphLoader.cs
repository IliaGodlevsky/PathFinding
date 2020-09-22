using GraphLibrary.DTO;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.GraphSerialization.GraphLoader.Interface
{
    /// <summary>
    /// Provides methods for deserializion of graph
    /// </summary>
    public interface IGraphLoader
    {
        event Action<string> OnBadLoad;
        IGraph LoadGraph(string path, Func<VertexDto, IVertex> dtoConverter);
    }
}
