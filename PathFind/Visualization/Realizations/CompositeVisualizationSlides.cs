using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualizationSlides<TVertex> : IVisualizationSlides<TVertex>, IEnumerable<IVisualizationSlides<TVertex>>
        where TVertex : IVertex, IVisualizable
    {
        private readonly List<IVisualizationSlides<TVertex>> slides;

        public CompositeVisualizationSlides()
        {
            slides = new List<IVisualizationSlides<TVertex>>();
        }

        public void Add(IAlgorithm<IGraphPath> algorithm, TVertex vertex)
        {
            slides.ForEach(slide => slide.Add(algorithm, vertex));
        }

        public void Add(IVisualizationSlides<TVertex> slide)
        {
            slides.Add(slide);
        }

        public void Clear()
        {
            slides.ForEach(slide => slide.Clear());
        }

        public IEnumerator<IVisualizationSlides<TVertex>> GetEnumerator()
        {
            return (IEnumerator<IVisualizationSlides<TVertex>>)slides.GetEnumerator();
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            slides.ForEach(slide => slide.Remove(algorithm));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return slides.GetEnumerator();
        }
    }
}