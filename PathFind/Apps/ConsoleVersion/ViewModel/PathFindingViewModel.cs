using Common;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.Resource;
using ConsoleVersion.View;
using GraphLib.Interface;
using GraphLib.Realizations;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Configuration;
using System.Linq;

using static Algorithm.Realizations.AlgorithmsPluginLoader;

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

            int algorithmDelayTimeUpperRange
                    = Convert.ToInt32(ConfigurationManager.AppSettings["algorithmDelayTimeUpperRange"]);
            AlgorithmDelayTimeValueRange = new ValueRange(algorithmDelayTimeUpperRange, 0);
        }

        public override void FindPath()
        {
            if (mainViewModel.Graph.Vertices.Any())
            {
                var mainModel = mainViewModel as MainViewModel;

                mainModel.ClearGraph();
                mainModel.DisplayGraph();
                ChooseExtremeVertex();
                mainModel.DisplayGraph();

                var algorithmKeyIndex = GetAlgorithmKeyIndex();
                var algorithmKeys = AlgorithmsDescriptions;
                AlgorithmKey = algorithmKeys.ElementAt(algorithmKeyIndex);

                DelayTime = Input.InputNumber(
                    Resources.DelayTimeInputMsg,
                    AlgorithmDelayTimeValueRange);

                base.FindPath();

                UpdatePathfindingStatistics();
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
            var chooseMessages = new string[]
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
                vertex.SetAsExtremeVertex();
                Console.SetCursorPosition(cursorLeft, cursorTop);
            }
        }

        private Coordinate2D ChoosePoint(string message)
        {
            Console.WriteLine(message);

            var upperPosibleXValue = (mainViewModel.Graph as Graph2D).Width - 1;
            var upperPosibleYValue = (mainViewModel.Graph as Graph2D).Length - 1;
            Coordinate2D point; 
            IVertex vertex;
            do
            {
                point = Input.InputPoint(upperPosibleXValue, upperPosibleYValue);
                vertex = mainViewModel.Graph[point];
            } while (!EndPoints.CanBeEndPoint(vertex));

            return point;
        }

        private void UpdatePathfindingStatistics()
        {
            var coordinate = MainView.PathfindingStatisticsPosition;
            Console.SetCursorPosition(coordinate.X, coordinate.Y);
            Console.Write(mainViewModel.PathFindingStatistics);
        }

        private readonly int maxAlgorithmKeysNumber;
        private readonly int minAlgorithmKeysNumber;
    }
}
