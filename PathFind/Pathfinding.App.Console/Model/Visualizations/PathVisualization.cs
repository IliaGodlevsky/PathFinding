using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages.DataMessages;
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

        private void ColorsRecieve(DataMessage<(ConsoleColor Path, ConsoleColor Crossed)> msg)
        {
            PathVertexColor = msg.Value.Path;
            CrossedPathVertexColor = msg.Value.Crossed;
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
            messenger.RegisterData<(ConsoleColor Path, ConsoleColor Crossed)>(this, Tokens.Path, ColorsRecieve);
            messenger.RegisterData<Vertex>(this, Tokens.Path, RemoveVertex);
        }

        private void RemoveVertex(DataMessage<Vertex> msg)
        {
            vertices.Remove(msg.Value);
        }
    }
}
