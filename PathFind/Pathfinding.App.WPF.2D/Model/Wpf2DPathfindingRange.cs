using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using System.Diagnostics;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class Wpf2DPathfindingRange : VisualPathfindingRange<Vertex>, IGraphSubscription<Vertex>
    {
        private void SetPathfindingRange(object sender, MouseButtonEventArgs e)
        {
            IncludeInPathfindingRange((Vertex)e.Source);
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown += SetPathfindingRange;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseLeftButtonDown -= SetPathfindingRange;
            }
        }
    }
}
