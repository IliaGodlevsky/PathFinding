using Autofac;
using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using GraphLib.Realizations.Coordinates;
using System;

namespace ConsoleVersion.Views
{
    internal sealed class MainView : View
    {
        private static int PreviousMaxValueOfRange;
        private static int CurrentMaxValueOfRange;

        private readonly IMessenger messenger;

        private IGraph graph = NullGraph.Instance;

        public static int LateralDistanceBetweenVertices { get; private set; }

        public static Coordinate2D GraphFieldPosition { get; }

        public static Coordinate2D StatisticsPosition { get; private set; }

        public MainView(MainViewModel model) : base(model)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainView, OnNewGraphCreated);
            messenger.Register<CostRangeChangedMessage>(this, MessageTokens.MainView, OnCostRangeChanged);
            messenger.Register<UpdateStatisticsMessage>(this, MessageTokens.MainView, OnStatisticsUpdated);
            OnCostRangeChanged(new CostRangeChangedMessage(BaseVertexCost.CostRange));
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
            var fieldPosition = MainView.StatisticsPosition;
            Console.SetCursorPosition(fieldPosition.X, fieldPosition.Y + menuOffset);
        }

        private void OnNewGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
            PreviousMaxValueOfRange = CurrentMaxValueOfRange;
            int pathFindingStatisticsOffset = message.Graph.Length
                + Constants.HeightOfAbscissaView * 2 + Constants.HeightOfGraphParametresView;
            StatisticsPosition = new Coordinate2D(0, pathFindingStatisticsOffset);
            LateralDistanceBetweenVertices = CalculateLateralDistanceBetweenVertices();
            graph.ForEach<Vertex>(RecalculateConsolePosition);
        }

        private void OnCostRangeChanged(CostRangeChangedMessage message)
        {
            int upperValueRange = message.CostRange.UpperValueOfRange;
            int lowerValueRange = message.CostRange.LowerValueOfRange;
            int max = Math.Max(upperValueRange, Math.Abs(lowerValueRange));
            PreviousMaxValueOfRange = Math.Max(CurrentMaxValueOfRange, PreviousMaxValueOfRange);
            CurrentMaxValueOfRange = max;
            LateralDistanceBetweenVertices = CalculateLateralDistanceBetweenVertices();
            graph.ForEach<Vertex>(RecalculateConsolePosition);
        }

        private void OnStatisticsUpdated(UpdateStatisticsMessage message)
        {
            Cursor.SetPosition(MainView.StatisticsPosition);
            Console.Write(message.Statistics.PadRight(Console.BufferWidth));
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