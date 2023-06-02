using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Visualizations.Visuals;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class TotalVertexVisualization : ITotalVisualization<Vertex>
    {
        private readonly IReadOnlyDictionary<VisualizedType, IVisualizedVertices> visuals;

        public TotalVertexVisualization(IReadOnlyDictionary<VisualizedType, IVisualizedVertices> visuals)
        {
            this.visuals = visuals;
        }

        public bool IsVisualizedAsPath(Vertex visualizable)
        {
            return GetOrDefault(VisualizedType.Crossed).Contains(visualizable)
                || GetOrDefault(VisualizedType.Path).Contains(visualizable);
        }

        public bool IsVisualizedAsRange(Vertex visualizable)
        {
            return GetOrDefault(VisualizedType.Source).Contains(visualizable)
                || GetOrDefault(VisualizedType.Target).Contains(visualizable)
                || GetOrDefault(VisualizedType.Transit).Contains(visualizable);
        }

        public void VisualizeAsPath(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange())
            {
                if (!visualizable.IsVisualizedAsPath())
                {
                    GetOrDefault(VisualizedType.Path).Visualize(visualizable);
                }
                else
                {
                    GetOrDefault(VisualizedType.Crossed).Visualize(visualizable);
                }
            }
        }

        public void VisualizeAsSource(Vertex visualizable)
        {
            GetOrDefault(VisualizedType.Source).Visualize(visualizable);
        }

        public void VisualizeAsTarget(Vertex visualizable)
        {
            GetOrDefault(VisualizedType.Target).Visualize(visualizable);
        }

        public void VisualizeAsTransit(Vertex visualizable)
        {
            GetOrDefault(VisualizedType.Transit).Visualize(visualizable);
        }

        public void VisualizeAsObstacle(Vertex vertex)
        {
            GetOrDefault(VisualizedType.Obstacle).Visualize(vertex);
        }

        public void VisualizeAsRegular(Vertex vertex)
        {
            GetOrDefault(VisualizedType.Regular).Visualize(vertex);
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            GetOrDefault(VisualizedType.Visited).Visualize(vertex);
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            GetOrDefault(VisualizedType.Enqueued).Visualize(vertex);
        }

        private IVisualizedVertices GetOrDefault(VisualizedType type)
        {
            return visuals.GetOrDefault(type, NullVisualizedVertices.Instance);
        }
    }
}