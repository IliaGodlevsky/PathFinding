using System;
using System.IO;
using System.Threading.Tasks;

namespace GraphLib.Interface
{
    public interface IGraphSerializer
    {
        event Action<Exception> OnExceptionCaught;

        Task SaveGraphAsync(IGraph graph, Stream stream);

        void SaveGraph(IGraph graph, Stream stream);

        IGraph LoadGraph(Stream stream);
    }
}
