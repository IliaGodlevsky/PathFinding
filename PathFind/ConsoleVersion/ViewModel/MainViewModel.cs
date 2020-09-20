using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using System;
using GraphLibrary.ViewModel;
using ConsoleVersion.Model.EventHolder;
using GraphLibrary.Extensions.CustomTypeExtensions;
using ConsoleVersion.Model.Vertex;
using GraphLibrary.GraphFactory;
using GraphLibrary.Graphs.Interface;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : AbstractMainModel
    {
        public MainViewModel() : base()
        {            
            GraphParametresFormat = ConsoleVersionResources.GraphParametresFormat;

            VertexEventHolder = new ConsoleVertexEventHolder();

            graphFieldFactory = new ConsoleGraphFieldFactory();

            generator = (dto) => new ConsoleVertex(dto);

            var factory = new GraphFactory(new GraphParametres(width: 25, height: 25, obstaclePercent: 25));
            Graph = factory.GetGraph(() => new ConsoleVertex());
            GraphParametres = Graph.GetFormattedInfo(GraphParametresFormat);
            VertexEventHolder.Graph = Graph;
        }

        public override void CreateNewGraph()
        {
            var model = new CreateGraphViewModel(this);
            var view = new GraphCreateView(model);
            view.Start();
        }

        public override void FindPath()
        {
            var model = new PathFindViewModel(this);
            var view = new PathFindView(model);
            view.Start();
        }

        public void Reverse()
        {
            if (Graph == null)
                return;
            var point = Input.InputPoint(Graph.Width, Graph.Height);
            VertexEventHolder.ReversePolarity(Graph[point.X, point.Y], new EventArgs());
        }

        public void ChangeVertexValue()
        {
            if (Graph == null)
                return;
            var point = Input.InputPoint(Graph.Width, Graph.Height);
            while (Graph[point.X, point.Y].IsObstacle)
                point = Input.InputPoint(Graph.Width, Graph.Height);
            VertexEventHolder.ChangeVertexValue(Graph[point.X, point.Y], new EventArgs());
        }

        protected override string GetSavePath() => GetPath();

        protected override string GetLoadPath() => GetPath();

        private string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }
    }
}
