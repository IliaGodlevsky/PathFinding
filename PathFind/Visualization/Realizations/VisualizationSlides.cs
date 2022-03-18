using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualizationSlides : IVisualizationSlides
    {
        private readonly IVisualizationSlides[] slides;

        public CompositeVisualizationSlides(params IVisualizationSlides[] slides)
        {
            this.slides = slides;
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            slides.ForEach(slide => slide.Add(algorithm, vertex));
        }

        public void Clear()
        {
            slides.ForEach(slide => slide.Clear());
        }

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return slides.SelectMany(slide => slide.GetVertices(algorithm)).ToArray();
        }

        public void Remove(IAlgorithm algorithm)
        {
            slides.ForEach(slide => slide.Remove(algorithm));
        }
    }
}