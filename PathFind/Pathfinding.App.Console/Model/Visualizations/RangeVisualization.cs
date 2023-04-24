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
    internal sealed class RangeVisualization : IRangeVisualization<Vertex>, ICanRecieveMessage
    {
        private readonly HashSet<Vertex> vertices = new();
        private readonly IMessenger messenger;

        public ConsoleColor SourceVertexColor { get; set; } = ConsoleColor.Magenta;

        public ConsoleColor TargetVertexColor { get; set; } = ConsoleColor.Red;

        public ConsoleColor TransitVertexColor { get; set; } = ConsoleColor.Green;

        public RangeVisualization(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public bool IsVisualizedAsRange(Vertex visualizable)
        {
            return vertices.Contains(visualizable);
        }

        public void VisualizeAsSource(Vertex visualizable)
        {
            vertices.Add(visualizable);
            visualizable.Color = SourceVertexColor;
        }

        public void VisualizeAsTarget(Vertex visualizable)
        {
            vertices.Add(visualizable);
            visualizable.Color = TargetVertexColor;
        }

        public void VisualizeAsTransit(Vertex visualizable)
        {
            vertices.Add(visualizable);
            visualizable.Color = TransitVertexColor;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<(ConsoleColor Source, ConsoleColor Target, ConsoleColor Transit)>(this, Tokens.Range, ColorsRecieved);
            messenger.RegisterData<Vertex>(this, Tokens.Range, RemoveVertex);
        }

        private void ColorsRecieved(DataMessage<(ConsoleColor Source, ConsoleColor Target, ConsoleColor Transit)> msg)
        {
            SourceVertexColor = msg.Value.Source;
            TransitVertexColor = msg.Value.Target;
            TargetVertexColor = msg.Value.Transit;
        }

        private void RemoveVertex(DataMessage<Vertex> msg)
        {
            vertices.Remove(msg.Value);
        }
    }
}
