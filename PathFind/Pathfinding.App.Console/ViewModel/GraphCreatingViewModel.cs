using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Delegates;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Logging.Interface;
using Shared.Collections;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphCreatingViewModel : ViewModel, IRequireIntInput, IDisposable
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

        [Order(0)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(CanCreateGraph))]
        [MenuItem(MenuItemsNames.CreateNewGraph)]
        private void CreateGraph()
        {
            var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, Width, Length);
            messenger.Send(new GraphCreatedMessage(graph));
            throw new Exception();
        }

        [Order(1)]
        [MenuItem(MenuItemsNames.ChooseGraphAssemble)]
        private void ChooseGraphAssemble()
        {
            int graphAssembleIndex = IntInput.Input(GraphAssembleInpuMessage, graphAssembleKeyRange) - 1;
            SelectedGraphAssemble = GraphAssembles[graphAssembleIndex];
        }

        [Order(2)]
        [MenuItem(MenuItemsNames.InputGraphParametres)]
        public void InputGraphParametres()
        {
            Width = IntInput.Input(MessagesTexts.GraphWidthInputMsg, Constants.GraphWidthValueRange);
            Length = IntInput.Input(MessagesTexts.GraphHeightInputMsg, Constants.GraphLengthValueRange);
        }

        [Order(3)]
        [MenuItem(MenuItemsNames.InputObstaclePercent)]
        public void InputObstaclePercent()
        {
            ObstaclePercent = IntInput.Input(MessagesTexts.ObstaclePercentInputMsg, Constants.ObstaclesPercentValueRange);
        }

        [Order(4)]
        [MenuItem(MenuItemsNames.Exit)]
        private void Interrupt()
        {
            RaiseViewClosed();
        }

        private bool CanCreateGraph()
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }
    }
}