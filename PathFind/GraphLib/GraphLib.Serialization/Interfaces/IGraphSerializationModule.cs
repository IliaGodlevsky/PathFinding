using GraphLib.Interfaces;

namespace GraphLib.Serialization.Interfaces
{
    public interface IGraphSerializationModule
    {
        void SaveGraph(IGraph graph);

        IGraph LoadGraph();
    }
}
