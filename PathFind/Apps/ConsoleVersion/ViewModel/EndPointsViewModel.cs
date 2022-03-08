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

        public IValueInput<int> IntInput { get; set; }

        public EndPointsViewModel(BaseEndPoints endPoints, ILog log)
        {
            this.endPoints = endPoints;
            this.log = log;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.EndPointsViewModel, SetGraph);
        }

        [MenuItem(MenuItemsNames.ChooseEndPoints, MenuItemPriority.Highest)]
        public void ChooseEndPoints()
        {
            if (graph.HasAvailableEndPoints() && !endPoints.HasSourceAndTargetSet())
            {
                MainView.SetCursorPositionUnderMenu(MenuOffset);
                Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
                IntInput.InputRequiredEndPoints(graph, endPoints).OnEndPointChosen();
            }
            else
            {
                log.Warn(MessagesTexts.NoVerticesAsEndPointsMsg);
            }
        }

        [MenuItem(MenuItemsNames.ReplaceSource, MenuItemPriority.Low)]
        public void ChangeSourceVertex() => ChangeVertex(endPoints.RemoveSource, MessagesTexts.SourceVertexChoiceMsg);

        [MenuItem(MenuItemsNames.ReplaceTarget, MenuItemPriority.Low)]
        public void ChangeTargetVertex() => ChangeVertex(endPoints.RemoveTarget, MessagesTexts.TargetVertexChoiceMsg);

        [MenuItem(MenuItemsNames.ClearEndPoints, MenuItemPriority.Low)]
        public void ClearEndPoints() => endPoints.Reset();

        [MenuItem(MenuItemsNames.ReplaceIntermediate, MenuItemPriority.Low)]
        public void ChangeIntermediates()
        {
            int numberOfIntermediates = endPoints.GetIntermediates().Count();
            if (numberOfIntermediates > 0)
            {
                string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
                int toReplaceNumber = IntInput.InputValue(msg, numberOfIntermediates);
                Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
                IntInput.InputExistingIntermediates(graph, endPoints, toReplaceNumber).OnMarkedToReplaceIntermediate();
                Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                IntInput.InputEndPoints(graph, endPoints, toReplaceNumber).OnEndPointChosen();
            }
        }

        [MenuItem(MenuItemsNames.ChooseIntermediates, MenuItemPriority.Normal)]
        public void ChooseIntermediates()
        {
            if (endPoints.HasSourceAndTargetSet())
            {
                string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
                MainView.SetCursorPositionUnderMenu(MenuOffset);
                int number = IntInput.InputValue(message, graph.GetAvailableIntermediatesNumber());
                Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
                IntInput.InputEndPoints(graph, endPoints, number).OnEndPointChosen();
            }
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
            if (endPoints.HasSourceAndTargetSet())
            {
                MainView.SetCursorPositionUnderMenu(MenuOffset);
                action();
                Console.WriteLine(message);
                IntInput.InputEndPoint(graph, endPoints).OnEndPointChosen();
            }
        }

        private readonly BaseEndPoints endPoints;
        private readonly ILog log;
        private Graph2D graph;
        private readonly IMessenger messenger;
    }
}