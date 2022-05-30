using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EventHolder;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Messages.ActionMessages;

namespace WPFVersion.Model
{
    internal sealed class GraphEvents : BaseGraphEvents
    {
        private readonly IMessenger messenger;

        public GraphEvents(IVertexCostFactory costFactory) : base(costFactory)
        {
            this.messenger = DI.Container.Resolve<IMessenger>();
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
                vert.MouseRightButtonDown += OnGraphChanged;
                vert.MouseWheel += ChangeVertexCost;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseRightButtonDown -= Reverse;
                vert.MouseRightButtonDown -= OnGraphChanged;
                vert.MouseWheel -= ChangeVertexCost;
            }
        }

        private void OnGraphChanged(object sender, EventArgs e)
        {
            messenger.Send(new GraphChangedMessage());
        }
    }
}
