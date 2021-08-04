using Algorithm.Infrastructure.EventArguments;
using Common.ValueRanges;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Model;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Graphs;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using System;
using System.Linq;
using System.Threading;
using static ConsoleVersion.Constants;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel, IInterruptable
    {
        private const int Yes = 1;
        private const int No = 0;

        public event InterruptEventHanlder Interrupted;
        public bool IsPathfindingStarted { get; set; }

        public string AlgorithmKeyInputMessage { private get; set; }

        public string TargetVertexInputMessage { private get; set; }

        public string SourceVertexInputMessage { private get; set; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Keys.Count, 1);
            mainModel = model as MainViewModel ?? throw new ArgumentException();
        }

        [MenuItem(Constants.FindPath, MenuItemPriority.Highest)]
        public override void FindPath()
        {
            if (endPoints.HasEndPointsSet)
            {
                try
                {
                    Console.CursorVisible = false;
                    IsPathfindingStarted = true;
                    base.FindPath();
                    while (IsPathfindingStarted)
                    {
                        DetectAlgorithmInterruption();
                    }
                    mainModel.PathFindingStatistics = string.Empty;
                    Console.CursorVisible = true;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            else
            {
                log.Warn("Firstly choose endpoints");
            }
        }

        protected override void Summarize()
        {
            mainModel.PathFindingStatistics = path.PathLength > 0 ? GetStatistics() : CouldntFindPathMsg;
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            base.OnVertexVisited(sender, e);
            mainModel.PathFindingStatistics = GetStatistics();
        }

        protected override void OnAlgorithmInterrupted(object sender, InterruptEventArgs e)
        {
            base.OnAlgorithmInterrupted(sender, e);
            IsPathfindingStarted = false;
        }

        protected override void OnAlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            IsPathfindingStarted = false;
        }

        [MenuItem(Constants.ChooseAlgorithm, MenuItemPriority.High)]
        public void ChooseAlgorithm()
        {
            int algorithmKeyIndex = InputNumber(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;
            var algorithmKey = Algorithms.Keys.ElementAt(algorithmKeyIndex);
            Algorithm = Algorithms[algorithmKey];
        }

        [MenuItem(Constants.InputDelayTime)]
        public void InputDelayTime()
        {
            if (IsVisualizationRequired)
            {
                DelayTime = InputNumber(DelayTimeInputMsg, AlgorithmDelayTimeValueRange);
            }
        }

        [MenuItem(Constants.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            ClearGraph();
            Interrupted?.Invoke(this, new InterruptEventArgs());
            Interrupted = null;
        }

        [MenuItem(Constants.ChooseEndPoints, MenuItemPriority.High)]
        public void ChooseExtremeVertex()
        {
            if (HasAnyVerticesToChooseAsEndPoints())
            {
                endPoints.Reset();
                var chooseMessages = new[] { SourceVertexInputMessage, TargetVertexInputMessage };
                foreach (var message in chooseMessages)
                {
                    var vertex = ChooseVertex(message);
                    int cursorLeft = Console.CursorLeft;
                    int cursorRight = Console.CursorTop;
                    (vertex as Vertex)?.SetAsExtremeVertex();
                    Console.SetCursorPosition(cursorLeft, cursorRight);
                }
            }
            else
            {
                log.Warn("No vertices to choose as end points");
            }
        }

        [MenuItem(Constants.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph()
        {
            mainViewModel.ClearGraph();
        }

        [MenuItem(Constants.ApplyVisualization, MenuItemPriority.Low)]
        public void ApplyVisualization()
        {
            int input = InputNumber(VisualizationMsg, Yes, No);
            IsVisualizationRequired = input == Yes;
        }

        private IVertex ChooseVertex(string message)
        {
            if (mainViewModel.Graph is Graph2D graph2D)
            {
                Console.WriteLine(message);
                IVertex vertex;
                do
                {
                    vertex = InputVertex(graph2D);
                } while (!endPoints.CanBeEndPoint(vertex));

                return vertex;
            }
            return new NullVertex();
        }

        private void DetectAlgorithmInterruption()
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.End)
            {
                algorithm.Interrupt();
            }
        }

        private bool HasAnyVerticesToChooseAsEndPoints()
        {
            var regularVertices = mainViewModel.Graph.Vertices.FilterObstacles();
            int availiableVerticesCount = regularVertices.Count(vertex => !vertex.IsIsolated());
            return availiableVerticesCount >= 2;
        }

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        private readonly MainViewModel mainModel;
    }
}