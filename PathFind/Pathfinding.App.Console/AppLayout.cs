using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Drawing;

namespace Pathfinding.App.Console
{
    internal sealed class AppLayout : ICanRecieveMessage
    {
        public const int HeightOfAbscissaView = 2;
        public const int HeightOfGraphParametresView = 1;

        public static readonly int WidthOfOrdinateView = (Constants.GraphLengthValueRange.UpperValueOfRange - 1).GetDigitsNumber() + 1;
        public static readonly int YCoordinatePadding = WidthOfOrdinateView - 1;

        private static readonly Point GraphFieldPosition = new(WidthOfOrdinateView, HeightOfAbscissaView + HeightOfGraphParametresView);

        private static int CurrentMaxValueOfRange;
        private static Point StatisticsPosition = Point.Empty;

        private static Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public static int LateralDistanceBetweenVertices { get; private set; }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<InclusiveValueRange<int>>(this, Tokens.AppLayout, SetRange);
            messenger.RegisterData<string>(this, Tokens.AppLayout, ShowStatistics);
            messenger.RegisterGraph(this, Tokens.AppLayout, SetGraph);
        }

        public static void SetCursorPositionUnderGraphField()
        {
            System.Console.SetCursorPosition(StatisticsPosition.X, StatisticsPosition.Y + 1);
        }

        private static void SetGraph(Graph2D<Vertex> graph)
        {
            Graph = graph;
            int pathFindingStatisticsOffset = Graph.Length + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
            StatisticsPosition = new(0, pathFindingStatisticsOffset);
            RecalculateVerticesConsolePosition();
        }

        private static void SetRange(InclusiveValueRange<int> range)
        {
            int upperValueRange = range.UpperValueOfRange;
            int lowerValueRange = range.LowerValueOfRange;
            CurrentMaxValueOfRange = Math.Max(Math.Abs(upperValueRange), Math.Abs(lowerValueRange));
            RecalculateVerticesConsolePosition();
        }

        private static void ShowStatistics(string statistics)
        {
            Cursor.SetPosition(StatisticsPosition);
            System.Console.Write(statistics.PadRight(System.Console.BufferWidth));
        }

        private static void RecalculateVerticesConsolePosition()
        {
            int costWidth = CurrentMaxValueOfRange.GetDigitsNumber();
            int costUpperValue = Constants.GraphWidthValueRange.UpperValueOfRange - 1;
            int width = costUpperValue.GetDigitsNumber();
            LateralDistanceBetweenVertices = costWidth >= width ? costWidth + 2 : width * 2 - costWidth;
            Graph.ForEach(vertex =>
            {
                var point = (Coordinate2D)vertex.Position;
                int left = GraphFieldPosition.X + point.X * LateralDistanceBetweenVertices;
                int top = GraphFieldPosition.Y + point.Y;
                vertex.ConsolePosition = new(left, top);
            });
        }
    }
}