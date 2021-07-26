using Common.ValueRanges;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using System.Collections.Generic;
using System.Linq;
using static ConsoleVersion.Constants;
using static ConsoleVersion.InputClass.Input;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphCreatingViewModel : GraphCreatingModel, IInterruptable
    {
        public event InterruptEventHanlder OnInterrupted;

        public string GraphAssembleInpuMessage { private get; set; }

        public string ObstaclePercentInputMessage { private get; set; }

        public string WidthInputMessage { private get; set; }

        public string HeightInputMessage { private get; set; }

        public bool IsInterruptRequested { get; private set; }

        public GraphCreatingViewModel(ILog log, IMainModel model, IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, model, graphAssembles)
        {
            graphAssembleKeyRange = new InclusiveValueRange<int>(graphAssembles.Count(), 1);
            IsInterruptRequested = false;
        }

        [MenuItem(Constants.CreateNewGraph, MenuItemPriority.Highest)]
        public override void CreateGraph()
        {
            if (CanCreateGraph())
            {
                base.CreateGraph();
            }
            else
            {
                log.Warn("Not enough parametres to create graph");
            }
        }

        [MenuItem(Constants.ChooseGraphAssemble, MenuItemPriority.High)]
        public void ChooseGraphAssemble()
        {
            int graphAssembleIndex = InputNumber(GraphAssembleInpuMessage, graphAssembleKeyRange) - 1;
            var graphAssembleKeys = GraphAssembles.Keys.ToArray();
            string selectedGraphAssembleKey = graphAssembleKeys[graphAssembleIndex];
            SelectedGraphAssemble = GraphAssembles[selectedGraphAssembleKey];
        }

        [MenuItem(Constants.InputGraphParametres, MenuItemPriority.High)]
        public void InputGraphParametres()
        {
            Width = InputNumber(WidthInputMessage, GraphWidthValueRange);
            Length = InputNumber(HeightInputMessage, GraphLengthValueRange);
        }

        [MenuItem(Constants.InputObstaclePercent, MenuItemPriority.Low)]
        public void InputObstaclePercent()
        {
            ObstaclePercent = InputNumber(ObstaclePercentInputMessage, ObstaclesPercentValueRange);
        }

        [MenuItem(Constants.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            IsInterruptRequested = true;
            OnInterrupted?.Invoke(this, new InterruptEventArgs());
            OnInterrupted = null;
        }

        private bool CanCreateGraph()
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }

        private readonly InclusiveValueRange<int> graphAssembleKeyRange;
    }
}