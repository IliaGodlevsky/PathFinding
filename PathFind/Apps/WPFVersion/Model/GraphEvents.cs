using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EventHolder;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Messages.ActionMessages;

namespace WPFVersion.Model
{
    internal sealed class GraphEvents : BaseGraphEvents<Vertex>
    {
        private readonly IMessenger messenger;

        private bool IsEditorModeStarted { get; set; } = false;

        public GraphEvents(IVertexCostFactory costFactory) : base(costFactory)
        {
            this.messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<StartEditorModeMessage>(this, OnEditorModeStarted);
            messenger.Register<StopEditorModeMessage>(this, OnEditorModeStopped);
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseWheelEventArgs args ? (args.Delta > 0 ? 1 : -1) : default;
        }

        protected override void SubscribeToEvents(Vertex vertex)
        {
            vertex.MouseEnter += EditorModeReverse;
            vertex.MouseRightButtonDown += Reverse;
            vertex.MouseWheel += ChangeVertexCost;
        }

        protected override void UnsubscribeFromEvents(Vertex vertex)
        {
            vertex.MouseEnter -= EditorModeReverse;
            vertex.MouseRightButtonDown -= Reverse;
            vertex.MouseWheel -= ChangeVertexCost;
        }

        private void EditorModeReverse(object sender, EventArgs e)
        {
            if (IsEditorModeStarted)
            {
                base.Reverse(sender, e);
                OnGraphChanged(sender, e);
            }
        }

        protected override void Reverse(object sender, EventArgs e)
        {
            if (!IsEditorModeStarted)
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
            IsEditorModeStarted = true;
        }

        private void OnEditorModeStopped(StopEditorModeMessage message)
        {
            IsEditorModeStarted = false;
        }
    }
}
