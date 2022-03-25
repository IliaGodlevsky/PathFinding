using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [PreValidationMethod(nameof(CanCreateGraph))]
        [MenuItem(0), Description(MenuItemsNames.CreateNewGraph)]
        public override void CreateGraph()
        {
            var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, Width, Length);
            var token = MessageTokens.MainModel | MessageTokens.MainView;
            messenger.Forward(new GraphCreatedMessage(graph), token);
        }

        [MenuItem(1), Description(MenuItemsNames.ChooseGraphAssemble)]
        public void ChooseGraphAssemble()
        {
            int graphAssembleIndex = IntInput.Input(GraphAssembleInpuMessage, graphAssembleKeyRange) - 1;
            string selectedGraphAssembleKey = GraphAssembles.Keys.ElementAt(graphAssembleIndex);
            SelectedGraphAssemble = GraphAssembles[selectedGraphAssembleKey];
        }

        [MenuItem(2), Description(MenuItemsNames.InputGraphParametres)]
        public void InputGraphParametres()
        {
            Width = IntInput.Input(MessagesTexts.GraphWidthInputMsg, Constants.GraphWidthValueRange);
            Length = IntInput.Input(MessagesTexts.GraphHeightInputMsg, Constants.GraphLengthValueRange);
        }

        [MenuItem(3), Description(MenuItemsNames.InputObstaclePercent)]
        public void InputObstaclePercent()
        {
            ObstaclePercent = IntInput.Input(MessagesTexts.ObstaclePercentInputMsg, Constants.ObstaclesPercentValueRange);
        }

        [MenuItem(4), Description(MenuItemsNames.Exit)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
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

        private void ExecuteSafe(Action action)
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