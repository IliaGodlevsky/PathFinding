using Algorithm.Interfaces;
using Commands.Extensions;
using Commands.Interfaces;
using GraphLib.Interfaces;
using Visualization.Extensions;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualization : IExecutable<IAlgorithm>
    {
        public CompositeVisualization(IGraph graph, params IExecutable<IAlgorithm>[] visualizations)
        {
            this.graph = graph;
            this.visualizations = visualizations;
        }

        public void Execute(IAlgorithm algorithm)
        {
            graph.RemoveAllColors();
            visualizations.ExecuteAll(algorithm);
        }

        private readonly IGraph graph;
        private readonly IExecutable<IAlgorithm>[] visualizations;
    }
}