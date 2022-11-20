using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(3)]
    internal sealed class PathfindingRangeViewModel : ViewModel, IRequireIntInput, IDisposable
    {
        private const int RequiredVerticesForRange = 2;

        private readonly ConsolePathfindingRangeAdapter adapter;
        private readonly Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        private int numberOfIntermediates;

        public IInput<int> IntInput { get; set; }

        public PathfindingRangeViewModel(ConsolePathfindingRangeAdapter range, ICache<Graph2D<Vertex>> graph)
        {
            this.adapter = range;
            this.graph = graph.Cached;
        }

        [Condition(nameof(HasAvailableVerticesToIncludeInRange))]
        [Condition(nameof(HasSourceAndTargetNotSet), 1)]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 0)]
        private void ChooseEndPoints()
        {
            using (Cursor.UsePositionAndClear())
            {
                ColorfulConsole.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
                InputVertices(RequiredVerticesForRange).ForEach(IncludeInRange);
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        private void ReplaceSourceVertex()
        {
            using (Cursor.UsePositionAndClear())
            {
                IncludeInRange(adapter.Source);
                IncludeInRange(InputVertex(MessagesTexts.SourceVertexChoiceMsg));
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        private void ReplaceTargetVertex()
        {
            using (Cursor.UsePositionAndClear())
            {
                IncludeInRange(adapter.Target);
                IncludeInRange(InputVertex(MessagesTexts.TargetVertexChoiceMsg));
            }
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, 5)]
        private void ClearPathfindingRange()
        {
            adapter.Undo();
        }

        [Condition(nameof(CanReplaceIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        private void ReplaceIntermediates()
        {
            using (Cursor.UsePositionAndClear())
            {
                string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
                int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
                ColorfulConsole.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
                IntInput.InputExistingIntermediates(graph, adapter, toReplaceNumber).ForEach(MarkAsToReplace);
                ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                InputVertices(toReplaceNumber).ForEach(IncludeInRange);
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, 1)]
        private void ChooseIntermediates()
        {
            using (Cursor.UsePositionAndClear())
            {
                string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
                int available = graph.GetAvailableIntermediatesVerticesNumber();
                int number = IntInput.Input(message, available);
                ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                InputVertices(number).ForEach(IncludeInRange);
            }
        }

        private void IncludeInRange(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                adapter.IncludeInRange(vertex);
            }
        }

        private void MarkAsToReplace(Vertex vertex)
        {
            using(Cursor.UseCurrentPosition())
            {
                adapter.MarkAsIntermediateToReplace(vertex);
            }
        }

        private IEnumerable<Vertex> InputVertices(int number)
        {
            return IntInput.InputVertices(graph, adapter, number);
        }

        private Vertex InputVertex(string message)
        {
            ColorfulConsole.WriteLine(message);
            return IntInput.InputVertex(graph, adapter);
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