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
    internal class EndPointsViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

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
            messenger.Register<ClaimGraphAnswer>(this, SetGraph);
            messenger.Send(new ClaimGraphMessage());
        }

        [PreValidationMethod(nameof(CanChooseEndPoints))]
        [MenuItem(MenuItemsNames.ChooseEndPoints, 0)]
        public void ChooseEndPoints()
        {
            Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredEndPoints(graph, endPoints).OnEndPointChosen();
        }

        [PreValidationMethod(nameof(CanReplaceSourceOrTargetVertex))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        public void ReplaceSourceVertex()
        {
            endPoints.Source.As<Vertex>().OnEndPointChosen();
            IntInput.InputEndPoint(MessagesTexts.SourceVertexChoiceMsg, graph, endPoints).OnEndPointChosen();
        }

        [PreValidationMethod(nameof(CanReplaceSourceOrTargetVertex))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        public void ReplaceTargetVertex()
        {
            endPoints.Target.As<Vertex>().OnEndPointChosen();
            IntInput.InputEndPoint(MessagesTexts.TargetVertexChoiceMsg, graph, endPoints).OnEndPointChosen();
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, 5)]
        public void ClearEndPoints()
        {
            endPoints.Reset();
        }

        [PreValidationMethod(nameof(CanReplaceIntermediates))]
        [MenuItem(MenuItemsNames.ReplaceIntermediate, 4)]
        public void ReplaceIntermediates()
        {
            string msg = MessagesTexts.NumberOfIntermediatesVerticesToReplaceMsg;
            int toReplaceNumber = IntInput.Input(msg, numberOfIntermediates);
            Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            IntInput.InputExistingIntermediates(graph, endPoints, toReplaceNumber).OnMarkedToReplaceIntermediate();
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputEndPoints(graph, endPoints, toReplaceNumber).OnEndPointChosen();
        }

        [PreValidationMethod(nameof(CanReplaceSourceOrTargetVertex))]
        [MenuItem(MenuItemsNames.ChooseIntermediates, 1)]
        public void ChooseIntermediates()
        {
            string message = MessagesTexts.NumberOfIntermediateVerticesInputMsg;
            int number = IntInput.Input(message, graph.GetAvailableIntermediatesNumber());
            Console.WriteLine(MessagesTexts.IntermediateVertexChoiceMsg);
            IntInput.InputEndPoints(graph, endPoints, number).OnEndPointChosen();
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
            return (numberOfIntermediates = endPoints.GetIntermediates().Count()) > 0;
        }

        private bool CanReplaceSourceOrTargetVertex()
        {
            return endPoints.HasSourceAndTargetSet();
        }

        private bool CanChooseEndPoints()
        {
            return graph.HasAvailableEndPoints() && !endPoints.HasSourceAndTargetSet();
        }
    }
}