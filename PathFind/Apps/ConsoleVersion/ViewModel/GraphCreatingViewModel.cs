using Common;
using ConsoleVersion.InputClass;
using ConsoleVersion.View;
using GraphLib.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Configuration;

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
            int upperRangeOfGraphWidth
                = Convert.ToInt32(ConfigurationManager.AppSettings["upperRangeOfGraphWidth"]);
            int upperRangeOfGraphLength
                = Convert.ToInt32(ConfigurationManager.AppSettings["upperRangeOfGraphLength"]);

            GraphWidthValueRange = new ValueRange(upperRangeOfGraphWidth, 0);
            GraphLengthValueRange = new ValueRange(upperRangeOfGraphLength, 0);
        }

        public override void CreateGraph()
        {
            ObstaclePercent = Input.InputNumber(ObstaclePercentInputMessage,
                ObstaclePercentValueRange.UpperValueOfRange,
                ObstaclePercentValueRange.LowerValueOfRange);

            Width = Input.InputNumber(WidthInputMessage, GraphWidthValueRange);

            Length = Input.InputNumber(HeightInputMessage, GraphLengthValueRange);

            base.CreateGraph();

            MainView.UpdatePositionOfVisualElements(model.Graph);
        }
    }
}
