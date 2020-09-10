using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using System;
using System.Drawing;
using GraphLibrary.ViewModel;
using ConsoleVersion.Model.EventHolder;
using GraphLibrary.GraphSerialization.GraphSaver;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphSerialization.GraphLoader;
using ConsoleVersion.Model.Vertex;
using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : AbstractMainModel
    {
        public MainViewModel()
        {            
            GraphParametresFormat = ConsoleVersionResources.GraphParametresFormat;
            saver = new GraphSaver();
            saver.OnBadSave += message => { Console.WriteLine(message); Console.ReadKey(); };
            loader = new GraphLoader();
            VertexEventHolder = new ConsoleVertexEventHolder();
            graphFieldFiller = new ConsoleGraphFieldFactory();
            var factory = new GraphFactory(percentOfObstacles: 25, width: 25, 
                height: 25, VertexSize.SIZE_BETWEEN_VERTICES);
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

        private void VertexChange(EventHandler method)
        {
            if (Graph == null)
                return;
            Point point = Input.InputPoint(Graph.Width, Graph.Height);
            method(Graph[point.X, point.Y], new EventArgs());
        }

        public void Reverse() => VertexChange(VertexEventHolder.ReversePolarity);

        public void ChangeVertexValue() => VertexChange(VertexEventHolder.ChangeVertexValue);

        protected override string GetSavePath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }

        protected override string GetLoadPath()
        {
            return GetSavePath();
        }

        public void LoadGraph() => base.LoadGraph(dto => new ConsoleVertex(dto));
    }
}
