using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using System.Diagnostics;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class Wpf3DPathfindingRange : VisualPathfindingRange<Vertex3D>, IGraphSubscription<Vertex3D>
    {
        private void SetPathfindingRange(object sender, MouseButtonEventArgs e)
        {
            IncludeInPathfindingRange((Vertex3D)e.Source);
        }

        private void MarkAsIntermediateToRemove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                MarkIntermediateVertexToReplace((Vertex3D)e.Source);
            }
        }

        public void Subscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown += SetPathfindingRange;
                vertex.MouseUp += MarkAsIntermediateToRemove;
            }
        }

        public void Unsubscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown -= SetPathfindingRange;
                vertex.MouseUp -= MarkAsIntermediateToRemove;
            }
        }
    }
}
