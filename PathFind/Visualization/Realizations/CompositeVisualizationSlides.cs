using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualizationSlides : IVisualizationSlides<IVertex>, IEnumerable<IVisualizationSlides<IVertex>>
    {
        private readonly List<IVisualizationSlides<IVertex>> slides;

        public CompositeVisualizationSlides()
        {
            slides = new List<IVisualizationSlides<IVertex>>();
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            slides.ForEach(slide => slide.Add(algorithm, vertex));
        }

        public void Add(IVisualizationSlides<IVertex> slide)
        {
            slides.Add(slide);
        }

        public void Clear()
        {
            slides.ForEach(slide => slide.Clear());
        }

        public IEnumerator<IVisualizationSlides<IVertex>> GetEnumerator()
        {
            return (IEnumerator<IVisualizationSlides<IVertex>>)slides.GetEnumerator();
        }

        public void Remove(IAlgorithm algorithm)
        {
            slides.ForEach(slide => slide.Remove(algorithm));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return slides.GetEnumerator();
        }
    }
}