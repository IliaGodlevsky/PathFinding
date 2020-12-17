using Algorithm.AlgorithmCreating;
using Common.ValueRanges;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using GraphLib.Coordinates;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using System.Threading;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public string AlgorithmKeyInputMessage { private get; set; }

        public string EndVertexInputMessage { private get; set; }

        public string StartVertexInputMessage { private get; set; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            maxAlgorithmKeysNumber = AlgorithmFactory.AlgorithmsDescriptions.Count();
            minAlgorithmKeysNumber = 1;
        }

        public override void FindPath()
        {
            if (graph.Any())
            {
                var mainModel = mainViewModel as MainViewModel;

                mainModel.ClearGraph();
                mainModel.DisplayGraph();
                ChooseExtremeVertex();
                mainModel.DisplayGraph();

                var algorithmKeyIndex = GetAlgorithmKeyIndex();
                var algorithmKeys = AlgorithmFactory.AlgorithmsDescriptions;
                AlgorithmKey = algorithmKeys.ElementAt(algorithmKeyIndex);

                DelayTime = Input.InputNumber(
                    ConsoleVersionResources.DelayTimeInputMsg,
                    Range.DelayValueRange.UpperRange,
                    Range.DelayValueRange.LowerRange);

                base.FindPath();
            }
        }

        protected override void OnAlgorithmStarted()
        {
            IsPathfindingEnded = false;
            base.OnAlgorithmStarted();

            thread = new Thread(DisplayGraphDuringPathfinding);
            thread.Start();
        }

        protected override void OnAlgorithmFinished()
        {
            IsPathfindingEnded = true;
            thread.Join();

            base.OnAlgorithmFinished();
        }

        private int GetAlgorithmKeyIndex()
        {
            return Input.InputNumber(
                AlgorithmKeyInputMessage,
                maxAlgorithmKeysNumber,
                minAlgorithmKeysNumber) - 1;
        }

        private void ChooseExtremeVertex()
        {
            var chooseMessages = new string[]
            {
                StartVertexInputMessage,
                EndVertexInputMessage
            };

            for (int i = 0; i < chooseMessages.Length; i++)
            {
                var point = ChoosePoint(chooseMessages[i]);
                var vertex = mainViewModel.Graph[point] as ConsoleVertex;
                vertex.SetAsExtremeVertex();
            }
        }

        private Coordinate2D ChoosePoint(string message)
        {
            Console.WriteLine(message);

            var upperPosibleXValue = (graph as Graph2D).Width - 1;
            var upperPosibleYValue = (graph as Graph2D).Length - 1;

            var point = Input.InputPoint(upperPosibleXValue, upperPosibleYValue);

            while (!graph[point].IsValidToBeExtreme())
            {
                point = Input.InputPoint(upperPosibleXValue, upperPosibleYValue);
            }

            return point;
        }

        private void DisplayGraphDuringPathfinding()
        {
            var mainModel = mainViewModel as MainViewModel;
            while (!IsPathfindingEnded)
            {
                Thread.Sleep(millisecondsTimeout: 135);
                mainModel.DisplayGraph();
            }
        }

        private bool IsPathfindingEnded { get; set; }

        private Thread thread;

        private readonly int maxAlgorithmKeysNumber;
        private readonly int minAlgorithmKeysNumber;
    }
}
