using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Extensions;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class VertexChangeCostSubscription : IGraphSubscription<Vertex>
    {
        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseWheel += ChangeVertexCost;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseWheel -= ChangeVertexCost;
            }
        }

        private void ChangeVertexCost(object sender, MouseWheelEventArgs e)
        {
            var vertex = (Vertex)e.Source;
            if (!vertex.IsObstacle)
            {
                var range = vertex.Cost.CostRange;
                int delta = (e.Delta > 0 ? 1 : -1);
                int newCost = range.ReturnInRange(vertex.Cost.CurrentCost + delta);
                vertex.Cost = vertex.Cost.SetCost(newCost);
            }
        }
    }
}
