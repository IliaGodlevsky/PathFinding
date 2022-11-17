using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using System;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console
{
    internal static class Screen
    {
        public const int HeightOfAbscissaView = 2;
        public const int HeightOfGraphParametresView = 1;

        public static readonly int WidthOfOrdinateView 
            = (Constants.GraphLengthValueRange.UpperValueOfRange - 1).ToString().Length + 1;
        public static readonly int YCoordinatePadding = WidthOfOrdinateView - 1;

        private static readonly object recipient = new object();
        private static readonly IMessenger messenger;

        private static int PreviousMaxValueOfRange;
        private static int CurrentMaxValueOfRange;

        private static Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public static int LateralDistanceBetweenVertices { get; private set; }

        public static Coordinate2D GraphFieldPosition { get; }

        public static Coordinate2D StatisticsPosition { get; private set; }

        static Screen()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<CostRangeChangedMessage>(recipient, OnCostRangeChanged);
            messenger.Register<UpdatePathfindingStatisticsMessage>(recipient, OnStatisticsUpdated);
            messenger.Register<GraphCreatedMessage>(recipient, OnNewGraphCreated);
            OnCostRangeChanged(new CostRangeChangedMessage(VertexCost.CostRange));
            int x = WidthOfOrdinateView;
            int y = HeightOfAbscissaView + HeightOfGraphParametresView;
            GraphFieldPosition = new Coordinate2D(x, y);
            StatisticsPosition = Coordinate2D.Empty;
        }

        public static void SetCursorPositionUnderMenu(int menuOffset)
        {
            ColorfulConsole.SetCursorPosition(StatisticsPosition.X, StatisticsPosition.Y + menuOffset);
        }

        private static void OnNewGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
            PreviousMaxValueOfRange = CurrentMaxValueOfRange;
            int pathFindingStatisticsOffset = message.Graph.Length + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
            StatisticsPosition = new Coordinate2D(0, pathFindingStatisticsOffset);
            LateralDistanceBetweenVertices = CalculateLateralDistanceBetweenVertices();
            Graph.ForEach(RecalculateConsolePosition);
        }

        private static void OnCostRangeChanged(CostRangeChangedMessage message)
        {
            int upperValueRange = message.CostRange.UpperValueOfRange;
            int lowerValueRange = message.CostRange.LowerValueOfRange;
            int max = Math.Max(upperValueRange, Math.Abs(lowerValueRange));
            PreviousMaxValueOfRange = Math.Max(CurrentMaxValueOfRange, PreviousMaxValueOfRange);
            CurrentMaxValueOfRange = max;
            LateralDistanceBetweenVertices = CalculateLateralDistanceBetweenVertices();
            Graph.ForEach(RecalculateConsolePosition);
        }

        private static void OnStatisticsUpdated(UpdatePathfindingStatisticsMessage message)
        {
            Cursor.SetPosition(StatisticsPosition);
            ColorfulConsole.Write(message.Statistics.PadRight(ColorfulConsole.BufferWidth));
        }

        private static void RecalculateConsolePosition(Vertex vertex)
        {
            var point = (Coordinate2D)vertex.Position;
            int left = GraphFieldPosition.X + point.X * LateralDistanceBetweenVertices;
            int top = GraphFieldPosition.Y + point.Y;
            vertex.ConsolePosition = new Coordinate2D(left, top);
        }

        private static int CalculateLateralDistanceBetweenVertices()
        {
            int currentCostWidth = CurrentMaxValueOfRange.ToString().Length;
            int previousCostWidth = PreviousMaxValueOfRange.ToString().Length;
            int costWidth = Math.Max(currentCostWidth, previousCostWidth);
            int width = (Constants.GraphWidthValueRange.UpperValueOfRange - 1).ToString().Length;
            return costWidth >= width ? costWidth + 2 : width + width - costWidth;
        }
    }
}