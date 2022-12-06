using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    using GraphAssemble = IGraphAssemble<Graph2D<Vertex>, Vertex>;

    [MenuColumnsNumber(2)]
    internal sealed class GraphCreatingViewModel : SafeViewModel, IRequireIntInput, IDisposable
    {
        private readonly IMessenger messenger;
        private readonly IRandom random;
        private readonly IVertexCostFactory costFactory;
        private readonly INeighborhoodFactory neighborhoodFactory;

        private int width;
        private int length;
        private int obstaclePercent;
        private GraphAssemble selectedGraphAssemble;
        private InclusiveValueRange<int> costRange = new InclusiveValueRange<int>(9, 1);

        public IReadOnlyList<GraphAssemble> GraphAssembles { get; set; }

        public IInput<int> IntInput { get; set; }
      
        public GraphCreatingViewModel(IMessenger messenger, IRandom random, IVertexCostFactory costFactory,
            INeighborhoodFactory neighborhoodFactory, ILog log)
            : base(log)
        {
            this.costFactory = costFactory;
            this.neighborhoodFactory = neighborhoodFactory;
            this.messenger = messenger;
            this.random = random;
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphSizeSet), 1)]
        [Condition(nameof(IsAssembleChosen))]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        private void CreateGraph()
        {
            var layers = CreateLayers();
            var graph = selectedGraphAssemble.AssembleGraph(layers, width, length);
            messenger.Send(new CostRangeChangedMessage(costRange));
            messenger.Send(new GraphCreatedMessage(graph));
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.Screen);
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.MainViewModel);
        }

        [MenuItem(MenuItemsNames.ChooseGraphAssemble, 1)]
        private void ChooseGraphAssemble()
        {
            var menuList = GraphAssembles.CreateMenuList(columnsNumber: 1);
            var range = new InclusiveValueRange<int>(GraphAssembles.Count, 1);
            string message = MessagesTexts.GraphAssembleChoiceMsg;
            using (Cursor.CleanUpAfter())
            {
                menuList.Display();
                int index = IntInput.Input(message, range) - 1;
                selectedGraphAssemble = GraphAssembles[index];
            }
        }

        [MenuItem(MenuItemsNames.InputGraphParametres, 2)]
        private void InputGraphParametres()
        {
            using (Cursor.CleanUpAfter())
            {
                width = IntInput.Input(MessagesTexts.GraphWidthInputMsg, Constants.GraphWidthValueRange);
                length = IntInput.Input(MessagesTexts.GraphHeightInputMsg, Constants.GraphLengthValueRange);
            }
        }

        [MenuItem(MenuItemsNames.InputCostRange, 3)]
        private void InputVertexCostRange()
        {
            using (Cursor.CleanUpAfter())
            {
                costRange = IntInput.InputRange(Constants.VerticesCostRange);
            }
        }

        [MenuItem(MenuItemsNames.InputObstaclePercent, 4)]
        private void InputObstaclePercent()
        {
            using (Cursor.CleanUpAfter())
            {
                obstaclePercent = IntInput.Input(MessagesTexts.ObstaclePercentInputMsg, Constants.ObstaclesPercentValueRange);
            }
        }

        private IReadOnlyCollection<ILayer<Graph2D<Vertex>, Vertex>> CreateLayers()
        {
            return new ILayer<Graph2D<Vertex>, Vertex>[]
            {
                new ObstacleLayer<Graph2D<Vertex>, Vertex>(random, obstaclePercent),
                new NeighborhoodLayer<Graph2D<Vertex>, Vertex>(neighborhoodFactory),
                new VertexCostLayer<Graph2D<Vertex>, Vertex>(costFactory, costRange, random)
            };
        }

        [FailMessage(MessagesTexts.AssebleIsNotChosenMsg)]
        private bool IsAssembleChosen()
        {
            return selectedGraphAssemble != null;
        }

        [FailMessage(MessagesTexts.GraphSizeIsNotSetMsg)]
        private bool IsGraphSizeSet()
        {
            return Constants.GraphWidthValueRange.Contains(width)
                && Constants.GraphLengthValueRange.Contains(length);
        }
    }
}