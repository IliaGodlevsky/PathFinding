using System;
using System.IO;

namespace GraphLib.Interface
{
    public interface IGraphSerializer
    {
        event Action<string> OnExceptionCaught;

        void SaveGraph(IGraph graph, Stream stream);

        IGraph LoadGraph(Stream stream);
    }
}
