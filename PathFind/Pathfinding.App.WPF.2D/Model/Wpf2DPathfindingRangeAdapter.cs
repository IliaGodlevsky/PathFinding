using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using System.Diagnostics;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class Wpf2DPathfindingRangeAdapter : PathfindingRangeAdapter<Vertex>, IGraphSubscription<Vertex>
    {
        public Wpf2DPathfindingRangeAdapter(IPathfindingRangeFactory rangeFactory)
            : base(rangeFactory)
        {
        }

        private void SetPathfindingRange(object sender, MouseButtonEventArgs e)
        {
            SetPathfindingRange((Vertex)e.OriginalSource);
        }

        private void MarkAsIntermediateToRemove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                MarkIntermediateVertexToReplace((Vertex)e.OriginalSource);
            }
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown += SetPathfindingRange;
                vertex.MouseUp += MarkAsIntermediateToRemove;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown -= SetPathfindingRange;
                vertex.MouseUp -= MarkAsIntermediateToRemove;
            }
        }
    }
}
