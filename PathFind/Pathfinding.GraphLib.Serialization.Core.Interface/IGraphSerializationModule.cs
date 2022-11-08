using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface IGraphSerializationModule<out TGraph, in TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        void SaveGraph(IGraph<IVertex> graph);

        TGraph LoadGraph();
    }
}
