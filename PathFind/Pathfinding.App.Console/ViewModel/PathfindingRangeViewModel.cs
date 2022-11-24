using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(3)]
    internal sealed class PathfindingRangeViewModel : ViewModel, IRequireIntInput, IDisposable
    {
        private const int RequiredVerticesForRange = 2;

        private readonly ConsolePathfindingRange range;
        private readonly Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        private int numberOfIntermediates;

        public IInput<int> IntInput { get; set; }

        public PathfindingRangeViewModel(ConsolePathfindingRange range, ICache<Graph2D<Vertex>> graph)
        {
            this.range = range;
            this.graph = graph.Cached;
        }

        [Condition(nameof(HasAvailableVerticesToIncludeInRange))]
        [Condition(nameof(HasSourceAndTargetNotSet), 1)]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 0)]
        private void ChooseEndPoints()
        {
            using (Cursor.ClearUpAfter())
            {
                System.Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
                InputVertices(RequiredVerticesForRange).ForEach(IncludeInRange);
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        private void ReplaceSourceVertex()
        {
            using (Cursor.ClearUpAfter())
            {
                IncludeInRange(range.Source);
                IncludeInRange(InputVertex(MessagesTexts.SourceVertexChoiceMsg));
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        private void ReplaceTargetVertex()
        {
            using (Cursor.ClearUpAfter())
            {
                IncludeInRange(range.Target);
                IncludeInRange(InputVertex(MessagesTexts.TargetVertexChoiceMsg));
            }
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, 5)]
        private void ClearPathfindingRange()
        {
            range.Undo();
        }

        [Condition(nameof(CanReplaceIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        private void ReplaceIntermediates()
        {
            using (Cursor.ClearUpAfter())
            {
                string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
                int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
                System.Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
                IntInput.InputExistingIntermediates(graph, range, toReplaceNumber).ForEach(MarkAsToReplace);
                System.Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                InputVertices(toReplaceNumber).ForEach(IncludeInRange);
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, 1)]
        private void ChooseIntermediates()
        {
            using (Cursor.ClearUpAfter())
            {
                string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
                int available = graph.GetAvailableIntermediatesVerticesNumber();
                int number = IntInput.Input(message, available);
                System.Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                InputVertices(number).ForEach(IncludeInRange);
            }
        }

        private void IncludeInRange(Vertex vertex)
        {
            using (Cursor.RestoreCurrentPosition())
            {
                range.IncludeInPathfindingRange(vertex);
            }
        }

        private void MarkAsToReplace(Vertex vertex)
        {
            using (Cursor.RestoreCurrentPosition())
            {
                range.MarkAsIntermediateToReplace(vertex);
            }
        }

        private IEnumerable<Vertex> InputVertices(int number)
        {
            return IntInput.InputVertices(graph, range, number);
        }

        private Vertex InputVertex(string message)
        {
            System.Console.WriteLine(message);
            return IntInput.InputVertex(graph, range);
        }

        [FailMessage(MessagesTexts.NoIntermediatesChosenMsg)]
        private bool CanReplaceIntermediates()
        {
            return (numberOfIntermediates = range.Count() - 2) > 0;
        }

        [FailMessage(MessagesTexts.NoPathfindingRangeMsg)]
        private bool HasSourceAndTargetSet()
        {
            return range.HasSourceAndTargetSet();
        }

        [FailMessage(MessagesTexts.NoVerticesToChooseAsRangeMsg)]
        private bool HasAvailableVerticesToIncludeInRange()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        [FailMessage(MessagesTexts.PathfindingRangeWasSetMsg)]
        private bool HasSourceAndTargetNotSet()
        {
            return !range.HasSourceAndTargetSet();
        }
    }
}