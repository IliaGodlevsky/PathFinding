using Common.ValueRanges;
using ConsoleVersion.InputClass;
using ConsoleVersion.View;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;

namespace ConsoleVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel
    {
        public string ObstaclePercentInputMessage { private get; set; }

        public string WidthInputMessage { private get; set; }

        public string HeightInputMessage { private get; set; }

        public GraphCreatingViewModel(IMainModel model, 
            IGraphFactory graphFactory) : base(model, graphFactory)
        {

        }

        public override void CreateGraph()
        {
            ObstaclePercent = Input.InputNumber(ObstaclePercentInputMessage,
                Range.ObstaclePercentValueRange.UpperValueOfRange,
                Range.ObstaclePercentValueRange.LowerValueOfRange);

            Width = Input.InputNumber(WidthInputMessage,
                Range.WidthValueRange.UpperValueOfRange,
                Range.WidthValueRange.LowerValueOfRange);

            Length = Input.InputNumber(HeightInputMessage,
                Range.HeightValueRange.UpperValueOfRange,
                Range.HeightValueRange.LowerValueOfRange);

            base.CreateGraph();

            MainView.UpdatePositionOfVisualElements(model.Graph);
        }
    }
}
