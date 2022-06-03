using Algorithm.Interfaces;

namespace Visualization.Interfaces
{
    internal interface IVisualizationSlides<TAdd>
    {
        void Add(IAlgorithm algorithm, TAdd add);

        void Remove(IAlgorithm algorithm);

        void Clear();
    }
}
