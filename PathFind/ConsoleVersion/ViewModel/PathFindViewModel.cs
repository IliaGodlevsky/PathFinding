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
            pathAlgorithm.Pauser = new PauseProvider() { PauseEvent = () => { } };
        }

        protected override void ShowMessage(string message)
        {
            GraphShower.DisplayGraph(mainViewModel);
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
