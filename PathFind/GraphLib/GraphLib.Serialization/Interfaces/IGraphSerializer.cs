using GraphLib.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace GraphLib.Serialization.Interfaces
{
    public interface IGraphSerializer
    {
        Task SaveGraphAsync(IGraph graph, Stream stream);

        void SaveGraph(IGraph graph, Stream stream);

        IGraph LoadGraph(Stream stream);
    }
}
