using Pathfinding.AlgorithmLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Visualization.Interfaces
{
    internal interface IVisualizationSlides<TAdd>
    {
        void Add(IAlgorithm<IGraphPath> algorithm, TAdd add);

        void Remove(IAlgorithm<IGraphPath> algorithm);

        void Clear();
    }
}
