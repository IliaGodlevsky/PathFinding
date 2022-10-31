using Algorithm.Interfaces;
using Commands.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;
using Visualization.Extensions;

namespace Visualization.Realizations
{
    internal sealed class CompositeVisualization<TGraph, TVertex> : List<IExecutable<IAlgorithm<IGraphPath>>>, IExecutable<IAlgorithm<IGraphPath>>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly TGraph graph;

        public CompositeVisualization(TGraph graph)
        {
            this.graph = graph;
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            graph.RemoveAllColors<TGraph, TVertex>();
            ForEach(command => command.Execute(algorithm));
        }
    }
}