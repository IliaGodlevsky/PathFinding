using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using System.Linq;

namespace GraphViewModel.Visualizations
{
    internal sealed class CompositeVisualization : IVisualization
    {
        public CompositeVisualization(IGraph graph, IVisualization visualization, params IVisualization[] visualizations)
        {
            this.graph = graph;
            this.visualizations = visualizations.Prepend(visualization).ToArray();
        }

        public void Visualize(IAlgorithm algorithm)
        {
            graph.Refresh();
            visualizations.ForEach(visualization => visualization.Visualize(algorithm));
        }

        private readonly IGraph graph;
        private readonly IVisualization[] visualizations;
    }
}
