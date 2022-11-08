using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Visualization.Extensions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Visualization.Realizations
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