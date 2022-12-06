using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
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

        private readonly IMessenger messenger;
        private readonly ReplaceTransitVerticesModule<Vertex> module;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private int numberOfIntermediates;

        public IInput<int> IntInput { get; set; }

        public PathfindingRangeViewModel(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            ReplaceTransitVerticesModule<Vertex> module, IMessenger messenger)
        {
            this.rangeBuilder = rangeBuilder;
            this.module = module;
            this.messenger = messenger;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        [Condition(nameof(HasAvailableVerticesToIncludeInRange))]
        [Condition(nameof(HasSourceAndTargetNotSet), 1)]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 0)]
        private void ChoosePathfindingRange()
        {
            using (Cursor.CleanUpAfter())
            {
                System.Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
                InputVertices(RequiredVerticesForRange).ForEach(IncludeInRange);
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        private void ReplaceSourceVertex()
        {
            using (Cursor.CleanUpAfter())
            {
                ExcludeFromRange(rangeBuilder.Range.Source);
                IncludeInRange(InputVertex(MessagesTexts.SourceVertexChoiceMsg));
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        private void ReplaceTargetVertex()
        {
            using (Cursor.CleanUpAfter())
            {
                ExcludeFromRange(rangeBuilder.Range.Target);
                IncludeInRange(InputVertex(MessagesTexts.TargetVertexChoiceMsg));
            }
        }

        [MenuItem(MenuItemsNames.ClearPathfindingRange, 5)]
        private void ClearPathfindingRange()
        {
            rangeBuilder.Undo();
        }

        [Condition(nameof(CanReplaceTransitVertices), 0)]
        [Condition(nameof(HasTransitVerticesToReplace), 1)]
        //[MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        private void ReplaceIntermediates()
        {
            using (Cursor.CleanUpAfter())
            {
                string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
                int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
                System.Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
                IntInput.InputExistingIntermediates(graph, rangeBuilder.Range, toReplaceNumber).ForEach(MarkTransitVertex);
                System.Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                InputVertices(toReplaceNumber).ForEach(ReplaceTransitVertex);
            }
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        //[MenuItem(MenuItemsNames.ChooseTransit, 1)]
        private void ChooseTransitVertices()
        {
            using (Cursor.CleanUpAfter())
            {
                string message = MessagesTexts.NumberOfTransitVerticesInputMsg;
                int available = graph.GetAvailableTransitVerticesNumber();
                int number = IntInput.Input(message, available);
                System.Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                InputVertices(number).ForEach(IncludeInRange);
            }
        }

        private void ReplaceTransitVertex(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                module.ReplaceTransitWith(rangeBuilder.Range, vertex);
            }
        }

        private void IncludeInRange(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                rangeBuilder.Include(vertex);
            }
        }

        private void ExcludeFromRange(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                rangeBuilder.Exclude(vertex);
            }
        }

        private void MarkTransitVertex(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                module.MarkTransitVertex(rangeBuilder.Range, vertex);
            }
        }

        private IEnumerable<Vertex> InputVertices(int number)
        {
            return IntInput.InputVertices(graph, rangeBuilder.Range, number);
        }

        private Vertex InputVertex(string message)
        {
            System.Console.WriteLine(message);
            return IntInput.InputVertex(graph, rangeBuilder.Range);
        }

        [FailMessage(MessagesTexts.NoIntermediatesChosenMsg)]
        private bool HasTransitVerticesToReplace() => (numberOfIntermediates = rangeBuilder.Range.Count() - 2) > 0;

        [FailMessage(MessagesTexts.NoPathfindingRangeMsg)]
        private bool HasSourceAndTargetSet() => rangeBuilder.Range.HasSourceAndTargetSet();

        [FailMessage(MessagesTexts.NoVerticesToChooseAsRangeMsg)]
        private bool HasAvailableVerticesToIncludeInRange() => graph.GetNumberOfNotIsolatedVertices() > 1;

        [FailMessage(MessagesTexts.PathfindingRangeWasSetMsg)]
        private bool HasSourceAndTargetNotSet() => !rangeBuilder.Range.HasSourceAndTargetSet();

        [FailMessage("Replacing transit vertices is not supported")]
        private bool CanReplaceTransitVertices() => module != null;
    }
}