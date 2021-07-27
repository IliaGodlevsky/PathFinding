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
                    Console.ReadLine();
                    mainViewModel.PathFindingStatistics = string.Empty;
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

        [MenuItem(Constants.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            ClearGraph();
            OnInterrupted?.Invoke(this, new InterruptEventArgs());
            OnInterrupted = null;
        }

        [MenuItem(Constants.ChooseEndPoints, MenuItemPriority.High)]
        public void ChooseExtremeVertex()
        {
            if (HasAnyVerticesToChooseAsEndPoints())
            {
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
                log.Warn("No vertices to choose as end points\n");
            }
        }

        [MenuItem(Constants.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph()
        {
            mainViewModel.ClearGraph();
        }

        protected override void ColorizeProcessedVertices(object sender, AlgorithmEventArgs e) { }

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

        private bool HasAnyVerticesToChooseAsEndPoints()
        {
            var regularVertices = mainViewModel.Graph.Vertices.FilterObstacles();
            int availiableVerticesCount = regularVertices.Count(vertex => !vertex.IsIsolated());
            return availiableVerticesCount >= 2;
        }

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
    }
}