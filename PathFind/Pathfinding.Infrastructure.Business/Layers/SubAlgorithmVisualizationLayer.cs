using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Visualization;
using Pathfinding.Service.Interface.Extensions;
using Shared.Extensions;
using System;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Layers
{
    public sealed class SubAlgorithmVisualizationLayer : VisualizationLayer
    {
        public event Action OnStarted;
        public event Action OnVisited;
        public event Action OnFinished;

        public SubAlgorithmVisualizationLayer(RunVisualizationModel algorithm)
            : base(algorithm)
        {

        }

        public override void Overlay(IGraph<IVertex> graph)
        {
            OnStarted?.Invoke();
            foreach (var subAlgorithm in algorithm.Algorithms)
            {
                foreach (var vertex in subAlgorithm.Visited)
                {
                    OnVisited?.Invoke();
                    var current = (IPathfindingVisualizable)graph.Get(vertex.Visited);
                    current.VisualizeAsVisited();
                    vertex.Enqueued.Select(graph.Get)
                        .OfType<IPathfindingVisualizable>()
                        .ForEach(x => x.VisualizeAsEnqueued());
                }
                subAlgorithm.Path.Select(graph.Get)
                    .OfType<IPathVisualizable>()
                    .VisualizeAsPath();
            }
            OnFinished?.Invoke();
        }
    }
}
