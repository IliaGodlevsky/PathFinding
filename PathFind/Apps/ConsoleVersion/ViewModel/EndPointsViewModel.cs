using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Views;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Realizations.Graphs;
using Logging.Interface;
using System;
using System.Linq;

namespace ConsoleVersion.ViewModel
{
    internal class EndPointsViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

        private const int MenuOffset = 8;

        private readonly BaseEndPoints endPoints;
        private readonly ILog log;
        private readonly IMessenger messenger;

        private int numberOfIntermediates;

        private Graph2D graph;

        public IInput<int> IntInput { get; set; }

        public EndPointsViewModel(BaseEndPoints endPoints, ILog log)
        {
            this.endPoints = endPoints;
            this.log = log;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.EndPointsViewModel, SetGraph);
            var message = new ClaimGraphMessage(MessageTokens.EndPointsViewModel);
            messenger.Forward(message, MessageTokens.Everyone);
        }

        [ExecutionCheckMethod(nameof(CanChooseEndPoints))]
        [MenuItem(MenuItemsNames.ChooseEndPoints, MenuItemPriority.Highest)]
        public void ChooseEndPoints()
        {
            MainView.SetCursorPositionUnderMenu(MenuOffset);
            Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredEndPoints(graph, endPoints).OnEndPointChosen();
        }

        [ExecutionCheckMethod(nameof(CanChangeVertex))]
        [MenuItem(MenuItemsNames.ReplaceSource, MenuItemPriority.Low)]
        public void ChangeSourceVertex()
        {
            ChangeVertex(endPoints.RemoveSource, MessagesTexts.SourceVertexChoiceMsg);
        }

        [ExecutionCheckMethod(nameof(CanChangeVertex))]
        [MenuItem(MenuItemsNames.ReplaceTarget, MenuItemPriority.Low)]
        public void ChangeTargetVertex()
        {
            ChangeVertex(endPoints.RemoveTarget, MessagesTexts.TargetVertexChoiceMsg);
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, MenuItemPriority.Low)]
        public void ClearEndPoints()
        {
            endPoints.Reset();
        }

        [ExecutionCheckMethod(nameof(CanChangeIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, MenuItemPriority.Low)]
        public void ChangeIntermediates()
        {
            string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
            int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
            Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            IntInput.InputExistingIntermediates(graph, endPoints, toReplaceNumber).OnMarkedToReplaceIntermediate();
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputEndPoints(graph, endPoints, toReplaceNumber).OnEndPointChosen();
        }

        [ExecutionCheckMethod(nameof(CanChangeVertex))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, MenuItemPriority.Normal)]
        public void ChooseIntermediates()
        {
            string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
            MainView.SetCursorPositionUnderMenu(MenuOffset);
            int number = IntInput.Input(message, graph.GetAvailableIntermediatesNumber());
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputEndPoints(graph, endPoints, number).OnEndPointChosen();
        }

        [MenuItem(MenuItemsNames.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        public void Dispose()
        {
            WindowClosed = null;
            messenger.Unregister(this);
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        private void ChangeVertex(Action action, string message)
        {
            MainView.SetCursorPositionUnderMenu(MenuOffset);
            action();
            Console.WriteLine(message);
            IntInput.InputEndPoint(graph, endPoints).OnEndPointChosen();
        }

        private bool CanChangeIntermediates()
        {
            return (numberOfIntermediates = endPoints.GetIntermediates().Count()) > 0;
        }

        private bool CanChangeVertex()
        {
            return endPoints.HasSourceAndTargetSet();
        }

        private bool CanChooseEndPoints()
        {
            return graph.HasAvailableEndPoints() && !endPoints.HasSourceAndTargetSet();
        }
    }
}