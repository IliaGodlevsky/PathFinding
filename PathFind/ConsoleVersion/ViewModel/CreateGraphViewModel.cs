using ConsoleVersion.GraphFactory;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using System;

namespace ConsoleVersion.ViewModel
{
    internal class CreateGraphViewModel : AbstractCreateGraphModel
    {
        public Tuple<string,string,string> Messages { get; set; }

        public CreateGraphViewModel(IMainModel model) : base(model)
        {
            filler = new ConsoleGraphFiller();            
        }

        public override IGraphFactory GetFactory()
        {
            const int MAX_PERCENT_OF_OBSTACLES = 100;
            const int MAX_GRAPH_WIDTH = 100;
            const int MAX_GRAPH_HEIGHT = MAX_GRAPH_WIDTH;
            const int MIN_GRAPH_WIDTH = 10;
            const int MIN_GRAPH_HEIGHT = MIN_GRAPH_WIDTH;

            int obstacles = Input.InputNumber(Messages.Item1, MAX_PERCENT_OF_OBSTACLES);
            int width = Input.InputNumber(Messages.Item2, MAX_GRAPH_WIDTH, MIN_GRAPH_WIDTH);
            int height = Input.InputNumber(Messages.Item3, MAX_GRAPH_HEIGHT, MIN_GRAPH_HEIGHT);
            return new ConsoleGraphFactory(obstacles, width, height);
        }
    }
}
