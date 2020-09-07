using ConsoleVersion.Forms;
using ConsoleVersion.InputClass;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Extensions;
using GraphLibrary.Model;
using GraphLibrary.PauseMaker;
using System;
using System.Drawing;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Common.Constants;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindViewModel : AbstractPathFindModel
    {
        private readonly AbstractVertexEventHolder eventHolder;

        public Tuple<string, string, string> Messages { get; set; }

        public PathFindViewModel(IMainModel model) : base(model)
        {
            eventHolder = model.VertexEventHolder;
        }

        public override void FindPath()
        {
            mainViewModel.ClearGraph();
            GraphShower.DisplayGraph(mainViewModel);
            ChooseStart();
            ChooseEnd();
            GraphShower.DisplayGraph(mainViewModel);
            Algorithm = GetAlgorithmEnum();
            base.FindPath();
        }

        protected override void PrepareAlgorithm()
        {
            DelayTime = Input.InputNumber(ConsoleVersionResources.DelayTimeMsg, 
                Range.DelayValueRange.UpperRange, 
                Range.DelayValueRange.LowerRange);
            var pauser = new PauseProvider(DelayTime) { PauseEvent = () => { } };
            pathAlgorithm.OnVertexVisited += (vertex) => pauser.Pause();
            pathAlgorithm.OnFinished += (sender, eventArgs) =>
              {
                  if (!eventArgs.HasFoundPath)
                  {
                      GraphShower.DisplayGraph(mainViewModel);
                      Console.WriteLine(badResultMessage);
                      Console.ReadLine();
                  }
              };
            base.PrepareAlgorithm();
        }

        private Algorithms GetAlgorithmEnum()
        {
            return (Algorithms)Input.InputNumber(Messages.Item3,
                (int)Algorithms.ValueGreedyAlgorithm, 
                (int)Algorithms.LiAlgorithm);
        }

        private void ChooseStart()
        {
            ChooseRange(Messages.Item1, eventHolder.SetStartVertex);
        }

        private void ChooseEnd()
        {
            ChooseRange(Messages.Item2, eventHolder.SetDestinationVertex);
        }

        private void ChooseRange(string message, Action<IVertex> method)
        {
            Console.WriteLine(message);
            Point point = ChoosePoint();
            method(graph[point.X, point.Y]);
        }

        private Point ChoosePoint()
        {
            Point point = Input.InputPoint(graph.Width, graph.Height);
            while (!graph[point.X, point.Y].IsValidToBeRange())
                point = Input.InputPoint(graph.Width, graph.Height);
            return point;
        }
    }
}
