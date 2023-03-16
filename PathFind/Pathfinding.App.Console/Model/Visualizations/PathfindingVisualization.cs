using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class PathfindingVisualization : IPathfindingVisualization<Vertex>, ICanRecieveMessage
    {
        private readonly HashSet<Vertex> vertices = new();
        private readonly IMessenger messenger;

        public ConsoleColor EnqueuedVertexColor { get; set; } = ConsoleColor.Blue;

        public ConsoleColor VisitedVertexColor { get; set; } = ConsoleColor.White;

        public PathfindingVisualization(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        private void VisualizeVertex(Vertex vertex, ConsoleColor color)
        {
            if (!vertex.IsVisualizedAsRange() && !vertex.IsVisualizedAsPath())
            {
                vertex.Color = color;
            }
        }

        public void VisualizeAsEnqueued(Vertex visualizable)
        {
            VisualizeVertex(visualizable, EnqueuedVertexColor);
        }

        public void VisualizeAsVisited(Vertex visualizable)
        {
            VisualizeVertex(visualizable, VisitedVertexColor);
        }

        private void ColorsChanged(DataMessage<ConsoleColor[]> msg)
        {
            EnqueuedVertexColor = msg.Value.FirstOrDefault();
            VisitedVertexColor = msg.Value.LastOrDefault();
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<ConsoleColor[]>(this, Tokens.Pathfinding, ColorsChanged);
        }
    }
}
