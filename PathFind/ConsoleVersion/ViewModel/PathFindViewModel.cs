using ConsoleVersion.Forms;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.StatusSetter;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Extensions;
using GraphLibrary.Model;
using GraphLibrary.PauseMaker;
using System;
using System.Drawing;

namespace ConsoleVersion.ViewModel
{
    internal class PathFindViewModel : AbstractPathFindModel
    {
        private readonly ConsoleVertexStatusSetter changer;
        private readonly ConsoleVersionVertexEventSetter vertexEventSetter;

        public Tuple<string, string, string> Messages { get; set; }

        public PathFindViewModel(IMainModel model) : base(model)
        {
            changer = new ConsoleVertexStatusSetter(model.Graph);
            vertexEventSetter = new ConsoleVersionVertexEventSetter();
        }

        public PathFindViewModel(IMainModel model,
            string badResultMessage, string format) : 
            base(model, badResultMessage, format)
        {

        }

        public override void PathFind()
        {
            vertexEventSetter.RefreshGraph(model.Graph);
            GraphShower.DisplayGraph(model);
            ChooseStart();
            ChooseEnd();
            GraphShower.DisplayGraph(model);
            Algorithm = GetAlgorithmEnum();
            base.PathFind();
        }

        protected override void PrepareAlgorithm()
        {
            pathAlgorithm.Pauser = new PauseProvider() { PauseEvent = () => { } };
        }

        protected override void ShowMessage(string message)
        {
            GraphShower.DisplayGraph(model);
            Console.WriteLine(message);
            Console.ReadLine();
        }

        private Algorithms GetAlgorithmEnum()
        {
            return (Algorithms)Input.InputNumber(Messages.Item3,
                (int)Algorithms.ValueGreedyAlgorithm, 
                (int)Algorithms.WidePathFind);
        }

        private void ChooseStart()
        {
            ChooseRange(Messages.Item1, changer.SetStartVertex);
        }

        private void ChooseEnd()
        {
            ChooseRange(Messages.Item2, changer.SetDestinationVertex);
        }

        private void ChooseRange(string message, EventHandler method)
        {
            Console.WriteLine(message);
            Point point = ChoosePoint();
            method(graph[point.X, point.Y], new EventArgs());
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
