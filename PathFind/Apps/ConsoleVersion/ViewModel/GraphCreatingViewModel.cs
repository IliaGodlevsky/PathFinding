using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Delegates;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphCreatingViewModel : GraphCreatingModel, IViewModel, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;
        private readonly InclusiveValueRange<int> graphAssembleKeyRange;

        public string GraphAssembleInpuMessage { private get; set; }

        public IInput<int> IntInput { get; set; }

        public GraphCreatingViewModel(IEnumerable<IGraphAssemble> graphAssembles, ILog log)
            : base(log, graphAssembles)
        {
            graphAssembleKeyRange = new InclusiveValueRange<int>(graphAssembles.Count(), 1);
            messenger = DI.Container.Resolve<IMessenger>();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(CanCreateGraph))]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        public override void CreateGraph()
        {
            var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, Width, Length);
            messenger.Send(new GraphCreatedMessage(graph));
        }

        [MenuItem(MenuItemsNames.ChooseGraphAssemble, 1)]
        public void ChooseGraphAssemble()
        {
            int graphAssembleIndex = IntInput.Input(GraphAssembleInpuMessage, graphAssembleKeyRange) - 1;
            SelectedGraphAssemble = GraphAssembles[graphAssembleIndex];
        }

        [MenuItem(MenuItemsNames.InputGraphParametres, 2)]
        public void InputGraphParametres()
        {
            Width = IntInput.Input(MessagesTexts.GraphWidthInputMsg, Constants.GraphWidthValueRange);
            Length = IntInput.Input(MessagesTexts.GraphHeightInputMsg, Constants.GraphLengthValueRange);
        }

        [MenuItem(MenuItemsNames.InputObstaclePercent, 3)]
        public void InputObstaclePercent()
        {
            ObstaclePercent = IntInput.Input(MessagesTexts.ObstaclePercentInputMsg, Constants.ObstaclesPercentValueRange);
        }

        [MenuItem(MenuItemsNames.Exit, 4)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        private void Route()
        {

        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private bool CanCreateGraph()
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }

        private void ExecuteSafe(Command action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}