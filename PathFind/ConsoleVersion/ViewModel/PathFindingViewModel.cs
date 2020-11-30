using ConsoleVersion.InputClass;
using System;
using GraphLib.ViewModel;
using System.Threading;
using System.Linq;
using GraphLib.Coordinates;
using GraphLib.Graphs;
using Algorithm.AlgorithmCreating;
using GraphViewModel.Interfaces;
using GraphLib.Extensions;
using Common.ValueRanges;
using ConsoleVersion.Model;
using Algorithm.EventArguments;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public string AlgorithmKeyInputMessage { private get; set; }

        public string EndVertexInputMessage { private get; set; }

        public string StartVertexInputMessage { private get; set; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            maxAlgorithmKeysNumber = AlgorithmFactory.GetAlgorithmKeys().Count();
            minAlgorithmKeysNumber = 1;
            pauseProvider.PauseEvent += () => { };
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
                AlgorithmKey = AlgorithmFactory.
                    GetAlgorithmKeys().ElementAt(algorithmKeyIndex);

                DelayTime = Input.InputNumber(
                    ConsoleVersionResources.DelayTimeInputMsg,
                    Range.DelayValueRange.UpperRange,
                    Range.DelayValueRange.LowerRange);

                base.FindPath();
            }
        }

        protected override void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);

            thread = new Thread(DisplayGraphIndefinitly);
            thread.Start();
        }

        protected override void OnAlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            thread.Abort();

            base.OnAlgorithmFinished(sender, e);
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

        private void DisplayGraphIndefinitly()
        { 
            while (true)
            {
                Thread.Sleep(millisecondsTimeout: 135);
                (mainViewModel as MainViewModel).DisplayGraph();
            }
        }

        private Thread thread;

        private readonly int maxAlgorithmKeysNumber;
        private readonly int minAlgorithmKeysNumber;
    }
}
