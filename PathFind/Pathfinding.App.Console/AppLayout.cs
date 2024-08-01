using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using System;
using System.Drawing;

namespace Pathfinding.App.Console
{
    internal sealed class AppLayout : ICanReceiveMessage
    {
        public const int HeightOfAbscissaView = 2;
        public const int HeightOfGraphParametresView = 1;

        public static readonly int WidthOfOrdinateView
            = (Constants.GraphLengthValueRange.UpperValueOfRange - 1).GetDigitsNumber() + 1;
        public static readonly int YCoordinatePadding = WidthOfOrdinateView - 1;

        public static readonly Point GraphFieldPosition
            = new(WidthOfOrdinateView, HeightOfAbscissaView + HeightOfGraphParametresView);

        private static int CurrentMaxValueOfRange;
        private static Point StatisticsPosition = Point.Empty;

        private static IGraph<Vertex> Graph { get; set; } = Graph<Vertex>.Empty;

        public static int LateralDistanceBetweenVertices { get; private set; }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.Register<AppLayout, CostRangeChangedMessage, IToken>(this, Tokens.AppLayout, SetRange);
            messenger.Register<AppLayout, StatisticsLineMessage, IToken>(this, Tokens.AppLayout, ShowStatistics);
            messenger.RegisterGraph(this, Tokens.AppLayout, SetGraph);
        }

        public static void SetCursorPositionUnderGraphField()
        {
            Terminal.SetCursorPosition(StatisticsPosition.X, StatisticsPosition.Y + 1);
        }

        private void SetGraph(GraphMessage msg)
        {
            Graph = msg.Graph.Graph;
            int pathFindingStatisticsOffset = Graph.GetLength()
                + HeightOfAbscissaView + 2 + HeightOfGraphParametresView;
            StatisticsPosition = new(0, pathFindingStatisticsOffset);
            RecalculateVerticesConsolePosition();
        }

        private void SetRange(AppLayout th, CostRangeChangedMessage msg)
        {
            int upperValueRange = msg.CostRange.UpperValueOfRange;
            int lowerValueRange = msg.CostRange.LowerValueOfRange;
            CurrentMaxValueOfRange = Math.Max(Math.Abs(upperValueRange),
                Math.Abs(lowerValueRange));
            RecalculateVerticesConsolePosition();
        }

        private void ShowStatistics(AppLayout th, StatisticsLineMessage msg)
        {
            Cursor.SetPosition(StatisticsPosition);
            Terminal.Write(msg.Value.PadRight(Terminal.WindowWidth));
        }

        private static void RecalculateVerticesConsolePosition()
        {
            int costWidth = CurrentMaxValueOfRange.GetDigitsNumber();
            int costUpperValue = Constants.GraphWidthValueRange.UpperValueOfRange - 1;
            int width = costUpperValue.GetDigitsNumber();
            LateralDistanceBetweenVertices = costWidth >= width
                ? costWidth + 2 : width * 2 - costWidth;
            foreach (var vertex in Graph)
            {
                var point = vertex.Position;
                int left = GraphFieldPosition.X + point.GetX() * LateralDistanceBetweenVertices;
                int top = GraphFieldPosition.Y + point.GetY();
                vertex.ConsolePosition = new(left, top);
            }
        }
    }
}