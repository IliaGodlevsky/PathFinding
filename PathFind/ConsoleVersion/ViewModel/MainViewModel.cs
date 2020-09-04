using ConsoleVersion.GraphFactory;
using ConsoleVersion.GraphLoader;
using ConsoleVersion.GraphSaver;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.EventHolder;
using ConsoleVersion.View;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Model;
using System;
using System.Drawing;

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
