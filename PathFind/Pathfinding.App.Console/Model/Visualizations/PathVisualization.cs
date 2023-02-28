using GalaSoft.MvvmLight.Messaging;
using Org.BouncyCastle.Asn1.Esf;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class PathVisualization : IPathVisualization<Vertex>, ICanRecieveMessage
    {
        private readonly IMessenger messenger;

        public ConsoleColor PathVertexColor { get; set; } = ConsoleColor.DarkYellow;

        public ConsoleColor CrossedPathVertexColor { get; set; } = ConsoleColor.DarkRed;

        public PathVisualization(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public bool IsVisualizedAsPath(Vertex visualizable)
        {
            return visualizable.Color.IsOneOf(PathVertexColor, CrossedPathVertexColor);
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
            }
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<AskForPathColorsMessage>(this, AskForColors);
            messenger.Register<PathColorMessage>(this, MessageTokens.PathColors, ColorsRecieve);
        }
    }
}
