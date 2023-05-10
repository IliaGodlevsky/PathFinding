using Pathfinding.App.Console.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class TotalVertexVisualization : ITotalVisualization<Vertex>
    {
        private readonly IReadOnlyDictionary<VisualType, IVisual> visuals;

        public TotalVertexVisualization(IReadOnlyDictionary<VisualType, IVisual> visuals)
        {
            this.visuals = visuals;
        }

        public bool IsVisualizedAsPath(Vertex visualizable)
        {
            return TryGetOrNull(VisualType.Crossed)?.Contains(visualizable) == true
                || TryGetOrNull(VisualType.Path)?.Contains(visualizable) == true;
        }

        public bool IsVisualizedAsRange(Vertex visualizable)
        {
            return TryGetOrNull(VisualType.Source)?.Contains(visualizable) == true
                || TryGetOrNull(VisualType.Target)?.Contains(visualizable) == true
                || TryGetOrNull(VisualType.Transit)?.Contains(visualizable) == true;
        }

        public void VisualizeAsPath(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange())
            {
                if (!visualizable.IsVisualizedAsPath())
                {
                    TryGetOrNull(VisualType.Path)?.Visualize(visualizable);
                }
                else
                {
                    TryGetOrNull(VisualType.Crossed)?.Visualize(visualizable);
                }
            }
        }

        public void VisualizeAsSource(Vertex visualizable)
        {
            TryGetOrNull(VisualType.Source)?.Visualize(visualizable);
        }

        public void VisualizeAsTarget(Vertex visualizable)
        {
            TryGetOrNull(VisualType.Target)?.Visualize(visualizable);
        }

        public void VisualizeAsTransit(Vertex visualizable)
        {
            TryGetOrNull(VisualType.Transit)?.Visualize(visualizable);
        }

        public void VisualizeAsObstacle(Vertex vertex)
        {
            TryGetOrNull(VisualType.Obstacle)?.Visualize(vertex);
        }

        public void VisualizeAsRegular(Vertex vertex)
        {
            TryGetOrNull(VisualType.Regular)?.Visualize(vertex);
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            TryGetOrNull(VisualType.Visited)?.Visualize(vertex);
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            TryGetOrNull(VisualType.Enqueued)?.Visualize(vertex);
        }

        private IVisual TryGetOrNull(VisualType type)
        {
            return visuals.TryGetValue(type, out var visual) ? visual : null;
        }
    }
}