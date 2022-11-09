using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using System;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.Views
{
    internal sealed class MainView : View
    {
        private static int PreviousMaxValueOfRange;
        private static int CurrentMaxValueOfRange;

        private readonly IMessenger messenger;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public static int LateralDistanceBetweenVertices { get; private set; }

        public static Coordinate2D GraphFieldPosition { get; }

        public static Coordinate2D StatisticsPosition { get; private set; }

        public MainView(MainViewModel model) : base(model)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, OnNewGraphCreated);
            messenger.Register<CostRangeChangedMessage>(this, OnCostRangeChanged);
            messenger.Register<UpdateStatisticsMessage>(this, OnStatisticsUpdated);
            OnCostRangeChanged(new CostRangeChangedMessage(VertexCost.CostRange));
        }

        static MainView()
        {
            int x = Constants.WidthOfOrdinateView;
            int y = Constants.HeightOfAbscissaView + Constants.HeightOfGraphParametresView;
            GraphFieldPosition = new Coordinate2D(x, y);
            StatisticsPosition = new Coordinate2D(0, 0);
        }

        public static void SetCursorPositionUnderMenu(int menuOffset)
        {
            var fieldPosition = StatisticsPosition;
            ColorfulConsole.SetCursorPosition(fieldPosition.X, fieldPosition.Y + menuOffset);
        }

        private void OnNewGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
            PreviousMaxValueOfRange = CurrentMaxValueOfRange;
            int pathFindingStatisticsOffset = message.Graph.Length
                + Constants.HeightOfAbscissaView * 2 + Constants.HeightOfGraphParametresView;
            StatisticsPosition = new Coordinate2D(0, pathFindingStatisticsOffset);
            LateralDistanceBetweenVertices = CalculateLateralDistanceBetweenVertices();
            graph.ForEach(RecalculateConsolePosition);
        }

        private void OnCostRangeChanged(CostRangeChangedMessage message)
        {
            int upperValueRange = message.CostRange.UpperValueOfRange;
            int lowerValueRange = message.CostRange.LowerValueOfRange;
            int max = Math.Max(upperValueRange, Math.Abs(lowerValueRange));
            PreviousMaxValueOfRange = Math.Max(CurrentMaxValueOfRange, PreviousMaxValueOfRange);
            CurrentMaxValueOfRange = max;
            LateralDistanceBetweenVertices = CalculateLateralDistanceBetweenVertices();
            graph.ForEach(RecalculateConsolePosition);
        }

        private void OnStatisticsUpdated(UpdateStatisticsMessage message)
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