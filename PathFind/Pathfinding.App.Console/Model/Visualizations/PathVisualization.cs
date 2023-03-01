using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class PathVisualization : IPathVisualization<Vertex>, ICanRecieveMessage
    {
        private readonly HashSet<Vertex> vertices = new();
        private readonly IMessenger messenger;

        public ConsoleColor PathVertexColor { get; set; } = ConsoleColor.DarkYellow;

        public ConsoleColor CrossedPathVertexColor { get; set; } = ConsoleColor.DarkRed;

        public PathVisualization(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public bool IsVisualizedAsPath(Vertex visualizable)
        {
            return vertices.Contains(visualizable);
        }

        private void AskForColors(AskForPathColorsMessage msg)
        {
            var message = new PathColorMessage(PathVertexColor, CrossedPathVertexColor);
            messenger.Send(message, MessageTokens.PathColorsChangeItem);
        }

        private void ColorsRecieve(PathColorMessage msg)
        {
            PathVertexColor = msg.PathColor;
            CrossedPathVertexColor = msg.CrossedPathColor;
        }

        public void VisualizeAsPath(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange())
            {
                if (visualizable.IsVisualizedAsPath())
                {
                    visualizable.Color = CrossedPathVertexColor;
                }
                else
                {
                    visualizable.Color = PathVertexColor;
                }
                vertices.Add(visualizable);
            }
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<AskForPathColorsMessage>(this, AskForColors);
            messenger.Register<PathColorMessage>(this, MessageTokens.PathColors, ColorsRecieve);
            messenger.Register<RemoveFromVisualizedMessage>(this, MessageTokens.PathColors, RemoveVertex);
        }

        private void RemoveVertex(RemoveFromVisualizedMessage msg)
        {
            vertices.Remove(msg.Vertex);
        }
    }
}
