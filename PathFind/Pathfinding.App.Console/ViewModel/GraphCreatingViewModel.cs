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
using Pathfinding.Visualization.Models;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphCreatingViewModel : GraphCreatingModel<Graph2D<Vertex>, Vertex>, IViewModel, IRequireIntInput, IDisposable
    {
        public event Action ViewClosed;

        private readonly IMessenger messenger;
        private readonly InclusiveValueRange<int> graphAssembleKeyRange;

        public string GraphAssembleInpuMessage { private get; set; }

        public IInput<int> IntInput { get; set; }

        public GraphCreatingViewModel(IEnumerable<IGraphAssemble<Graph2D<Vertex>, Vertex>> graphAssembles, ILog log)
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
            ViewClosed?.Invoke();
        }

        public void Dispose()
        {
            ViewClosed = null;
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