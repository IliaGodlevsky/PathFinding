using AssembleClassesLib.Interface;
using Common.Logging;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Exceptions;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using GraphViewModel;
using GraphViewModel.Interfaces;
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

        public string EndVertexInputMessage { private get; set; }

        public string StartVertexInputMessage { private get; set; }

        public PathFindingViewModel(IAssembleClasses pluginsLoader, IMainModel model)
            : base(pluginsLoader, model)
        {
            maxAlgorithmKeysNumber = pluginsLoader.ClassesNames.Count;
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
                    var algorithmKeys = assembleClasses.ClassesNames;
                    AlgorithmKey = algorithmKeys.ElementAt(algorithmKeyIndex);
                    DelayTime = InputNumber(DelayTimeInputMsg, AlgorithmDelayTimeValueRange);
                    base.FindPath();
                    UpdatePathFindingStatistics();
                    Console.ReadLine();
                    mainModel.ClearGraph();
                    Console.CursorVisible = true;
                }
                catch (Exception ex)
                {
                    RaiseOnEventHappened(ex.Message);
                    Logger.Instance.Error(ex);
                }
            }
            else
            {
                string message = "No vertices to choose as end points\n";
                throw new NoVerticesToChooseAsEndPointsException(message, mainViewModel.Graph);
            }


        }

        protected override void ColorizeProcessedVertices()
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
            return InputNumber(
                AlgorithmKeyInputMessage,
                maxAlgorithmKeysNumber,
                minAlgorithmKeysNumber) - 1;
        }

        private void ChooseExtremeVertex()
        {
            var chooseMessages = new[] { StartVertexInputMessage, EndVertexInputMessage };

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
                } while (!EndPoints.CanBeEndPoint(vertex));

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
            bool IsNotObstacle(IVertex vertex) => !vertex.IsObstacle;
            bool HasNotObstacleNeighbours(IVertex vertex) => vertex.Neighbours.Any(IsNotObstacle);

            var regularVertices = mainViewModel.Graph.Vertices.Where(IsNotObstacle);
            int availiableVerticesToChooseAsEndPoint = regularVertices.Count(HasNotObstacleNeighbours);
            return availiableVerticesToChooseAsEndPoint >= RequiredNumberOfVerticesToStartPathFinding;
        }

        private const int RequiredNumberOfVerticesToStartPathFinding = 2;

        private readonly int maxAlgorithmKeysNumber;
        private readonly int minAlgorithmKeysNumber;
    }
}
