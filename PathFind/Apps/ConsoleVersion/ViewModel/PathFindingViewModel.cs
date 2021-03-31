using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.Resource;
using ConsoleVersion.View;
using GraphLib.Interface;
using GraphLib.Realizations;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using static Algorithm.Realizations.AlgorithmsFactory;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public string AlgorithmKeyInputMessage { private get; set; }

        public string EndVertexInputMessage { private get; set; }

        public string StartVertexInputMessage { private get; set; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            maxAlgorithmKeysNumber = AlgorithmsDescriptions.Count();
            minAlgorithmKeysNumber = 1;
        }

        public override void FindPath()
        {
            if (mainViewModel.Graph.Vertices.Any() && mainViewModel is MainViewModel mainModel)
            {
                mainModel.ClearGraph();
                mainModel.DisplayGraph();
                ChooseExtremeVertex();
                mainModel.DisplayGraph();

                var algorithmKeyIndex = GetAlgorithmKeyIndex();
                var algorithmKeys = AlgorithmsDescriptions;
                AlgorithmKey = algorithmKeys.ElementAt(algorithmKeyIndex);

                DelayTime = Input.InputNumber(Resources.DelayTimeInputMsg,
                    Constants.AlgorithmDelayTimeValueRange);

                base.FindPath();

                UpdatePathFindingStatistics();
                Console.ReadLine();
                mainModel.ClearGraph();
                Console.CursorVisible = true;
            }
        }

        protected override void OnAlgorithmIntermitted()
        {
        }

        protected override void OnVertexVisited(object sender, EventArgs e)
        {
            base.OnVertexVisited(sender, e);
            UpdatePathFindingStatistics();
        }

        protected override void OnAlgorithmStarted(object sender, EventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            Console.CursorVisible = false;
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
            var chooseMessages = new[]
            {
                StartVertexInputMessage,
                EndVertexInputMessage
            };

            for (int i = 0; i < chooseMessages.Length; i++)
            {
                var point = ChoosePoint(chooseMessages[i]);
                var vertex = mainViewModel.Graph[point] as Vertex;
                var cursorLeft = Console.CursorLeft;
                var cursorTop = Console.CursorTop;
                vertex?.SetAsExtremeVertex();
                Console.SetCursorPosition(cursorLeft, cursorTop);
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
                    point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);
                    vertex = mainViewModel.Graph[point];
                } while (!EndPoints.CanBeEndPoint(vertex));

                return point;
            }

            string paramName = nameof(mainViewModel.Graph);
            string requiredTypeName = nameof(Graph2D);
            string exceptionMessage = $"{paramName} is not of type {requiredTypeName}";
            throw new Exception(exceptionMessage);
        }

        private void UpdatePathFindingStatistics()
        {
            var coordinate = MainView.PathfindingStatisticsPosition;
            Console.SetCursorPosition(coordinate.X, coordinate.Y);
            Console.Write(mainViewModel.PathFindingStatistics);
        }

        private readonly int maxAlgorithmKeysNumber;
        private readonly int minAlgorithmKeysNumber;
    }
}
