using Autofac;
using Common.Extensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Realizations.Graphs;
using Logging.Interface;
using System;
using System.Linq;

namespace ConsoleVersion.ViewModel
{
    internal class PathfindingRangeViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

        private readonly BasePathfindingRange pathfindingRange;
        private readonly ILog log;
        private readonly IMessenger messenger;

        private int numberOfIntermediates;
        private Graph2D graph;

        public IInput<int> IntInput { get; set; }

        public PathfindingRangeViewModel(BasePathfindingRange pathfindingRange, ILog log)
        {
            this.pathfindingRange = pathfindingRange;
            this.log = log;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<ClaimGraphAnswer>(this, SetGraph);
            messenger.Send(new ClaimGraphMessage());
        }

        [Condition(nameof(CanChoosePathfindingRange))]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 0)]
        public void ChoosePathfindingRange()
        {
            Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredPathfindingRange(graph, pathfindingRange).OnEndPointChosen();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        public void ReplaceSourceVertex()
        {
            pathfindingRange.Source.As<Vertex>().OnEndPointChosen();
            IntInput.InputEndPoint(MessagesTexts.SourceVertexChoiceMsg, graph, pathfindingRange).OnEndPointChosen();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        public void ReplaceTargetVertex()
        {
            pathfindingRange.Target.As<Vertex>().OnEndPointChosen();
            IntInput.InputEndPoint(MessagesTexts.TargetVertexChoiceMsg, graph, pathfindingRange).OnEndPointChosen();
        }

        [MenuItem(MenuItemsNames.ClearPathfindingRange, 5)]
        public void ClearPathfindingRange()
        {
            pathfindingRange.Reset();
        }

        [Condition(nameof(CanReplaceIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        public void ReplaceIntermediates()
        {
            string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
            int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
            Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            IntInput.InputExistingIntermediates(graph, pathfindingRange, toReplaceNumber).OnMarkedToReplaceIntermediate();
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputPathfindingRange(graph, pathfindingRange, toReplaceNumber).OnEndPointChosen();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, 1)]
        public void ChooseIntermediates()
        {
            string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
            int number = IntInput.Input(message, graph.GetAvailableIntermediatesNumber());
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputPathfindingRange(graph, pathfindingRange, number).OnEndPointChosen();
        }

        [MenuItem(MenuItemsNames.Exit, 6)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        public void Dispose()
        {
            WindowClosed = null;
            messenger.Unregister(this);
        }

        private void SetGraph(ClaimGraphAnswer message)
        {
            graph = (Graph2D)message.Graph;
        }

        private bool CanReplaceIntermediates()
        {
            return (numberOfIntermediates = pathfindingRange.GetIntermediates().Count()) > 0;
        }

        private bool HasSourceAndTargetSet()
        {
            return pathfindingRange.HasSourceAndTargetSet();
        }

        private bool CanChoosePathfindingRange()
        {
            return graph.GetAvailableIntermediatesNumber() > 0 && !pathfindingRange.HasSourceAndTargetSet();
        }
    }
}