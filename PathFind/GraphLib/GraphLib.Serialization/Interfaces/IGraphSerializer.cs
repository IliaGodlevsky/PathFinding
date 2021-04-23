using System;
using GraphLib.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace GraphLib.Serialization.Interfaces
{
    public interface IGraphSerializer
    {
        event Action<Exception, string> OnExceptionCaught;

        void SaveGraph(IGraph graph, Stream stream);

        Task SaveGraphAsync(IGraph graph, Stream stream);

        IGraph LoadGraph(Stream stream);
    }
}
