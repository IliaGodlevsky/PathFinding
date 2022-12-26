using Pathfinding.GraphLib.Core.Interface;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface IGraphSerializer<out TGraph, in TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        void SaveGraph(IGraph<IVertex> graph, Stream stream);

        TGraph LoadGraph(Stream stream);
    }
}
