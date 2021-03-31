using ConsoleVersion.View;
using GraphLib.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;

using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Constants;

namespace ConsoleVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel
    {
        public string ObstaclePercentInputMessage { private get; set; }

        public string WidthInputMessage { private get; set; }

        public string HeightInputMessage { private get; set; }

        public GraphCreatingViewModel(IMainModel model,
            IGraphAssembler graphFactory) : base(model, graphFactory)
        {

        }

        public override void CreateGraph()
        {
            ObstaclePercent = InputNumber(ObstaclePercentInputMessage, ObstaclesPercentValueRange);
            Width = InputNumber(WidthInputMessage, GraphWidthValueRange);
            Length = InputNumber(HeightInputMessage, GraphLengthValueRange);

            base.CreateGraph();

            MainView.UpdatePositionOfVisualElements(model.Graph);
        }
    }
}
