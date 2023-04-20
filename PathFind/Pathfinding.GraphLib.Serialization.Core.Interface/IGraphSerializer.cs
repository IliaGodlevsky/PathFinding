using Pathfinding.GraphLib.Core.Interface;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface IGraphSerializer<TGraph, TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        void SerializeTo(TGraph graph, Stream stream);

        TGraph DeserializeFrom(Stream stream);
    }
}
