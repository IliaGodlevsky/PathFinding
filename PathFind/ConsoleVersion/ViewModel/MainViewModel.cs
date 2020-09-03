using ConsoleVersion.GraphFactory;
using ConsoleVersion.GraphLoader;
using ConsoleVersion.GraphSaver;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.StatusSetter;
using ConsoleVersion.View;
using GraphFactory.GraphSaver;
using GraphLibrary;
using GraphLibrary.Common.Extensions;
using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphLoader;
using GraphLibrary.Model;
using GraphLibrary.VertexEventHolder;
using System;
using System.Drawing;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : AbstractMainModel
    {
        private readonly IVertexEventHolder changer;

        public MainViewModel()
        {            
            Format = ConsoleVersionResources.GraphParametresFormat;
            saver = new ConsoleGraphSaver();
            loader = new ConsoleGraphLoader();
            vertexEventSetter = new ConsoleVersionVertexEventSetter();
            graphFieldFiller = new ConsoleGraphFieldFiller();
            var factory = new ConsoleGraphFactory(
                percentOfObstacles: 25, width: 25, height: 25);
            Graph = factory.GetGraph();
            GraphParametres = Graph.GetFormattedInfo(Format);
            changer = new ConsoleVertexStatusSetter(Graph);
        }

        public MainViewModel(AbstractVertexEventSetter vertexEventSetter,
            AbstractGraphFieldFiller graphFieldFiller,
            IGraphSaver saver, IGraphLoader loader) :
            base(vertexEventSetter, graphFieldFiller, saver, loader)
        {

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

        public void Reverse() => VertexChange(changer.ReversePolarity);

        public void ChangeVertexValue() => VertexChange(changer.ChangeVertexValue);
    }
}
