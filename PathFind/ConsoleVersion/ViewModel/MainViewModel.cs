using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using System;
using System.Drawing;
using GraphLibrary.Extensions;
using GraphLibrary.ViewModel;
using ConsoleVersion.Model.GraphSaver;
using ConsoleVersion.Model.GraphLoader;
using ConsoleVersion.Model.GraphFactory;
using ConsoleVersion.Model.EventHolder;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : AbstractMainModel
    {
        public MainViewModel()
        {            
            GraphParametresFormat = ConsoleVersionResources.GraphParametresFormat;
            saver = new ConsoleGraphSaver();
            loader = new ConsoleGraphLoader();
            VertexEventHolder = new ConsoleVertexEventHolder();
            graphFieldFiller = new ConsoleGraphFieldFiller();
            var factory = new ConsoleGraphFactory(
                percentOfObstacles: 25, width: 25, height: 25);
            Graph = factory.GetGraph();
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
    }
}
