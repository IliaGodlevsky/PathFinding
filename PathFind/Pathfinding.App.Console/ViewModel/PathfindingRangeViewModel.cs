using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Menu.Realizations.Attributes;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(3)]
    internal sealed class PathfindingRangeViewModel : ViewModel, IRequireIntInput, IDisposable
    {
        private readonly PathfindingRangeAdapter<Vertex> adapter;
        private readonly Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        private int numberOfIntermediates;

        public IInput<int> IntInput { get; set; }

        public PathfindingRangeViewModel(PathfindingRangeAdapter<Vertex> range, ICache<Graph2D<Vertex>> graph)
        {
            this.adapter = range;
            this.graph = graph.Cached;
        }

        [Condition(nameof(HasAvailableVerticesToIncludeInRange))]
        [Condition(nameof(HasSourceAndTargetNotSet), 1)]
        [MenuItem(MenuItemsNames.ChooseEndPoints, 0)]
        private void ChooseEndPoints()
        {
            ColorfulConsole.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredVertices(graph, adapter).IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        private void ReplaceSourceVertex()
        {
            adapter.Source.IncludeInRange();
            IntInput.InputVertex(MessagesTexts.SourceVertexChoiceMsg, graph, adapter).IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        private void ReplaceTargetVertex()
        {
            adapter.Target.IncludeInRange();
            IntInput.InputVertex(MessagesTexts.TargetVertexChoiceMsg, graph, adapter).IncludeInRange();
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, 5)]
        private void ClearEndPoints()
        {
            adapter.Undo();
        }

        [Condition(nameof(CanReplaceIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        private void ReplaceIntermediates()
        {
            string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
            int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            IntInput.InputExistingIntermediates(graph, adapter, toReplaceNumber)
                .MarkAsIntermediateToReplace();
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputVertices(graph, adapter, toReplaceNumber).IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, 1)]
        private void ChooseIntermediates()
        {
            string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
            int number = IntInput.Input(message, graph.GetAvailableIntermediatesVerticesNumber());
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputVertices(graph, adapter, number).IncludeInRange();
        }

        [FailMessage(MessagesTexts.NoIntermediatesChosenMsg)]
        private bool CanReplaceIntermediates()
        {
            var adapterInterface = (IPathfindingRangeAdapter<Vertex>)adapter;
            return (numberOfIntermediates = adapterInterface.Intermediates.Count) > 0;
        }

        [FailMessage(MessagesTexts.NoPathfindingRangeMsg)]
        private bool HasSourceAndTargetSet()
        {
            return adapter.HasSourceAndTargetSet();
        }

        [FailMessage(MessagesTexts.NoVerticesToChooseAsRangeMsg)]
        private bool HasAvailableVerticesToIncludeInRange()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        [FailMessage(MessagesTexts.PathfindingRangeWasSetMsg)]
        private bool HasSourceAndTargetNotSet()
        {
            return !adapter.HasSourceAndTargetSet();
        }
    }
}