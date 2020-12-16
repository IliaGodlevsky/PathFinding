using GraphLib.Graphs.Abstractions;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using System;

namespace GraphLib.Graphs.Serialization.Interfaces
{
    public interface IGraphSerializer
    {
        event Action<string> OnExceptionCaught;

        void SaveGraph(IGraph graph, string path);

        IGraph LoadGraph(string path,
            Func<VertexInfo, IVertex> vertexFactory);
    }
}
