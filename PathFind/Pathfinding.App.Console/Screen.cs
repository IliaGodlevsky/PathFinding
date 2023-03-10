using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Drawing;

namespace Pathfinding.App.Console
{
    internal static class Screen
    {
        public const int HeightOfAbscissaView = 2;
        public const int HeightOfGraphParametresView = 1;

        public static readonly int WidthOfOrdinateView
            = (Constants.GraphLengthValueRange.UpperValueOfRange - 1).GetDigitsNumber() + 1;
        public static readonly int YCoordinatePadding = WidthOfOrdinateView - 1;

        private static readonly object recipient = new object();
        private static readonly IMessenger messenger;

        private static int CurrentMaxValueOfRange;

        private static Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public static int LateralDistanceBetweenVertices { get; private set; }

        public static Point GraphFieldPosition { get; }

        public static Point StatisticsPosition { get; private set; } = Point.Empty;

        static Screen()
        {
            messenger = Registry.Container.Resolve<IMessenger>();
            messenger.RegisterData<InclusiveValueRange<int>>(recipient, Tokens.Screen, OnCostRangeChanged);
            messenger.RegisterData<string>(recipient, Tokens.Screen, OnStatisticsUpdated);
            messenger.RegisterGraph(recipient, Tokens.Screen, OnNewGraphCreated);
            int x = WidthOfOrdinateView;
            int y = HeightOfAbscissaView + HeightOfGraphParametresView;
            GraphFieldPosition = new(x, y);
        }

        public static void SetCursorPositionUnderMenu(int menuOffset = 0)
        {
            System.Console.SetCursorPosition(StatisticsPosition.X, StatisticsPosition.Y + menuOffset);
        }

        private static void OnNewGraphCreated(DataMessage<Graph2D<Vertex>> message)
        {
            Graph = message.Value;
            int pathFindingStatisticsOffset = message.Value.Length + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
            StatisticsPosition = new(0, pathFindingStatisticsOffset);
            RecalculateVerticesConsolePosition();
        }

        private static void OnCostRangeChanged(DataMessage<InclusiveValueRange<int>> message)
        {
            int upperValueRange = message.Value.UpperValueOfRange;
            int lowerValueRange = message.Value.LowerValueOfRange;
            int max = Math.Max(Math.Abs(upperValueRange), Math.Abs(lowerValueRange));
            CurrentMaxValueOfRange = max;
            RecalculateVerticesConsolePosition();
        }

        private static void RecalculateVerticesConsolePosition()
        {
            LateralDistanceBetweenVertices = CalculateLateralDistanceBetweenVertices();
            Graph.ForEach(RecalculateConsolePosition);
        }

        private static void OnStatisticsUpdated(DataMessage<string> msg)
        {
            Cursor.SetPosition(StatisticsPosition);
            System.Console.Write(msg.Value.PadRight(System.Console.BufferWidth));
        }

        private static void RecalculateConsolePosition(Vertex vertex)
        {
            var point = (Coordinate2D)vertex.Position;
            int left = GraphFieldPosition.X + point.X * LateralDistanceBetweenVertices;
            int top = GraphFieldPosition.Y + point.Y;
            vertex.ConsolePosition = new(left, top);
        }

        private static int CalculateLateralDistanceBetweenVertices()
        {
            int costWidth = CurrentMaxValueOfRange.GetDigitsNumber();
            int width = (Constants.GraphWidthValueRange.UpperValueOfRange - 1).GetDigitsNumber();
            return costWidth >= width ? costWidth + 2 : width + width - costWidth;
        }
    }
}