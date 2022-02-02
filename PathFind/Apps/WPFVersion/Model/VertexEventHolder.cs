using GraphLib.Base;
using GraphLib.Infrastructure.EventArguments;
using GraphLib.Infrastructure.EventHandlers;
using GraphLib.Infrastructure.Interfaces;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Input;

namespace WPFVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder, IVertexEventHolder,
        INotifyVertexCostChanged, INotifyObstacleChanged
    {
        public event CostChangedEventHandler CostChanged;
        public event ObstacleChangedEventHandler ObstacleChanged;

        public VertexEventHolder(IVertexCostFactory costFactory) : base(costFactory)
        {

        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseWheelEventArgs args ? args.Delta > 0 ? 1 : -1 : default;
        }

        public override void ChangeVertexCost(object sender, EventArgs e)
        {
            base.ChangeVertexCost(sender, e);
            if (sender is IVertex vertex && !vertex.IsObstacle)
            {
                CostChanged?.Invoke(this, new CostChangedEventArgs(vertex));
            }
        }

        public override void Reverse(object sender, EventArgs e)
        {
            base.Reverse(sender, e);
            if (sender is IVertex vertex)
            {
                ObstacleChanged?.Invoke(this, new ObstacleChangedEventArgs(vertex));
            }
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseRightButtonDown += Reverse;
                vert.MouseWheel += ChangeVertexCost;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseRightButtonDown -= Reverse;
                vert.MouseWheel -= ChangeVertexCost;
            }
        }
    }
}
