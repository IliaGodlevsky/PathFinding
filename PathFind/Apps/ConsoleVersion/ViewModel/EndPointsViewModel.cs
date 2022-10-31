using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
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

        private readonly BaseEndPoints<Vertex> endPoints;
        private readonly ILog log;

        private int numberOfIntermediates;
        private readonly Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        public EndPointsViewModel(BaseEndPoints<Vertex> endPoints, ICache<Graph2D<Vertex>> graph, ILog log)
        {
            this.endPoints = endPoints;
            this.log = log;
            this.graph = graph.Cached;
        }

        [Condition(nameof(CanChooseEndPoints))]
        [MenuItem(MenuItemsNames.ChooseEndPoints, 0)]
        public void ChooseEndPoints()
        {
            Console.WriteLine(MessagesTexts.SourceAndTargetInputMsg);
            IntInput.InputRequiredEndPoints(graph, endPoints).OnEndPointChosen();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceSource, 2)]
        public void ReplaceSourceVertex()
        {
            endPoints.Source.OnEndPointChosen();
            IntInput.InputEndPoint(MessagesTexts.SourceVertexChoiceMsg, graph, endPoints).OnEndPointChosen();
        }

        [Condition(nameof(HasSourceAndTargetSet))]
        [MenuItem(MenuItemsNames.ReplaceTarget, 3)]
        public void ReplaceTargetVertex()
        {
            endPoints.Target.OnEndPointChosen();
            IntInput.InputEndPoint(MessagesTexts.TargetVertexChoiceMsg, graph, endPoints).OnEndPointChosen();
        }

        [MenuItem(MenuItemsNames.ClearEndPoints, 5)]
        public void ClearEndPoints()
        {
            endPoints.Reset();
        }

        [Condition(nameof(CanReplaceIntermediates))]
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

        [Condition(nameof(HasSourceAndTargetSet))]
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
        }

        private bool CanReplaceIntermediates()
        {
            return (numberOfIntermediates = endPoints.GetIntermediates().Count()) > 0;
        }

        private bool HasSourceAndTargetSet()
        {
            return endPoints.HasSourceAndTargetSet();
        }

        private bool CanChooseEndPoints()
        {
            return graph.GetAvailableIntermediatesNumber() > 0 && !endPoints.HasSourceAndTargetSet();
        }
    }
}