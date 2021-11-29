using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using ConsoleVersion.Views;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Graphs;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.ViewModel
{
    internal class EndPointsViewModel : IViewModel, IRequireInt32Input, IDisposable
    {
        public string[] EndPointsMessages { private get; set; }
        public string[] ReplaceIntermediatesMessages { private get; set; }

        private int NumberOfAvailableIntermediate => graph.Size - graph.GetIsolatedCount() - 2;
        private bool HasAnyVerticesToChooseAsEndPoints => NumberOfAvailableIntermediate >= 0;

        private const int MethodsCount = 6;
        private const int Offset = MethodsCount + 1;

        public event Action WindowClosed;

        public ConsoleValueInput<int> Int32Input { get; set; }

        public EndPointsViewModel(BaseEndPoints endPoints, ILog log)
        {
            graph = NullGraph.Instance;
            this.endPoints = endPoints;
            this.log = log;
            Messenger.Default.Register<GraphCreatedMessage>(this, MessageTokens.EndPointsViewModel, SetGraph);
            var claimMessage = new ClaimGraphMessage(MessageTokens.EndPointsViewModel);
            Messenger.Default.Forward(claimMessage, MessageTokens.Everyone);
        }

        [MenuItem(MenuItemsNames.ChooseEndPoints, MenuItemPriority.Highest)]
        public void ChooseEndPoints()
        {
            if (HasAnyVerticesToChooseAsEndPoints)
            {
                endPoints.Reset();
                SetCursorPositionUnderMenu();
                ChooseEndPointVertices(EndPointsMessages);
                isEndPointsChosen = true;
            }
            else
            {
                log.Warn(MessagesTexts.NoVerticesAsEndPointsMsg);
            }
        }

        [MenuItem(MenuItemsNames.ChooseIntermediates, MenuItemPriority.Normal)]
        public void ChooseIntermediates()
        {
            if (isEndPointsChosen)
            {
                string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
                SetCursorPositionUnderMenu();
                int numberOfIntermediates = Int32Input.InputValue(message, NumberOfAvailableIntermediate);
                var messages = Enumerable.Repeat(MessagesTexts.IntermediateVertexChoiceMsg, numberOfIntermediates);
                ChooseEndPointVertices(messages);
            }
        }

        [MenuItem(MenuItemsNames.ReplaceSourceVertex, MenuItemPriority.Low)]
        public void ReplaceSourceVertex()
        {
            ReplaceEndPoint(endPoints.Source, MessagesTexts.SourceVertexChoiceMsg);
        }

        [MenuItem(MenuItemsNames.ReplaceTargetVertex, MenuItemPriority.Low)]
        public void ReplaceTargetVertex() 
        { 
            ReplaceEndPoint(endPoints.Target, MessagesTexts.TargetVertexChoiceMsg); 
        }

        [MenuItem(MenuItemsNames.ReplaceIntermediates, MenuItemPriority.Low)]
        public void ReplaceIntermediates()
        {
            var intermdiates = endPoints.GetIntermediates().ToArray();
            if (isEndPointsChosen && intermdiates.Length > 0)
            {               
                string message = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
                SetCursorPositionUnderMenu();
                int numberOfIntermediates = Int32Input.InputValue(message, intermdiates.Length);
                var messages = Enumerable.Repeat(ReplaceIntermediatesMessages, numberOfIntermediates);
                cursorLeft = Console.CursorLeft;
                cursorRight = Console.CursorTop;
                foreach (var msg in messages)
                {
                    InputVertexAndPerformAction(msg[0], intermdiates.Contains, MarkToReplace);
                    InputVertexAndPerformAction(msg[1], endPoints.CanBeEndPoint, MarkAsEndPoint);
                }
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
            Messenger.Default.Unregister(this);
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        private void SetCursorPositionUnderMenu()
        {
            var fieldPosition = MainView.PathfindingStatisticsPosition;
            Console.SetCursorPosition(fieldPosition.X, fieldPosition.Y + Offset);
        }

        private void ReplaceEndPoint(IVertex vertex, string message)
        {
            if (isEndPointsChosen)
            {
                var toReplace = vertex as Vertex;
                toReplace?.OnEndPointChosen();
                SetCursorPositionUnderMenu();
                var messages = new[] { message };
                ChooseEndPointVertices(messages);
            }
        }

        private void InputVertexAndPerformAction(string message,
            Predicate<IVertex> inputPredicate, Action<Vertex> markAction)
        {
            Console.SetCursorPosition(cursorLeft, cursorRight);
            var vertex = (Vertex)InputVertex(message, inputPredicate);
            cursorLeft = Console.CursorLeft;
            cursorRight = Console.CursorTop;
            markAction(vertex);
        }

        private void ChooseEndPointVertices(IEnumerable<string> messages)
        {
            cursorLeft = Console.CursorLeft;
            cursorRight = Console.CursorTop;
            foreach (var message in messages)
            {
                InputVertexAndPerformAction(message, endPoints.CanBeEndPoint, MarkAsEndPoint);
            }
        }

        private IVertex InputVertex(string message, Predicate<IVertex> inputPredicate)
        {
            if (graph is Graph2D graph2D)
            {
                Console.WriteLine(message);
                IVertex vertex;
                do
                {
                    vertex = Int32Input.InputVertex(graph2D);
                } while (!inputPredicate(vertex));

                return vertex;
            }

            return NullVertex.Instance;
        }

        private static void MarkToReplace(Vertex vertex) => vertex.OnMarkedToReplaceIntermediate();
        private static void MarkAsEndPoint(Vertex vertex) => vertex.OnEndPointChosen();
        
        private readonly BaseEndPoints endPoints;
        private readonly ILog log;
        private bool isEndPointsChosen;
        private IGraph graph;
        private int cursorLeft;
        private int cursorRight;
    }
}