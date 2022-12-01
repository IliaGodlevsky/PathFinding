using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class PathfindingRangeBuilderSubscription : IGraphSubscription<Vertex3D>
    {
        private readonly IPathfindingRangeBuilder<Vertex3D> rangeBuilder;

        public PathfindingRangeBuilderSubscription(IPathfindingRangeBuilder<Vertex3D> rangeBuilder)
        {
            this.rangeBuilder = rangeBuilder;
        }

        private void IncludeInRange(object sender, MouseButtonEventArgs e)
        {
            rangeBuilder.Include((Vertex3D)e.Source);
        }

        private void ExcludeFromRange(object sender, MouseButtonEventArgs e)
        {
            rangeBuilder.Exclude((Vertex3D)e.Source);
        }

        public void Subscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown += IncludeInRange;
                vertex.MouseRightButtonUp += ExcludeFromRange;
            }
        }

        public void Unsubscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown -= IncludeInRange;
                vertex.MouseRightButtonUp -= ExcludeFromRange;
            }
        }
    }
}
