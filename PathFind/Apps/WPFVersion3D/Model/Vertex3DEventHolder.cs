using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EventHolder;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Messages.ActionMessages;

namespace WPFVersion3D.Model
{
    internal sealed class GraphEvents : BaseGraphEvents
    {
        private readonly IMessenger messenger;

        public GraphEvents(IVertexCostFactory costFactory, IMessenger messenger) : base(costFactory)
        {
            this.messenger = DI.Container.Resolve<IMessenger>();
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown += Reverse;
                vertex3D.MouseRightButtonDown += OnGraphChanged;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown -= Reverse;
                vertex3D.MouseRightButtonDown -= OnGraphChanged;
            }
        }

        private void OnGraphChanged(object sender, EventArgs e)
        {
            messenger.Send(new GraphChangedMessage());
        }
    }
}
