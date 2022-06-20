using Algorithm.Interfaces;
using Commands.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;
using Visualization.Extensions;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualization : List<IExecutable<IAlgorithm>>, IExecutable<IAlgorithm>
    {
        private readonly IGraph graph;

        public CompositeVisualization(IGraph graph)
        {
            this.graph = graph;
        }

        public void Execute(IAlgorithm algorithm)
        {
            graph.RemoveAllColors();
            ForEach(command => command.Execute(algorithm));
        }
    }
}