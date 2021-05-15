using AssembleClassesLib.Interface;
using ConsoleVersion.View;
using GraphLib.Exceptions;
using GraphLib.Realizations.Graphs;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
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

        public GraphCreatingViewModel(ILog log, IMainModel model, IAssembleClasses graphFactories)
            : base(log, model, graphFactories)
        {
            maxGraphAssembleKeyNumber = graphFactories.ClassesNames.Count;
            minGraphAssembleKeyNumber = 1;
        }

        public override void CreateGraph()
        {
            int graphAssembleIndex = GetGraphAssembleIndex();
            var graphAssembleKeys = graphAssembleClasses.ClassesNames;
            GraphAssembleKey = graphAssembleKeys.ElementAt(graphAssembleIndex);

            ObstaclePercent = InputNumber(ObstaclePercentInputMessage, ObstaclesPercentValueRange);
            Width = InputNumber(WidthInputMessage, GraphWidthValueRange);
            Length = InputNumber(HeightInputMessage, GraphLengthValueRange);

            base.CreateGraph();

            if (!(model.Graph is Graph2D))
            {
                string message = "An error occurred while creating graph\n";
                message += $"Graph must be {nameof(Graph2D)} type";
                throw new WrongGraphTypeException(message, model.Graph);
            }

            MainView.UpdatePositionOfVisualElements(model.Graph);
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
