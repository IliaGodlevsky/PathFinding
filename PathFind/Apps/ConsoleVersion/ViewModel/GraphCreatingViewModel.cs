using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.ValueInput;
using GalaSoft.MvvmLight.Messaging;
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
    internal sealed class GraphCreatingViewModel : GraphCreatingModel, IViewModel, IRequireInt32Input, IDisposable
    {
        public event Action WindowClosed;

        public string GraphAssembleInpuMessage { private get; set; }

        public ConsoleValueInput<int> Int32Input { get; set; }

        public GraphCreatingViewModel(IEnumerable<IGraphAssemble> graphAssembles, ILog log)
            : base(log, graphAssembles)
        {
            graphAssembleKeyRange = new InclusiveValueRange<int>(graphAssembles.Count(), 1);
            messenger = DI.Container.Resolve<IMessenger>();
        }

        [MenuItem(MenuItemsNames.CreateNewGraph, MenuItemPriority.Highest)]
        public override void CreateGraph()
        {
            if (CanCreateGraph())
            {
                try
                {
                    var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, Width, Length);
                    var token = MessageTokens.MainModel | MessageTokens.MainView;
                    messenger.Forward(new GraphCreatedMessage(graph), token);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            else
            {
                log.Warn(MessagesTexts.NotEnoughParamtres);
            }
        }

        [MenuItem(MenuItemsNames.ChooseGraphAssemble, MenuItemPriority.High)]
        public void ChooseGraphAssemble()
        {
            int graphAssembleIndex = Int32Input.InputValue(GraphAssembleInpuMessage, graphAssembleKeyRange) - 1;
            string selectedGraphAssembleKey = GraphAssembles.Keys.ElementAt(graphAssembleIndex);
            SelectedGraphAssemble = GraphAssembles[selectedGraphAssembleKey];
        }

        [MenuItem(MenuItemsNames.InputGraphParametres, MenuItemPriority.High)]
        public void InputGraphParametres()
        {
            Width = Int32Input.InputValue(MessagesTexts.GraphWidthInputMsg, Constants.GraphWidthValueRange);
            Length = Int32Input.InputValue(MessagesTexts.GraphHeightInputMsg, Constants.GraphLengthValueRange);
        }

        [MenuItem(MenuItemsNames.InputObstaclePercent, MenuItemPriority.Normal)]
        public void InputObstaclePercent()
        {
            ObstaclePercent = Int32Input.InputValue(MessagesTexts.ObstaclePercentInputMsg, Constants.ObstaclesPercentValueRange);
        }

        [MenuItem(MenuItemsNames.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        private bool CanCreateGraph()
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private readonly IMessenger messenger;
        private readonly InclusiveValueRange<int> graphAssembleKeyRange;
    }
}