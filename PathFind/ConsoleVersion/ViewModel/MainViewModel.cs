using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLibrary.ViewModel;
using ConsoleVersion.Model.EventHolder;
using ConsoleVersion.Model.Vertex;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : MainModel
    {
        public MainViewModel() : base()
        {            
            GraphParametresFormat = ConsoleVersionResources.GraphParametresFormat;
            VertexEventHolder = new ConsoleVertexEventHolder();
            graphFieldFactory = new ConsoleGraphFieldFactory();
            dtoConverter = (dto) => new ConsoleVertex(dto);
        }

        public override void CreateNewGraph()
        {
            var model = new GraphCreatingViewModel(this);
            var view = new GraphCreateView(model);
            view.Start();
        }

        public override void FindPath()
        {
            var model = new PathFindingViewModel(this);
            var view = new PathFindView(model);
            view.Start();
        }

        public void Reverse()
        {
            var point = Input.InputPoint(Graph.Width, Graph.Height);
            (Graph[point] as ConsoleVertex).ChangeRole();
        }

        public void ChangeVertexValue()
        {
            var point = Input.InputPoint(Graph.Width, Graph.Height);
            while (Graph[point].IsObstacle)
                point = Input.InputPoint(Graph.Width, Graph.Height);
            (Graph[point] as ConsoleVertex).ChangeCost();
        }

        protected override string GetSavePath() => GetPath();

        protected override string GetLoadPath() => GetPath();

        private string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }

        public void DisplayGraph()
        {
            Console.Clear();
            Console.ForegroundColor = Color.White;
            Console.WriteLine(GraphParametres);
            (GraphField as ConsoleGraphField)?.ShowGraphWithFrames();
            Console.WriteLine(Statistics);
        }
    }
}
