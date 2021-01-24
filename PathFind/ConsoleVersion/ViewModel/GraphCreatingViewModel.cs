using Common.ValueRanges;
using ConsoleVersion.InputClass;
using ConsoleVersion.View;
using GraphLib.Vertex.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;

namespace ConsoleVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel
    {
        public string ObstaclePercentInputMessage { private get; set; }

        public string WidthInputMessage { private get; set; }

        public string HeightInputMessage { private get; set; }

        public GraphCreatingViewModel(IMainModel model) : base(model)
        {

        }

        public override void CreateGraph(Func<IVertex> generator)
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

            base.CreateGraph(generator);

            MainView.UpdatePositionOfVisualElements(model.Graph);
        }
    }
}
