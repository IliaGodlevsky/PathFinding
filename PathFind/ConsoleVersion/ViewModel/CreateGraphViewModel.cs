using ConsoleVersion.InputClass;
using ConsoleVersion.Model.GraphFactory;
using GraphLibrary.GraphFactory;
using GraphLibrary.ValueRanges;
using GraphLibrary.ViewModel;
using GraphLibrary.ViewModel.Interface;
using System;

namespace ConsoleVersion.ViewModel
{
    internal class CreateGraphViewModel : AbstractCreateGraphModel
    {
        public Tuple<string,string,string> Messages { get; set; }

        public CreateGraphViewModel(IMainModel model) : base(model)
        {

        }

        public override IGraphFactory GetFactory()
        {
            ObstaclePercent = Input.InputNumber(Messages.Item1, 
                Range.ObstaclePercentValueRange.UpperRange,
                Range.ObstaclePercentValueRange.LowerRange);

            Width = Input.InputNumber(Messages.Item2, 
                Range.WidthValueRange.UpperRange,
                Range.WidthValueRange.LowerRange);

            Height = Input.InputNumber(Messages.Item3,
                Range.HeightValueRange.UpperRange,
                Range.HeightValueRange.LowerRange);

            return new ConsoleGraphFactory(ObstaclePercent, Width, Height);
        }
    }
}
