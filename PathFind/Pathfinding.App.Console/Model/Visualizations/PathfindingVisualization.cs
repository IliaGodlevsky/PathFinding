using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.VisualizationLib.Core.Interface;
using System;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class PathfindingVisualization : IPathfindingVisualization<Vertex>, ICanRecieveMessage
    {
        private readonly IMessenger messenger;

        public ConsoleColor EnqueuedVertexColor { get; set; } = ConsoleColor.Blue;

        public ConsoleColor VisitedVertexColor { get; set; } = ConsoleColor.White;

        public PathfindingVisualization(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void VisualizeAsEnqueued(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange() && !visualizable.IsVisualizedAsPath())
            {
                visualizable.Color = EnqueuedVertexColor;
            }
        }

        public void VisualizeAsVisited(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange() && !visualizable.IsVisualizedAsPath())
            {
                visualizable.Color = VisitedVertexColor;
            }
        }

        private void AskForColorsMessage(AskForPathfindingColorsMessage msg)
        {
            var message = new PathfindingColorsMessage(EnqueuedVertexColor, VisitedVertexColor);
            messenger.Send(message, MessageTokens.PathfindingColors);
        }

        private void ColorsChanged(PathfindingColorsMessage msg)
        {
            EnqueuedVertexColor = msg.EnqueuColor;
            VisitedVertexColor = msg.VisitColor;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<PathfindingColorsMessage>(this, ColorsChanged);
            messenger.Register<AskForPathfindingColorsMessage>(this, AskForColorsMessage);
        }
    }
}
