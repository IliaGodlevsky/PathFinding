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

        private bool IsRedactorModeStarted { get; set; } = false;

        public GraphEvents(IVertexCostFactory costFactory) : base(costFactory)
        {
            this.messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<StartRedactorModeMessage>(this, OnRedactorModeStarted);
            messenger.Register<StopRedactorModeMessage>(this, OnRedactorModeStopped);
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseWheelEventArgs args ? args.Delta > 0 ? 1 : -1 : default;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseEnter += ReverseVertexModeDependent;
                vert.MouseRightButtonDown += Reverse;
                vert.MouseRightButtonDown += OnGraphChanged;
                vert.MouseWheel += ChangeVertexCost;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseEnter -= ReverseVertexModeDependent;
                vert.MouseRightButtonDown -= Reverse;
                vert.MouseRightButtonDown -= OnGraphChanged;
                vert.MouseWheel -= ChangeVertexCost;
            }
        }

        private void ReverseVertexModeDependent(object sender, EventArgs e)
        {
            if (IsRedactorModeStarted)
            {
                base.Reverse(sender, e);
                OnGraphChanged(sender, e);
            }
        }

        protected override void Reverse(object sender, EventArgs e)
        {
            if (!IsRedactorModeStarted)
            {
                base.Reverse(sender, e);
            }
        }

        private void OnGraphChanged(object sender, EventArgs e)
        {
            messenger.Send(new GraphChangedMessage());
        }

        private void OnRedactorModeStarted(StartRedactorModeMessage message)
        {
            IsRedactorModeStarted = true;
        }

        private void OnRedactorModeStopped(StopRedactorModeMessage message)
        {
            IsRedactorModeStarted = false;
        }
    }
}
