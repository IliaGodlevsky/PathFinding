using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
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
        private const int Offset = MethodsCount;

        public event Action WindowClosed;

        public ConsoleValueInput<int> Int32Input { get; set; }

        public EndPointsViewModel(BaseEndPoints endPoints, ILog log)
        {
            graph = NullGraph.Instance;
            this.endPoints = endPoints;
            this.log = log;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.EndPointsViewModel, SetGraph);
        }

        [MenuItem(MenuItemsNames.ChooseEndPoints, MenuItemPriority.Highest)]
        public void ChooseEndPoints()
        {
            if (HasAnyVerticesToChooseAsEndPoints)
            {
                if (!endPoints.HasSourceAndTargetSet())
                {
                    SetCursorPositionUnderMenu();
                    ChooseEndPointVertices(EndPointsMessages);
                }
            }
            else
            {
                log.Warn(MessagesTexts.NoVerticesAsEndPointsMsg);
            }
        }

        [MenuItem(MenuItemsNames.ChooseIntermediates, MenuItemPriority.Normal)]
        public void ChooseIntermediates()
        {
            if (endPoints.HasSourceAndTargetSet())
            {
                string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
                SetCursorPositionUnderMenu();
                int numberOfIntermediates = Int32Input.InputValue(message, NumberOfAvailableIntermediate);
                var messages = Enumerable.Repeat(MessagesTexts.IntermediateVertexChoiceMsg, numberOfIntermediates);
                ChooseEndPointVertices(messages);
            }
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, MenuItemPriority.Low)]
        public void ClearEndPoints()
        {
            endPoints.Reset();
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

        private void SetCursorPositionUnderMenu()
        {
            var fieldPosition = MainView.PathfindingStatisticsPosition;
            Console.SetCursorPosition(fieldPosition.X, fieldPosition.Y + Offset);
        }

        private void ChooseEndPointVertices(IEnumerable<string> messages)
        {
            ConsoleCursor.SaveCursorPosition();
            foreach (var message in messages)
            {
                ConsoleCursor.RestoreCursorPosition();
                var vertex = (Vertex)InputVertex(message);
                ConsoleCursor.SaveCursorPosition();
                vertex.OnEndPointChosen();
            }
        }

        private IVertex InputVertex(string message)
        {
            if (graph is Graph2D graph2D)
            {
                Console.WriteLine(message);
                IVertex vertex;
                do
                {
                    vertex = Int32Input.InputVertex(graph2D);
                } while (!endPoints.CanBeEndPoint(vertex));

                return vertex;
            }

            return NullVertex.Instance;
        }

        private readonly BaseEndPoints endPoints;
        private readonly ILog log;
        private IGraph graph;
        private readonly IMessenger messenger;
    }
}