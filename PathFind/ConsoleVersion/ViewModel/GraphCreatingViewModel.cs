using Common.ValueRanges;
using ConsoleVersion.InputClass;
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
            ObstaclePercent = Input.InputNumber(
                ObstaclePercentInputMessage,
                Range.ObstaclePercentValueRange.UpperRange,
                Range.ObstaclePercentValueRange.LowerRange);

            Width = Input.InputNumber(
                WidthInputMessage,
                Range.WidthValueRange.UpperRange,
                Range.WidthValueRange.LowerRange);

            Length = Input.InputNumber(
                HeightInputMessage,
                Range.HeightValueRange.UpperRange,
                Range.HeightValueRange.LowerRange);

            base.CreateGraph(generator);
        }
    }
}
