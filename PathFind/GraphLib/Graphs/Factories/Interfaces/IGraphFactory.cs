using GraphLib.Graphs.Abstractions;

namespace GraphLib.Graphs.Factories.Interfaces
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(int[] dimensionSizes);
    }
}
