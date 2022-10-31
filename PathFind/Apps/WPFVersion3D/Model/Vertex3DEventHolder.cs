using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EventHolder;
using GraphLib.Interfaces.Factories;
using System;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Messages.ActionMessages;

namespace WPFVersion3D.Model
{
    internal sealed class GraphEvents : BaseGraphEvents<Vertex3D>
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

        protected override void SubscribeToEvents(Vertex3D vertex)
        {
            vertex.MouseRightButtonDown += Reverse;
            vertex.MouseRightButtonDown += OnGraphChanged;
        }

        protected override void UnsubscribeFromEvents(Vertex3D vertex)
        {
            vertex.MouseRightButtonDown -= Reverse;
            vertex.MouseRightButtonDown -= OnGraphChanged;
        }

        private void OnGraphChanged(object sender, EventArgs e)
        {
            messenger.Send(new GraphChangedMessage());
        }
    }
}
