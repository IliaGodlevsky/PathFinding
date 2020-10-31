using GraphLib.Graphs.Abstractions;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using System;

namespace GraphLib.GraphLib.Graphs.Serialization.Interface
{
    public interface IGraphSerializer
    {
        event Action<string> OnExceptionCaught;
        void SaveGraph(IGraph graph, string path);
        IGraph LoadGraph(string path, Func<VertexInfo, IVertex> vertexFactory);
    }
}
