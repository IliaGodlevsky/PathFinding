using ConsoleVersion.InputClass;
using System;
using GraphLib.ViewModel;
using System.Threading;
using System.Linq;
using GraphLib.PauseMaking;
using GraphLib.Coordinates;
using GraphLib.Graphs;
using Algorithm.AlgorithmCreating;
using GraphViewModel.Interfaces;
using GraphLib.Extensions;
using Common.ValueRanges;
using ConsoleVersion.Model;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public Tuple<string, string, string> Messages { get; set; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            maxAlgorithmValue = AlgorithmFactory.AlgorithmKeys.Count();
            minAlgorithmValue = 1;
        }

        public override void FindPath()
        {
            if (!graph.Any())
                return;

            mainViewModel.ClearGraph();
            (mainViewModel as MainViewModel).DisplayGraph();
            ChooseExtremeVertex();
            (mainViewModel as MainViewModel).DisplayGraph();
            AlgorithmKey = AlgorithmFactory.AlgorithmKeys.ElementAt(GetAlgorithmKeyIndex());

            base.FindPath();
        }

        protected override void PrepareAlgorithm()
        {
            DelayTime = Input.InputNumber(
                ConsoleVersionResources.DelayTimeMsg, 
                Range.DelayValueRange.UpperRange, 
                Range.DelayValueRange.LowerRange);

            base.PrepareAlgorithm();

            var thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(millisecondsTimeout: 135);
                    (mainViewModel as MainViewModel).DisplayGraph();
                }
            });

            pathAlgorithm.OnStarted += (sender, eventArgs) =>
            {
                thread.Start();
            };

            var pauser = new PauseProvider(DelayTime);
            pauser.PauseEvent += () => { };
            pathAlgorithm.OnVertexVisited += (vertex) => pauser.Pause();

            pathAlgorithm.OnFinished += (sender, eventArgs) =>
            {
                thread.Abort();
                thread.Join();

                if (!eventArgs.HasFoundPath)
                {
                    (mainViewModel as MainViewModel).DisplayGraph();
                    Console.WriteLine(badResultMessage);
                    Console.ReadLine();
                }
            };
        }



        private int GetAlgorithmKeyIndex()
        {
            return Input.InputNumber(
                Messages.Item3,
                maxAlgorithmValue, 
                minAlgorithmValue) - 1;
        }

        private void ChooseExtremeVertex()
        {           
            const int EXTREME_VERTICES_COUNT = 2;

            string[] chooseMessages = new string[EXTREME_VERTICES_COUNT] 
            { 
                Messages.Item1, 
                Messages.Item2 
            };

            for (int i = 0; i < EXTREME_VERTICES_COUNT; i++)
            {
                var point = ChoosePoint(chooseMessages[i]);
                (mainViewModel.Graph[point] as ConsoleVertex).SetAsExtremeVertex();
            }
        }

        private Coordinate2D ChoosePoint(string message)
        {
            Console.WriteLine(message);

            var upperPosibleXValue = (graph as Graph2D).Width;
            var upperPosibleYValue = (graph as Graph2D).Length;

            var point = Input.InputPoint(upperPosibleXValue, upperPosibleYValue);

            while (!graph[point].IsValidToBeRange())
            {
                point = Input.InputPoint(upperPosibleXValue, upperPosibleYValue);
            }

            return point;
        }

        private readonly int maxAlgorithmValue;
        private readonly int minAlgorithmValue;
    }
}
