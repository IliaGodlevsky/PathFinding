using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using Visualization.Extensions;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualization : IVisualization
    {
        public CompositeVisualization(IGraph graph, params IVisualization[] visualizations)
        {
            this.graph = graph;
            this.visualizations = visualizations;
        }

        public void Visualize(IAlgorithm algorithm)
        {
            graph.RemoveAllColors();
            visualizations.ForEach(visualization => visualization.Visualize(algorithm));
        }

        private readonly IGraph graph;
        private readonly IVisualization[] visualizations;
    }
}