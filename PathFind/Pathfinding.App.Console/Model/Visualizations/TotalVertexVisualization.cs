using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Visualizations.Visuals;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
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
            return GetOrDefault(VisualType.Crossed).Contains(visualizable) == true
                || GetOrDefault(VisualType.Path).Contains(visualizable) == true;
        }

        public bool IsVisualizedAsRange(Vertex visualizable)
        {
            return GetOrDefault(VisualType.Source).Contains(visualizable) == true
                || GetOrDefault(VisualType.Target).Contains(visualizable) == true
                || GetOrDefault(VisualType.Transit).Contains(visualizable) == true;
        }

        public void VisualizeAsPath(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange())
            {
                if (!visualizable.IsVisualizedAsPath())
                {
                    GetOrDefault(VisualType.Path).Visualize(visualizable);
                }
                else
                {
                    GetOrDefault(VisualType.Crossed).Visualize(visualizable);
                }
            }
        }

        public void VisualizeAsSource(Vertex visualizable)
        {
            GetOrDefault(VisualType.Source).Visualize(visualizable);
        }

        public void VisualizeAsTarget(Vertex visualizable)
        {
            GetOrDefault(VisualType.Target).Visualize(visualizable);
        }

        public void VisualizeAsTransit(Vertex visualizable)
        {
            GetOrDefault(VisualType.Transit).Visualize(visualizable);
        }

        public void VisualizeAsObstacle(Vertex vertex)
        {
            GetOrDefault(VisualType.Obstacle).Visualize(vertex);
        }

        public void VisualizeAsRegular(Vertex vertex)
        {
            GetOrDefault(VisualType.Regular).Visualize(vertex);
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            GetOrDefault(VisualType.Visited).Visualize(vertex);
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            GetOrDefault(VisualType.Enqueued).Visualize(vertex);
        }

        private IVisual GetOrDefault(VisualType type)
        {
            return visuals.GetOrDefault(type, NullVisual.Instance);
        }
    }
}