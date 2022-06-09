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

        private bool IsEditorModeEnabled { get; set; } = false;

        public GraphEvents(IVertexCostFactory costFactory) : base(costFactory)
        {
            this.messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<StartEditorModeMessage>(this, OnEditorModeStarted);
            messenger.Register<StopEditorModeMessage>(this, OnEditorModeStopped);
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseWheelEventArgs args ? args.Delta > 0 ? 1 : -1 : default;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseEnter += EditorModeReverse;
                vert.MouseRightButtonDown += Reverse;
                vert.MouseWheel += ChangeVertexCost;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseEnter -= EditorModeReverse;
                vert.MouseRightButtonDown -= Reverse;
                vert.MouseWheel -= ChangeVertexCost;
            }
        }

        private void EditorModeReverse(object sender, EventArgs e)
        {
            if (IsEditorModeEnabled)
            {
                base.Reverse(sender, e);
                OnGraphChanged(sender, e);
            }
        }

        protected override void Reverse(object sender, EventArgs e)
        {
            if (!IsEditorModeEnabled)
            {
                base.Reverse(sender, e);
                OnGraphChanged(sender, e);
            }
        }

        private void OnGraphChanged(object sender, EventArgs e)
        {
            messenger.Send(new GraphChangedMessage());
        }

        private void OnEditorModeStarted(StartEditorModeMessage message)
        {
            IsEditorModeEnabled = true;
        }

        private void OnEditorModeStopped(StopEditorModeMessage message)
        {
            IsEditorModeEnabled = false;
        }
    }
}
