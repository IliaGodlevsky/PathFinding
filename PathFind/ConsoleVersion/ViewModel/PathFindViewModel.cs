using ConsoleVersion.Forms;
using ConsoleVersion.InputClass;
using GraphLibrary.PauseMaker;
using System;
using System.Drawing;
using GraphLibrary.Enums;
using GraphLibrary.EventHolder;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using GraphLibrary.ViewModel;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.ValueRanges;
using System.Threading;
using System.Linq;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindViewModel : AbstractPathFindModel
    {
        public Tuple<string, string, string> Messages { get; set; }

        public PathFindViewModel(IMainModel model) : base(model)
        {
            eventHolder = model.VertexEventHolder;
            var algorithmEnums = Enum.GetValues(typeof(Algorithms)).Cast<byte>();
            maxAlgorithmValue = algorithmEnums.Last();
            minAlgorithmValue = algorithmEnums.First();
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
                Range.DelayValueRange.UpperRange, Range.DelayValueRange.LowerRange);
            var thread = new Thread(() => 
            {
                while (true)
                {
                    Thread.Sleep(millisecondsTimeout: 135);
                    GraphShower.DisplayGraph(mainViewModel);
                }
            });

            var pauser = new PauseProvider(DelayTime) { PauseEvent = () => { } };

            pathAlgorithm.OnStarted += (sender, eventArgs) => thread.Start();
            pathAlgorithm.OnVertexVisited += (vertex) => pauser.Pause();

            pathAlgorithm.OnFinished += (sender, eventArgs) =>
            {
                thread.Abort();
                thread.Join();
                Console.ForegroundColor = ConsoleColor.Gray;
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
                maxAlgorithmValue, minAlgorithmValue);
        }

        private void ChooseStart() => ChooseRange(Messages.Item1, eventHolder.SetStartVertex);

        private void ChooseEnd() => ChooseRange(Messages.Item2, eventHolder.SetDestinationVertex);

        private void ChooseRange(string message, Action<IVertex> method)
        {
            Console.WriteLine(message);
            var point = ChoosePoint();
            method(graph[point.X, point.Y]);
        }

        private Point ChoosePoint()
        {
            var point = Input.InputPoint(graph.Width, graph.Height);
            while (!graph[point.X, point.Y].IsValidToBeRange())
                point = Input.InputPoint(graph.Width, graph.Height);
            return point;
        }

        private readonly AbstractVertexEventHolder eventHolder;
        private readonly byte maxAlgorithmValue;
        private readonly byte minAlgorithmValue;
    }
}
