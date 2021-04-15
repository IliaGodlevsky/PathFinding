using GraphLib.Interfaces;
using System.IO;

namespace GraphLib.Serialization.Interfaces
{
    public interface IGraphSerializer
    {
        void SaveGraph(IGraph graph, Stream stream);

        IGraph LoadGraph(Stream stream);
    }
}
