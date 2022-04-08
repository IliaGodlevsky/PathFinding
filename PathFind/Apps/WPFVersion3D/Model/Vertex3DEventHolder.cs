using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EventHolder;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using WPFVersion3D.Messages.ActionMessages;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DEventHolder : BaseVertexEventHolder
    {
        private readonly IMessenger messenger;

        public Vertex3DEventHolder(IVertexCostFactory costFactory, IMessenger messenger) : base(costFactory)
        {
            this.messenger = messenger;
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        public override void Reverse(object sender, EventArgs e)
        {
            base.Reverse(sender, e);
            messenger.Send(new GraphChangedMessage());
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown += Reverse;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown -= Reverse;
            }
        }
    }
}
