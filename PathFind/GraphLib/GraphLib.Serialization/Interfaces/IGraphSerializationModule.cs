using GraphLib.Interfaces;

namespace GraphLib.Serialization.Interfaces
{
    public interface IGraphSerializationModule<out TGraph, in TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        void SaveGraph(IGraph<IVertex> graph);

        TGraph LoadGraph();
    }
}
