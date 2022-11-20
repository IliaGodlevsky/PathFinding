using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Wpf2DVertexChangeCostModule : IGraphSubscription<Vertex>
    {
        private readonly IVertexCostFactory costFactory;

        public Wpf2DVertexChangeCostModule(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
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
            var vertex = (Vertex)e.Source;
            if (!vertex.IsObstacle)
            {
                int delta = (e.Delta > 0 ? 1 : -1);
                int newCost = vertex.Cost.CurrentCost + delta;
                vertex.Cost = costFactory.CreateCost(newCost);
            }
        }
    }
}
