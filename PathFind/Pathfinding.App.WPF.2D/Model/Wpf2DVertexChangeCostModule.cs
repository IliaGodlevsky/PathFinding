using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Subscriptions;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Wpf2DVertexChangeCostModule : ChangeVertexCostModule<Vertex>, IGraphSubscription<Vertex>
    {
        public Wpf2DVertexChangeCostModule(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

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
            var vertex = (Vertex)e.OriginalSource;
            if (!vertex.IsObstacle)
            {
                int delta = (e.Delta > 0 ? 1 : -1);
                int newCost = vertex.Cost.CurrentCost + delta;
                ChangeVertexCost(vertex, newCost);
            }
        }
    }
}
