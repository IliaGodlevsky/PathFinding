using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface IGraphSerializationModule<TVertex>
        where TVertex : IVertex
    {
        void SaveGraph(IGraph<TVertex> graph);

        IGraph<TVertex> LoadGraph();
    }
}
