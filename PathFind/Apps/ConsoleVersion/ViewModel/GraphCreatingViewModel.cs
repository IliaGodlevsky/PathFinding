using Common.ValueRanges;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using static ConsoleVersion.Constants;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphCreatingViewModel : GraphCreatingModel, IModel, IInterruptable, IRequireInt32Input
    {
        public event ProcessEventHandler Interrupted;

        public string GraphAssembleInpuMessage { private get; set; }

        public IValueInput<int> Int32Input { get; set; }

        public GraphCreatingViewModel(ILog log, IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, graphAssembles)
        {
            graphAssembleKeyRange = new InclusiveValueRange<int>(graphAssembles.Count(), 1);
        }

        [MenuItem(MenuItemsNames.CreateNewGraph, MenuItemPriority.Highest)]
        public override void CreateGraph()
        {
            if (CanCreateGraph())
            {
                try
                {
                    var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, GraphParametres);
                    var message = new GraphCreatedMessage(graph);
                    Messenger.Default.SendMany(message, MessageTokens.Everyone);
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
            var graphAssembleKeys = GraphAssembles.Keys.ToArray();
            string selectedGraphAssembleKey = graphAssembleKeys[graphAssembleIndex];
            SelectedGraphAssemble = GraphAssembles[selectedGraphAssembleKey];
        }

        [MenuItem(MenuItemsNames.InputGraphParametres, MenuItemPriority.High)]
        public void InputGraphParametres()
        {
            Width = Int32Input.InputValue(MessagesTexts.GraphWidthInputMsg, GraphWidthValueRange);
            Length = Int32Input.InputValue(MessagesTexts.GraphHeightInputMsg, GraphLengthValueRange);
        }

        [MenuItem(MenuItemsNames.InputObstaclePercent, MenuItemPriority.Low)]
        public void InputObstaclePercent()
        {
            ObstaclePercent = Int32Input.InputValue(MessagesTexts.ObstaclePercentInputMsg, ObstaclesPercentValueRange);
        }

        [MenuItem(MenuItemsNames.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            Interrupted?.Invoke(this, new ProcessEventArgs());
            Interrupted = null;
        }

        private bool CanCreateGraph()
        {
            return SelectedGraphAssemble != null
                && GraphWidthValueRange.Contains(Width)
                && GraphLengthValueRange.Contains(Length);
        }

        private readonly InclusiveValueRange<int> graphAssembleKeyRange;
    }
}