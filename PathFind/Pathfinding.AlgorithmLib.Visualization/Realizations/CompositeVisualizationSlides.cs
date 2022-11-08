using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Visualization.Interfaces;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Visualization.Realizations
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
            return slides.GetEnumerator();
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            slides.ForEach(slide => slide.Remove(algorithm));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}