using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using System;
using System.Linq;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ViewModel
{
    internal class PathfindingRangeViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action ViewClosed;

        private readonly VisualPathfindingRange<Vertex> pathfindingRange;
        private readonly ILog log;

        private int numberOfIntermediates;
        private readonly Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        public PathfindingRangeViewModel(VisualPathfindingRange<Vertex> range, 
            ICache<Graph2D<Vertex>> graph, ILog log)
        {
            this.pathfindingRange = range;
            this.log = log;
            this.graph = graph.Cached;
        }

        [Condition(nameof(CanChooseEndPoints))]
        [MenuItem(MenuItemsNames.ChooseEndPoints, 0)]
        private void ChooseEndPoints()
        {
            ColorfulConsole.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredVertices(graph, pathfindingRange)
                .IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        private void ReplaceSourceVertex()
        {
            pathfindingRange.Source.IncludeInRange();
            IntInput.InputVertex(MessagesTexts.SourceVertexChoiceMsg, graph, pathfindingRange)
                .IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        private void ReplaceTargetVertex()
        {
            pathfindingRange.Target.IncludeInRange();
            IntInput.InputVertex(MessagesTexts.TargetVertexChoiceMsg, graph, pathfindingRange)
                .IncludeInRange();
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, 5)]
        private void ClearEndPoints()
        {
            pathfindingRange.Undo();
        }

        [Condition(nameof(CanReplaceIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        private void ReplaceIntermediates()
        {
            string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
            int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            IntInput.InputExistingIntermediates(graph, pathfindingRange, toReplaceNumber)
                .MarkAsIntermediateToReplace();
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputVertices(graph, pathfindingRange, toReplaceNumber).IncludeInRange();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, 1)]
        private void ChooseIntermediates()
        {
            string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
            int number = IntInput.Input(message, graph.GetAvailableIntermediatesNumber());
            ColorfulConsole.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputVertices(graph, pathfindingRange, number).IncludeInRange();
        }

        [MenuItem(MenuItemsNames.Exit, 6)]
        private void Interrupt()
        {
            ViewClosed?.Invoke();
        }

        public void Dispose()
        {
            ViewClosed = null;
        }

        private bool CanReplaceIntermediates()
        {
            return (numberOfIntermediates = pathfindingRange.GetIntermediates().Count()) > 0;
        }

        private bool HasSourceAndTargetSet()
        {
            return pathfindingRange.HasSourceAndTargetSet();
        }

        private bool CanChooseEndPoints()
        {
            return graph.GetAvailableIntermediatesNumber() > 0 && !pathfindingRange.HasSourceAndTargetSet();
        }
    }
}