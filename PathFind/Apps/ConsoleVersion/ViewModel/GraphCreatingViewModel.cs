using ConsoleVersion.View;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System.Collections.Generic;
using System.Linq;
using static ConsoleVersion.Constants;
using static ConsoleVersion.InputClass.Input;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphCreatingViewModel : GraphCreatingModel
    {
        public string GraphAssembleInpuMessage { private get; set; }

        public string ObstaclePercentInputMessage { private get; set; }

        public string WidthInputMessage { private get; set; }

        public string HeightInputMessage { private get; set; }

        public GraphCreatingViewModel(ILog log, IMainModel model, IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, model, graphAssembles)
        {
            maxGraphAssembleKeyNumber = graphAssembles.Count();
            minGraphAssembleKeyNumber = 1;
        }

        public override void CreateGraph()
        {
            int graphAssembleIndex = GetGraphAssembleIndex();
            var graphAssembleKeys = GraphAssembles.Keys.ToArray();
            string selectedGraphAssembleKey = graphAssembleKeys[graphAssembleIndex];
            SelectedGraphAssemble = GraphAssembles[selectedGraphAssembleKey];

            ObstaclePercent = InputNumber(ObstaclePercentInputMessage, ObstaclesPercentValueRange);
            Width = InputNumber(WidthInputMessage, GraphWidthValueRange);
            Length = InputNumber(HeightInputMessage, GraphLengthValueRange);

            base.CreateGraph();
        }

        private int GetGraphAssembleIndex()
        {
            return InputNumber(
                GraphAssembleInpuMessage,
                maxGraphAssembleKeyNumber,
                minGraphAssembleKeyNumber) - 1;
        }

        private readonly int maxGraphAssembleKeyNumber;
        private readonly int minGraphAssembleKeyNumber;
    }
}
