using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EventHolder;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Input;
using WPFVersion.Messages.ActionMessages;

namespace WPFVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder, IVertexEventHolder
    {
        private readonly IMessenger messenger;

        public VertexEventHolder(IVertexCostFactory costFactory, IMessenger messenger) : base(costFactory)
        {
            this.messenger = messenger;
        }

        public override void Reverse(object sender, EventArgs e)
        {            
            base.Reverse(sender, e);
            messenger.Send(new GraphChangedMessage());
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseWheelEventArgs args ? args.Delta > 0 ? 1 : -1 : default;
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
