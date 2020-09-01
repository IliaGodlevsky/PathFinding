using ConsoleVersion.GraphFactory;
using ConsoleVersion.GraphLoader;
using ConsoleVersion.GraphSaver;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.StatusSetter;
using ConsoleVersion.View;
using GraphLibrary;
using GraphLibrary.Model;
using GraphLibrary.StatusSetter;
using System;
using System.Drawing;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : AbstractMainModel
    {
        private readonly IVertexStatusSetter changer;

        public MainViewModel()
        {            
            Format = ConsoleVersionResources.GraphParametresFormat;
            saver = new ConsoleGraphSaver();
            loader = new ConsoleGraphLoader();
            filler = new ConsoleGraphFiller();

            var factory = new ConsoleGraphFactory(
                percentOfObstacles: 25, width: 25, height: 25);
            Graph = factory.GetGraph();
            GraphParametres = GraphParametresPresenter.GetFormattedData(Graph, Format);
            changer = new ConsoleVertexStatusSetter(Graph);
        }

        public override void CreateNewGraph()
        {
            var model = new CreateGraphViewModel(this);
            var view = new GraphCreateView(model);
            view.Start();
        }

        public override void PathFind()
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

        public void Reverse() => VertexChange(changer.ReversePolarity);

        public void ChangeVertexValue() => VertexChange(changer.ChangeVertexValue);
    }
}
