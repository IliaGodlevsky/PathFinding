using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Wpf2DVertexReverseModule : IGraphSubscription<Vertex>
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRange range;

        private bool IsEditorModeStarted { get; set; } = false;

        public Wpf2DVertexReverseModule(IPathfindingRange range, IMessenger messenger)
        {
            this.range = range;
            this.messenger = messenger;
            messenger.Register<StartEditorModeMessage>(this, OnEditorModeStarted);
            messenger.Register<StopEditorModeMessage>(this, OnEditorModeStopped);
        }

        private void ReverseVertex(Vertex vertex)
        {
            if (vertex.IsObstacle)
            {
                vertex.IsObstacle = false;
                vertex.VisualizeAsRegular();
            }
            else
            {
                if (!range.IsInRange(vertex))
                {
                    vertex.IsObstacle = true;
                }
            }
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseEnter += EditorModeReverse;
                vertex.MouseRightButtonDown += ReverseVertex;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseEnter -= EditorModeReverse;
                vertex.MouseRightButtonDown -= ReverseVertex;
            }
        }

        private void ReverseVertex(object sender, MouseEventArgs e)
        {
            if (!IsEditorModeStarted)
            {
                ReverseVertex((Vertex)e.Source);
                GraphChanged();
            }
        }

        private void EditorModeReverse(object sender, MouseEventArgs e)
        {
            if (IsEditorModeStarted)
            {
                ReverseVertex((Vertex)e.Source);
                GraphChanged();
            }
        }

        private void GraphChanged()
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
