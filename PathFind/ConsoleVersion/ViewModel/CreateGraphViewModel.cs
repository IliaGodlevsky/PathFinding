using ConsoleVersion.GraphFactory;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;

namespace ConsoleVersion.ViewModel
{
    public class CreateGraphViewModel : AbstractCreateGraphModel
    {
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

            int obstacles = Input.InputNumber(Res.PercentMsg, MAX_PERCENT_OF_OBSTACLES);
            int width = Input.InputNumber(Res.WidthMsg, MAX_GRAPH_WIDTH, MIN_GRAPH_WIDTH);
            int height = Input.InputNumber(Res.HeightMsg, MAX_GRAPH_HEIGHT, MIN_GRAPH_HEIGHT);
            return new ConsoleGraphFactory(obstacles, width, height);
        }
    }
}
