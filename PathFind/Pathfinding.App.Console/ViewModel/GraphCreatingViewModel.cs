using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Delegates;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.MenuCommands.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Logging.Interface;
using Shared.Collections;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    internal sealed class GraphCreatingViewModel : SafeViewModel, IRequireIntInput, IDisposable
    {
        private readonly IMessenger messenger;
        private readonly InclusiveValueRange<int> graphAssembleKeyRange;

        private int Width { get; set; }

        private int Length { get; set; }

        private int ObstaclePercent { get; set; }

        public ReadOnlyList<IGraphAssemble<Graph2D<Vertex>, Vertex>> GraphAssembles { get; }

        public string GraphAssembleInpuMessage { private get; set; }

        public IInput<int> IntInput { get; set; }

        private IGraphAssemble<Graph2D<Vertex>, Vertex> SelectedGraphAssemble { get; set; }

        public GraphCreatingViewModel(IEnumerable<IGraphAssemble<Graph2D<Vertex>, Vertex>> graphAssembles, ILog log)
            : base(log)
        {
            GraphAssembles = graphAssembles.ToReadOnly();
            graphAssembleKeyRange = new InclusiveValueRange<int>(graphAssembles.Count(), 1);
            messenger = DI.Container.Resolve<IMessenger>();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsAssembleChosen))]
        [Condition(nameof(IsGraphSizeSet), 1)]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        private void CreateGraph()
        {
            var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, Width, Length);
            messenger.Send(new GraphCreatedMessage(graph));
        }

        [MenuItem(MenuItemsNames.ChooseGraphAssemble, 1)]
        private void ChooseGraphAssemble()
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

        [FailMessage(MessagesTexts.AssebleIsNotChosen)]
        private bool IsAssembleChosen()
        {
            return SelectedGraphAssemble != null;
        }

        [FailMessage(MessagesTexts.GraphSizeIsNotSet)]
        private bool IsGraphSizeSet()
        {
            return Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }
    }
}