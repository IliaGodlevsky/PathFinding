﻿using Algorithm.Infrastructure.EventArguments;
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
using Logging.Interface;
using System;
using System.Linq;
using static ConsoleVersion.Constants;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel
    {
        public string AlgorithmKeyInputMessage { private get; set; }

        public string TargetVertexInputMessage { private get; set; }

        public string SourceVertexInputMessage { private get; set; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            maxAlgorithmKeysNumber = Algorithms.Keys.Count;
            minAlgorithmKeysNumber = 1;
        }

        public override void FindPath()
        {
            bool canFindPath = HasAnyVerticesToChooseAsEndPoints();
            if (canFindPath && mainViewModel is MainViewModel mainModel)
            {
                try
                {
                    mainModel.ClearGraph();
                    mainModel.DisplayGraph();
                    ChooseExtremeVertex();
                    mainModel.DisplayGraph();
                    int algorithmKeyIndex = GetAlgorithmKeyIndex();
                    var algorithmKey = Algorithms.Keys.ElementAt(algorithmKeyIndex);
                    Algorithm = Algorithms[algorithmKey];
                    DelayTime = InputNumber(DelayTimeInputMsg, AlgorithmDelayTimeValueRange);
                    base.FindPath();
                    UpdatePathFindingStatistics();
                    Console.ReadLine();
                    mainModel.ClearGraph();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            else
            {
                string message = "No vertices to choose as end points\n";
                throw new NoVerticesToChooseAsEndPointsException(message, mainViewModel.Graph);
            }
        }

        protected override void ColorizeProcessedVertices(object sender, AlgorithmEventArgs e)
        {
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            base.OnVertexVisited(sender, e);
            UpdatePathFindingStatistics();
        }

        private int GetAlgorithmKeyIndex()
        {
            return InputNumber(
                AlgorithmKeyInputMessage,
                maxAlgorithmKeysNumber,
                minAlgorithmKeysNumber) - 1;
        }

        private void ChooseExtremeVertex()
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
                    vertex = mainViewModel.Graph[point];
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
            Console.Write(mainViewModel.PathFindingStatistics);
        }

        public bool HasAnyVerticesToChooseAsEndPoints()
        {
            var regularVertices = mainViewModel.Graph.Vertices.FilterObstacles();
            int availiableVerticesCount = regularVertices.Count(vertex => !vertex.IsIsolated());
            return availiableVerticesCount >= RequiredNumberOfVerticesToStartPathFinding;
        }

        private const int RequiredNumberOfVerticesToStartPathFinding = 2;

        private readonly int maxAlgorithmKeysNumber;
        private readonly int minAlgorithmKeysNumber;
    }
}