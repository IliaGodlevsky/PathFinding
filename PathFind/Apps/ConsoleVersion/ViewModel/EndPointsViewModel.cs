﻿using Autofac;
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
using NullObject.Extensions;
using System;
using System.ComponentModel;
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

        [PreValidationMethod(nameof(IsGraphValid))]
        [PreValidationMethod(nameof(CanChooseEndPoints))]
        [MenuItem(0), Description(MenuItemsNames.ChooseEndPoints)]
        public void ChooseEndPoints()
        {
            MainView.SetCursorPositionUnderMenu(MenuOffset);
            Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredEndPoints(graph, endPoints).OnEndPointChosen();
        }

        [PreValidationMethod(nameof(CanChangeVertex))]
        [MenuItem(2), Description(MenuItemsNames.ReplaceSource)]
        public void ChangeSourceVertex()
        {
            ChangeVertex(endPoints.RemoveSource, MessagesTexts.SourceVertexChoiceMsg);
        }

        [PreValidationMethod(nameof(CanChangeVertex))]
        [MenuItem(3), Description(MenuItemsNames.ReplaceTarget)]
        public void ChangeTargetVertex()
        {
            ChangeVertex(endPoints.RemoveTarget, MessagesTexts.TargetVertexChoiceMsg);
        }

        [MenuItem(5), Description(MenuItemsNames.ClearEndPoints)]
        public void ClearEndPoints()
        {
            endPoints.Reset();
        }

        [PreValidationMethod(nameof(IsGraphValid))]
        [PreValidationMethod(nameof(CanChangeIntermediates))]
        [MenuItem(4), Description(MenuItemsNames.ReplaceIntermediate)]
        public void ChangeIntermediates()
        {
            string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
            int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
            Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            IntInput.InputExistingIntermediates(graph, endPoints, toReplaceNumber).OnMarkedToReplaceIntermediate();
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputEndPoints(graph, endPoints, toReplaceNumber).OnEndPointChosen();
        }

        [PreValidationMethod(nameof(IsGraphValid))]
        [PreValidationMethod(nameof(CanChangeVertex))]
        [MenuItem(1), Description(MenuItemsNames.ChooseIntermediates)]
        public void ChooseIntermediates()
        {
            string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
            MainView.SetCursorPositionUnderMenu(MenuOffset);
            int number = IntInput.Input(message, graph.GetAvailableIntermediatesNumber());
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputEndPoints(graph, endPoints, number).OnEndPointChosen();
        }

        [MenuItem(6), Description(MenuItemsNames.Exit)]
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

        private bool IsGraphValid()
        {
            return !graph.IsNull();
        }
    }
}