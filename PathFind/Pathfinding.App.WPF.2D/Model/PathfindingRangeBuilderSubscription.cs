using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class PathfindingRangeBuilderSubscription : IGraphSubscription<Vertex>
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        public PathfindingRangeBuilderSubscription(IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.rangeBuilder = rangeBuilder;
        }

        private void IncludeInRange(object sender, MouseButtonEventArgs e)
        {
            rangeBuilder.Include((Vertex)e.Source);
        }

        private void ExcludeFromRange(object sender, MouseButtonEventArgs e)
        {
            rangeBuilder.Exclude((Vertex)e.Source);
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown += IncludeInRange;
                vertex.MouseRightButtonUp += ExcludeFromRange;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown -= IncludeInRange;
                vertex.MouseRightButtonUp -= ExcludeFromRange;
            }
        }
    }
}
