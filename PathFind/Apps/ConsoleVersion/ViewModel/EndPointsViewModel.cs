using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.ValueInput;
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
        private int NumberOfAvailableIntermediate => graph.Size - graph.GetIsolatedCount() - 2;
        private bool HasAnyVerticesToChooseAsEndPoints => NumberOfAvailableIntermediate >= 0;

        public ConsoleValueInput<int> IntInput { get; set; }

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
            if (HasAnyVerticesToChooseAsEndPoints && !endPoints.HasSourceAndTargetSet())
            {
                MainView.SetCursorPositionUnderMenu(MenuOffset);
                IntInput.ChooseEndPoints(graph, endPoints, MessagesTexts.EndPointsMessages);
            }
            else
            {
                log.Warn(MessagesTexts.NoVerticesAsEndPointsMsg);
            }
        }

        [MenuItem(MenuItemsNames.ReplaceSource, MenuItemPriority.Low)]
        public void ChangeSourceVertex()
        {
            if (endPoints.HasSourceAndTargetSet())
            {
                MainView.SetCursorPositionUnderMenu(MenuOffset);
                IntInput.ChangeSource(graph, endPoints);
            }
        }

        [MenuItem(MenuItemsNames.ReplaceTarget, MenuItemPriority.Low)]
        public void ChangeTargetVertex()
        {
            if (endPoints.HasSourceAndTargetSet())
            {
                MainView.SetCursorPositionUnderMenu(MenuOffset);
                IntInput.ChangeTarget(graph, endPoints);
            }
        }

        [MenuItem(MenuItemsNames.ReplaceIntermediate, MenuItemPriority.Low)]
        public void ChangeIntermediates()
        {
            int numberOfIntermediates = endPoints.GetIntermediates().Count();
            if (numberOfIntermediates > 0)
            {
                IntInput.ChangeIntermediates(graph, endPoints, numberOfIntermediates);
            }
        }

        [MenuItem(MenuItemsNames.ChooseIntermediates, MenuItemPriority.Normal)]
        public void ChooseIntermediates()
        {
            if (endPoints.HasSourceAndTargetSet())
            {
                MainView.SetCursorPositionUnderMenu(MenuOffset);
                IntInput.ChooseIntermediates(graph, endPoints, NumberOfAvailableIntermediate);
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
            graph = (Graph2D)message.Graph;
        }

        private readonly BaseEndPoints endPoints;
        private readonly ILog log;
        private Graph2D graph;
        private readonly IMessenger messenger;
    }
}