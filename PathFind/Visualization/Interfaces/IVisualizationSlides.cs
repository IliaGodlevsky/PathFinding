using Algorithm.Interfaces;

namespace Visualization.Interfaces
{
    internal interface IVisualizationSlides<TAdd>
    {
        void Add(IAlgorithm<IGraphPath> algorithm, TAdd add);

        void Remove(IAlgorithm<IGraphPath> algorithm);

        void Clear();
    }
}
