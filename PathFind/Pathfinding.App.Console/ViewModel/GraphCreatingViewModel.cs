using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
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
    using GraphAssemble = IGraphAssemble<Graph2D<Vertex>, Vertex>;

    [MenuColumnsNumber(2)]
    internal sealed class GraphCreatingViewModel : SafeViewModel, IRequireIntInput, IDisposable
    {
        private readonly IMessenger messenger;
        private readonly InclusiveValueRange<int> graphAssembleKeyRange;

        private int Width { get; set; }

        private int Length { get; set; }

        private int ObstaclePercent { get; set; }

        public ReadOnlyList<GraphAssemble> GraphAssembles { get; }

        private GraphAssemble SelectedGraphAssemble { get; set; }

        public string GraphAssembleInpuMessage { private get; set; }

        public IInput<int> IntInput { get; set; }        

        public GraphCreatingViewModel(IEnumerable<GraphAssemble> graphAssembles,
            IMessenger messenger, ILog log)
            : base(log)
        {
            GraphAssembles = graphAssembles.ToReadOnly();
            graphAssembleKeyRange = new InclusiveValueRange<int>(graphAssembles.Count(), 1);
            this.messenger = messenger;
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphSizeSet), 1)]
        [Condition(nameof(IsAssembleChosen))]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        private void CreateGraph()
        {
            var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, Width, Length);
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.Screen);
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.MainViewModel);
        }

        [MenuItem(MenuItemsNames.ChooseGraphAssemble, 1)]
        private void ChooseGraphAssemble()
        {
            using (Cursor.ClearUpAfter())
            {
                int index = IntInput.Input(GraphAssembleInpuMessage, graphAssembleKeyRange) - 1;
                SelectedGraphAssemble = GraphAssembles[index];
            }
        }

        [MenuItem(MenuItemsNames.InputGraphParametres, 2)]
        public void InputGraphParametres()
        {
            using (Cursor.ClearUpAfter())
            {
                Width = IntInput.Input(MessagesTexts.GraphWidthInputMsg, Constants.GraphWidthValueRange);
                Length = IntInput.Input(MessagesTexts.GraphHeightInputMsg, Constants.GraphLengthValueRange);
            }
        }

        [MenuItem(MenuItemsNames.InputObstaclePercent, 3)]
        public void InputObstaclePercent()
        {
            using (Cursor.ClearUpAfter())
            {
                ObstaclePercent = IntInput.Input(MessagesTexts.ObstaclePercentInputMsg, Constants.ObstaclesPercentValueRange);
            }
        }

        [FailMessage(MessagesTexts.AssebleIsNotChosenMsg)]
        private bool IsAssembleChosen()
        {
            return SelectedGraphAssemble != null;
        }

        [FailMessage(MessagesTexts.GraphSizeIsNotSetMsg)]
        private bool IsGraphSizeSet()
        {
            return Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }
    }
}