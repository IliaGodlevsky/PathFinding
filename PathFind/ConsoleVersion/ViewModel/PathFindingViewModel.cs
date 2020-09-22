using ConsoleVersion.Forms;
using ConsoleVersion.InputClass;
using System;
using GraphLibrary.Enums;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using GraphLibrary.ViewModel;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.ValueRanges;
using System.Threading;
using System.Linq;
using GraphLibrary.PauseMaking;
using ConsoleVersion.Model.Vertex;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public Tuple<string, string, string> Messages { get; set; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            var algorithmEnums = Enum.GetValues(typeof(Algorithms)).Cast<byte>();
            maxAlgorithmValue = algorithmEnums.Last();
            minAlgorithmValue = algorithmEnums.First();
        }

        public override void FindPath()
        {
            if (!graph.Any())
                return;
            mainViewModel.ClearGraph();
            GraphShower.DisplayGraph(mainViewModel);
            ChooseExtremeVertex();
            GraphShower.DisplayGraph(mainViewModel);
            Algorithm = GetAlgorithmEnum();
            base.FindPath();
        }

        protected override void PrepareAlgorithm()
        {
            DelayTime = Input.InputNumber(ConsoleVersionResources.DelayTimeMsg, 
                Range.DelayValueRange.UpperRange, Range.DelayValueRange.LowerRange);

            base.PrepareAlgorithm();

            var thread = new Thread(() => 
            {
                while (true)
                {
                    Thread.Sleep(millisecondsTimeout: 135);                    
                    GraphShower.DisplayGraph(mainViewModel);
                }
            });

            var pauser = new PauseProvider(DelayTime);
            pauser.PauseEvent += () => { };

            pathAlgorithm.OnStarted += (sender, eventArgs) => thread.Start();
            pathAlgorithm.OnVertexVisited += (vertex) => pauser.Pause();

            pathAlgorithm.OnFinished += (sender, eventArgs) =>
            {
                thread.Abort();
                thread.Join();
                if (!eventArgs.HasFoundPath)
                {
                    GraphShower.DisplayGraph(mainViewModel);
                    Console.WriteLine(badResultMessage);
                    Console.ReadLine();
                }                
            };         
        }

        private Algorithms GetAlgorithmEnum()
        {
            return (Algorithms)Input.InputNumber(Messages.Item3,
                maxAlgorithmValue, minAlgorithmValue);
        }

        private void ChooseExtremeVertex()
        {           
            const int EXTREME_VERTICES_COUNT = 2;
            string[] chooseMessages = new string[EXTREME_VERTICES_COUNT] 
                    { Messages.Item1, Messages.Item2 };
            for (int i = 0; i < EXTREME_VERTICES_COUNT; i++)
            {
                var point = ChoosePoint(chooseMessages[i]);
                (mainViewModel.Graph[point] as ConsoleVertex).SetAsExtremeVertex();
            }
        }

        private Position ChoosePoint(string message)
        {
            Console.WriteLine(message);
            var point = Input.InputPoint(graph.Width, graph.Height);
            while (!graph[point].IsValidToBeRange())
                point = Input.InputPoint(graph.Width, graph.Height);
            return point;
        }

        private readonly byte maxAlgorithmValue;
        private readonly byte minAlgorithmValue;
    }
}
