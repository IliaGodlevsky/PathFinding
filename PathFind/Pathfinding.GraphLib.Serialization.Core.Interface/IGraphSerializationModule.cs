using Pathfinding.GraphLib.Core.Interface;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface IGraphSerializationModule<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        void SaveGraph(TGraph graph);

        TGraph LoadGraph();
    }
}
