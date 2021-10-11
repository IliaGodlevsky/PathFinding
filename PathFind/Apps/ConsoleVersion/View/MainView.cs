using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.View.Abstraction;
using ConsoleVersion.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System;

namespace ConsoleVersion.View
{
    internal sealed class MainView : View<MainViewModel>, IView
    {
        public static int HeightOfAbscissaView => 2;
        public static int HeightOfGraphParametresView => 1;
        public static int YCoordinatePadding => WidthOfOrdinateView - 1;

        public static int WidthOfOrdinateView
            => Constants.GraphLengthValueRange.UpperValueOfRange.ToString().Length + 1;

        public static int GetLateralDistanceBetweenVertices()
        {
            int currentCostWidth = CurrentMaxValueOfRange.ToString().Length;
            int previousCostWidth = PreviousMaxValueOfRange.ToString().Length;
            int costWidth = Math.Max(currentCostWidth, previousCostWidth);
            int width = Constants.GraphWidthValueRange.UpperValueOfRange.ToString().Length;
            return costWidth >= width ? costWidth + 2 : width + width - costWidth;
        }

        private void OnNewGraphCreated(GraphCreatedMessage message)
        {
            PreviousMaxValueOfRange = CurrentMaxValueOfRange;
            if (message.Graph is Graph2D graph2D)
            {
                int pathFindingStatisticsOffset = graph2D.Length
                    + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
                PathfindingStatisticsPosition = new Coordinate2D(0, pathFindingStatisticsOffset);
            }
        }

        private void OnCostRangeChanged(CostRangeChangedMessage message)
        {
            int upperValueRange = message.CostRange.UpperValueOfRange;
            int lowerValueRange = message.CostRange.LowerValueOfRange;
            int max = Math.Max(upperValueRange, Math.Abs(lowerValueRange));
            PreviousMaxValueOfRange = Math.Max(CurrentMaxValueOfRange, PreviousMaxValueOfRange);
            CurrentMaxValueOfRange = max;
        }

        private void OnStatisticsUpdated(UpdateStatisticsMessage message)
        {
            var coordinate = MainView.PathfindingStatisticsPosition;
            if (coordinate != null)
            {
                Console.SetCursorPosition(coordinate.X, coordinate.Y);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(coordinate.X, coordinate.Y);
                Console.Write(message.Statistics);
            }
        }

        public static Coordinate2D GraphFieldPosition { get; set; }

        public static Coordinate2D PathfindingStatisticsPosition { get; set; }

        static MainView()
        {
            int x = WidthOfOrdinateView;
            int y = HeightOfAbscissaView + HeightOfGraphParametresView;
            GraphFieldPosition = new Coordinate2D(x, y);
        }

        public MainView(MainViewModel model) : base(model)
        {
            Messenger.Default.Register<GraphCreatedMessage>(this, MessageTokens.MainView, OnNewGraphCreated);
            Messenger.Default.Register<CostRangeChangedMessage>(this, MessageTokens.MainView, OnCostRangeChanged);
            Messenger.Default.Register<UpdateStatisticsMessage>(this, MessageTokens.MainView, OnStatisticsUpdated);
            Model.Interrupted += OnInterrupted;
            NewMenuIteration += Model.DisplayGraph;
            var message = new CostRangeChangedMessage(BaseVertexCost.CostRange);
            OnCostRangeChanged(message);
        }

        private static int PreviousMaxValueOfRange;
        private static int CurrentMaxValueOfRange;
    }
}