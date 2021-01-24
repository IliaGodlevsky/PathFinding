using Algorithm.AlgorithmCreating;
using Common.ValueRanges;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Coordinates;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Linq;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public static Coordinate2D ConsoleCoordinates { get; set; }

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
            if (mainViewModel.Graph.Any())
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
                    Range.DelayValueRange.UpperValueOfRange,
                    Range.DelayValueRange.LowerValueOfRange);

                base.FindPath();

                Console.ReadLine();
                mainModel.ClearGraph();
                Console.CursorVisible = true;
            }
        }

        protected override void OnAlgorithmIntermitted()
        {
            return;
        }

        protected override void OnVertexVisited(object sender, EventArgs e)
        {
            base.OnVertexVisited(sender, e);
            UpdatePathfindingStatistics();
        }

        protected override void OnAlgorithmFinished(object sender, EventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            UpdatePathfindingStatistics();
        }

        protected override void OnAlgorithmStarted(object sender, EventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            Console.CursorVisible = false;
        }

        private int GetAlgorithmKeyIndex()
        {
            var position = MainView.MainMenuPosition;
            Console.SetCursorPosition(position.X, position.Y);
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
            var position = MainView.MainMenuPosition;
            Console.SetCursorPosition(position.X, position.Y);
            for (int i = 0; i < chooseMessages.Length; i++)
            {               
                var point = ChoosePoint(chooseMessages[i]);
                var left = Console.CursorLeft;
                var top = Console.CursorTop;
                var vertex = mainViewModel.Graph[point] as Vertex;
                vertex.SetAsExtremeVertex();
                Console.SetCursorPosition(left, top);
            }
        }

        private Coordinate2D ChoosePoint(string message)
        {
            Console.WriteLine(message);

            var upperPosibleXValue = (mainViewModel.Graph as Graph2D).Width - 1;
            var upperPosibleYValue = (mainViewModel.Graph as Graph2D).Length - 1;

            var point = Input.InputPoint(upperPosibleXValue, upperPosibleYValue);

            while (!mainViewModel.Graph[point].IsValidToBeExtreme())
            {
                point = Input.InputPoint(upperPosibleXValue, upperPosibleYValue);
            }

            return point;
        }

        private void UpdatePathfindingStatistics()
        {
            var coordinate = MainView.PathfindingStatisticsConsoleStartCoordinate;
            Console.SetCursorPosition(coordinate.X, coordinate.Y);
            Console.Write(mainViewModel.PathFindingStatistics);
        }

        private readonly int maxAlgorithmKeysNumber;
        private readonly int minAlgorithmKeysNumber;
    }
}
