using Autofac;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Messages;
using ConsoleVersion.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System;

namespace ConsoleVersion.Views
{
    internal sealed class MainView : View
    {
        public static int HeightOfAbscissaView => 2;
        public static int HeightOfGraphParametresView => 1;
        public static int YCoordinatePadding => WidthOfOrdinateView - 1;

        public static int WidthOfOrdinateView
            => (Constants.GraphLengthValueRange.UpperValueOfRange - 1).ToString().Length + 1;

        public static Coordinate2D GraphFieldPosition { get; }

        public static Coordinate2D PathfindingStatisticsPosition { get; private set; }

        public static int GetLateralDistanceBetweenVertices()
        {
            int currentCostWidth = CurrentMaxValueOfRange.ToString().Length;
            int previousCostWidth = PreviousMaxValueOfRange.ToString().Length;
            int costWidth = Math.Max(currentCostWidth, previousCostWidth);
            int width = (Constants.GraphWidthValueRange.UpperValueOfRange - 1).ToString().Length;
            return costWidth >= width ? costWidth + 2 : width + width - costWidth;
        }

        public static void SetCursorPositionUnderMenu(int menuOffset)
        {
            var fieldPosition = MainView.PathfindingStatisticsPosition;
            Console.SetCursorPosition(fieldPosition.X, fieldPosition.Y + menuOffset);
        }

        static MainView()
        {
            int x = WidthOfOrdinateView;
            int y = HeightOfAbscissaView + HeightOfGraphParametresView;
            GraphFieldPosition = new Coordinate2D(x, y);
        }

        public MainView(MainViewModel model) : base(model)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainView, OnNewGraphCreated);
            messenger.Register<CostRangeChangedMessage>(this, MessageTokens.MainView, OnCostRangeChanged);
            messenger.Register<UpdateStatisticsMessage>(this, MessageTokens.MainView, OnStatisticsUpdated);
            var message = new CostRangeChangedMessage(BaseVertexCost.CostRange);
            OnCostRangeChanged(message);
        }

        private void OnNewGraphCreated(GraphCreatedMessage message)
        {
            PreviousMaxValueOfRange = CurrentMaxValueOfRange;
            if (message.Graph is Graph2D graph2D)
            {
                int pathFindingStatisticsOffset = graph2D.Length + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
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
            lock (locker)
            {
                var coordinate = MainView.PathfindingStatisticsPosition;
                if (coordinate != null)
                {
                    using (ConsoleCursor.UseCursorPosition(coordinate.X, coordinate.Y))
                    {
                        Console.Write(message.Statistics.PadRight(Console.BufferWidth));
                    }
                }
            }
        }

        private static int PreviousMaxValueOfRange;
        private static int CurrentMaxValueOfRange;
        private readonly IMessenger messenger;
        private static readonly object locker = new object();
    }
}