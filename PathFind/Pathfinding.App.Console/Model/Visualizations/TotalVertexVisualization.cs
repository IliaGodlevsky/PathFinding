using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class TotalVertexVisualization : ITotalVisualization<Vertex>, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingVisualization<Vertex> pathfindingVisualization;
        private readonly IRangeVisualization<Vertex> rangeVisualization;
        private readonly IPathVisualization<Vertex> pathVisualization;

        public ConsoleColor RegularVertexColor { get; set; } = ConsoleColor.DarkGray;

        public ConsoleColor ObstacleVertexColor { get; set; } = ConsoleColor.Black;

        public TotalVertexVisualization(IMessenger messenger,
            IPathfindingVisualization<Vertex> pathfindingVisualization = null,
            IRangeVisualization<Vertex> rangeVisualization = null,
            IPathVisualization<Vertex> pathVisualization = null)
        {
            this.messenger = messenger;
            this.pathfindingVisualization = pathfindingVisualization;
            this.rangeVisualization = rangeVisualization;
            this.pathVisualization = pathVisualization;
        }

        public void VisualizeAsObstacle(Vertex vertex)
        {
            vertex.Color = ObstacleVertexColor;
            messenger.SendData(vertex, Tokens.Range, Tokens.Path);
        }

        public void VisualizeAsRegular(Vertex vertex)
        {
            vertex.Color = RegularVertexColor;
            messenger.SendData(vertex, Tokens.Range, Tokens.Path);
        }

        public bool IsVisualizedAsPath(Vertex vertex) => pathVisualization?.IsVisualizedAsPath(vertex) == true;

        public bool IsVisualizedAsRange(Vertex vertex) => rangeVisualization?.IsVisualizedAsRange(vertex) == true;

        public void VisualizeAsTarget(Vertex vertex) => rangeVisualization?.VisualizeAsTarget(vertex);

        public void VisualizeAsSource(Vertex vertex) => rangeVisualization?.VisualizeAsSource(vertex);

        public void VisualizeAsTransit(Vertex vertex) => rangeVisualization?.VisualizeAsTransit(vertex);

        public void VisualizeAsPath(Vertex vertex) => pathVisualization?.VisualizeAsPath(vertex);

        public void VisualizeAsVisited(Vertex vertex) => pathfindingVisualization?.VisualizeAsVisited(vertex);

        public void VisualizeAsEnqueued(Vertex vertex) => pathfindingVisualization?.VisualizeAsEnqueued(vertex);

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<ConsoleColor[]>(this, Tokens.Graph, ColorsRecieved);
        }

        private void ColorsRecieved(DataMessage<ConsoleColor[]> msg)
        {
            RegularVertexColor = msg.Value.FirstOrDefault();
            ObstacleVertexColor = msg.Value.LastOrDefault();
        }
    }
}