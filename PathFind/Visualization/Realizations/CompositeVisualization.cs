using Algorithm.Interfaces;
using Commands.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;
using Visualization.Extensions;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualization : List<IExecutable<IAlgorithm<IGraphPath>>>, IExecutable<IAlgorithm<IGraphPath>>
    {
        private readonly IGraph graph;

        public CompositeVisualization(IGraph graph)
        {
            this.graph = graph;
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            graph.RemoveAllColors();
            ForEach(command => command.Execute(algorithm));
        }
    }
}