using GraphLib.Graphs.Abstractions;
using System;
using System.IO;

namespace GraphLib.Graphs.Serialization.Interfaces
{
    public interface IGraphSerializer
    {
        event Action<string> OnExceptionCaught;

        void SaveGraph(IGraph graph, Stream stream);

        IGraph LoadGraph(Stream stream);
    }
}
