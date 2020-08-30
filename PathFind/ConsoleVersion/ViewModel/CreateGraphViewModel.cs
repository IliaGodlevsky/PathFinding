using ConsoleVersion.GraphFactory;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using GraphLibrary.Common.Constants;
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
            int obstacles = Input.InputNumber(Messages.Item1, 
                GraphParametresRange.UpperObstacleValue, 
                GraphParametresRange.LowerObstacleValue);

            int width = Input.InputNumber(Messages.Item2, 
                GraphParametresRange.UpperWidthValue, 
                GraphParametresRange.LowerWidthValue);

            int height = Input.InputNumber(Messages.Item3, 
                GraphParametresRange.UpperHeightValue, 
                GraphParametresRange.LowerHeightValue);

            return new ConsoleGraphFactory(obstacles, width, height);
        }
    }
}
