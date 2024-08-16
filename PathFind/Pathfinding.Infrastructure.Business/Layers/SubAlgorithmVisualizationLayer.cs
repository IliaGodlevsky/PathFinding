using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Visualization;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Shared.Extensions;
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
                foreach (var (Visited, Enqueued) in subAlgorithm.Visited)
                {
                    OnVisited?.Invoke();
                    var current = (IPathfindingVisualizable)graph.Get(Visited);
                    current.VisualizeAsVisited();
                    Enqueued.Select(graph.Get)
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
