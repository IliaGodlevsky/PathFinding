using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.MenuCommands.Attributes;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Core.Abstractions;
using System;
using System.Linq;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(3)]
    internal sealed class PathfindingRangeViewModel : ViewModel, IRequireIntInput, IDisposable
    {
        private readonly VisualPathfindingRange<Vertex> range;
        private readonly Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        private int numberOfIntermediates;

        public IInput<int> IntInput { get; set; }

        public PathfindingRangeViewModel(VisualPathfindingRange<Vertex> range, 
            ICache<Graph2D<Vertex>> graph)
        {
            this.range = range;
            this.graph = graph.Cached;
        }

        [Condition(nameof(HasAvailableVerticesToIncludeInRange))]
        [Condition(nameof(HasSourceAndTargetNotSet), 1)]
        [MenuItem(MenuItemsNames.ChooseEndPoints, 0)]
        private void ChooseEndPoints()
        {
            ColorfulConsole.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredVertices(graph, range).IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        private void ReplaceSourceVertex()
        {
            range.Source.IncludeInRange();
            IntInput.InputVertex(MessagesTexts.SourceVertexChoiceMsg, graph, range).IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        private void ReplaceTargetVertex()
        {
            range.Target.IncludeInRange();
            IntInput.InputVertex(MessagesTexts.TargetVertexChoiceMsg, graph, range).IncludeInRange();
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, 5)]
        private void ClearEndPoints()
        {
            range.Undo();
        }

        [Condition(nameof(CanReplaceIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        private void ReplaceIntermediates()
        {
            string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
            int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            IntInput.InputExistingIntermediates(graph, range, toReplaceNumber)
                .MarkAsIntermediateToReplace();
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputVertices(graph, range, toReplaceNumber).IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, 1)]
        private void ChooseIntermediates()
        {
            string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
            int number = IntInput.Input(message, graph.GetAvailableIntermediatesVerticesNumber());
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputVertices(graph, range, number).IncludeInRange();
        }

        [FailMessage(MessagesTexts.NoIntermediatesChosen)]
        private bool CanReplaceIntermediates()
        {
            return (numberOfIntermediates = range.GetIntermediates().Count()) > 0;
        }

        [FailMessage(MessagesTexts.NoPathfindingRange)]
        private bool HasSourceAndTargetSet()
        {
            return range.HasSourceAndTargetSet();
        }

        [FailMessage("No available vertices to include in range")]
        private bool HasAvailableVerticesToIncludeInRange()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        [FailMessage("Source and target vertives were set")]
        private bool HasSourceAndTargetNotSet()
        {
            return !range.HasSourceAndTargetSet();
        }
    }
}