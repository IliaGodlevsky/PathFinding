using Algorithm.Infrastructure.EventArguments;
using Common.ValueRanges;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Base;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using System;
using System.Linq;
using static ConsoleVersion.Constants;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel, IInterruptable
    {
        public event InterruptEventHanlder OnInterrupted;

        public string AlgorithmKeyInputMessage { private get; set; }

        public string TargetVertexInputMessage { private get; set; }

        public string SourceVertexInputMessage { private get; set; }

        public bool IsInterruptRequested { get; private set; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Keys.Count, 1);
        }

        [MenuItem(Constants.FindPath, MenuItemPriority.Highest)]
        public override void FindPath()
        {
            if (endPoints.HasEndPointsSet)
            {
                try
                {
                    base.FindPath();
                    UpdatePathFindingStatistics();
                    Console.ReadLine();
                    mainViewModel.PathFindingStatistics = string.Empty;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }

        [MenuItem(Constants.ChooseAlgorithm, MenuItemPriority.High)]
        public void ChooseAlgorithm()
        {
            int algorithmKeyIndex = InputNumber(AlgorithmKeyInputMessage,
                algorithmKeysValueRange) - 1;
            var algorithmKey = Algorithms.Keys.ElementAt(algorithmKeyIndex);
            Algorithm = Algorithms[algorithmKey];
        }

        [MenuItem(Constants.InputDelayTime)]
        public void InputDelayTime()
        {
            DelayTime = InputNumber(DelayTimeInputMsg, AlgorithmDelayTimeValueRange);
        }

        [MenuItem(Constants.Cancel, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            IsInterruptRequested = true;
            ClearGraph();
            OnInterrupted?.Invoke(this, new InterruptEventArgs());
        }

        [MenuItem(Constants.ChooseEndPoints, MenuItemPriority.High)]
        public void ChooseExtremeVertex()
        {
            if (HasAnyVerticesToChooseAsEndPoints())
            {
                var chooseMessages = new[] { SourceVertexInputMessage, TargetVertexInputMessage };
                foreach (var message in chooseMessages)
                {
                    var point = ChoosePoint(message);
                    var vertex = mainViewModel.Graph[point] as Vertex;
                    int cursorLeft = Console.CursorLeft;
                    int cursorRight = Console.CursorTop;
                    vertex?.SetAsExtremeVertex();
                    Console.SetCursorPosition(cursorLeft, cursorRight);
                }
            }
            else
            {
                log.Warn("No vertices to choose as end points\n");
            }
        }

        [MenuItem(Constants.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph()
        {
            mainViewModel.ClearGraph();
        }

        public void DisplayGraph()
        {
            (mainViewModel as MainViewModel)?.DisplayGraph();
        }

        protected override void Summarize()
        {
            base.Summarize();
            UpdatePathFindingStatistics();
        }

        protected override void ColorizeProcessedVertices(object sender, AlgorithmEventArgs e) { }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            base.OnVertexVisited(sender, e);
            UpdatePathFindingStatistics();
        }

        private Coordinate2D ChoosePoint(string message)
        {
            if (mainViewModel.Graph is Graph2D graph2D)
            {
                Console.WriteLine(message);
                var upperPossibleXValue = graph2D.Width - 1;
                var upperPossibleYValue = graph2D.Length - 1;
                Coordinate2D point;
                IVertex vertex;
                do
                {
                    point = InputPoint(upperPossibleXValue, upperPossibleYValue);
                    vertex = graph2D[point];
                } while (!endPoints.CanBeEndPoint(vertex));

                return point;
            }

            string paramName = nameof(mainViewModel.Graph);
            string requiredTypeName = nameof(Graph2D);
            string exceptionMessage = $"{paramName} is not of type {requiredTypeName}";
            throw new WrongGraphTypeException(exceptionMessage);
        }

        private void UpdatePathFindingStatistics()
        {
            var coordinate = MainView.PathfindingStatisticsPosition;
            Console.SetCursorPosition(coordinate.X, coordinate.Y);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(coordinate.X, coordinate.Y);
            Console.Write(mainViewModel.PathFindingStatistics);
        }

        private bool HasAnyVerticesToChooseAsEndPoints()
        {
            var regularVertices = mainViewModel.Graph.Vertices.FilterObstacles();
            int availiableVerticesCount = regularVertices.Count(vertex => !vertex.IsIsolated());
            return availiableVerticesCount >= 2;
        }

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
    }
}