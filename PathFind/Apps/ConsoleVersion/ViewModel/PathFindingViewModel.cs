using Algorithm.Infrastructure.EventArguments;
using Common.Extensions;
using Common.ValueRanges;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.EventArguments;
using ConsoleVersion.EventHandlers;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Graphs;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Linq;
using static ConsoleVersion.Constants;

using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel,
        IModel, IInterruptable, IRequireInt32Input, IRequireAnswerInput
    {
        public event ProcessEventHandler Interrupted;

        public bool IsAlgorithmFindingPath { get; set; }

        public string AlgorithmKeyInputMessage { private get; set; }

        public string TargetVertexInputMessage { private get; set; }

        public IValueInput<int> Int32Input { get; set; }
        public IValueInput<Answer> AnswerInput { get; set; }

        public string SourceVertexInputMessage { private get; set; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Length, 1);
            mainModel = model as MainViewModel ?? throw new ArgumentException();
        }

        [MenuItem(Constants.FindPath, MenuItemPriority.Highest)]
        public override void FindPath()
        {
            if (!endPoints.HasIsolators())
            {
                try
                {
                    Console.CursorVisible = false;
                    IsAlgorithmFindingPath = true;
                    base.FindPath();
                    while (IsAlgorithmFindingPath)
                    {
                        HookAlgorithmInterruptionKeystrokes();
                    }
                    Console.CursorVisible = true;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            else
            {
                log.Warn("Firstly choose endpoints");
            }
        }

        protected override void Summarize()
        {
            string statistics = !path.IsNull() ? GetStatistics() : CouldntFindPathMsg;
            Messenger.Default.Send(new UpdateStatisticsMessage(statistics), Constants.MessageToken);
            visitedVerticesCount = 0;
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            timer.Wait(DelayTime);
            Messenger.Default.Send(new UpdateStatisticsMessage(GetStatistics()), Constants.MessageToken);
            base.OnVertexVisited(sender, e);
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmInterrupted(sender, e);
            IsAlgorithmFindingPath = false;
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            IsAlgorithmFindingPath = false;
        }

        [MenuItem(Constants.ChooseAlgorithm, MenuItemPriority.High)]
        public void ChooseAlgorithm()
        {
            int algorithmKeyIndex = Int32Input.InputValue(AlgorithmKeyInputMessage,
                algorithmKeysValueRange) - 1;
            Algorithm = Algorithms.ElementAt(algorithmKeyIndex).Item2;
        }

        [MenuItem(Constants.InputDelayTime)]
        public void InputDelayTime()
        {
            if (IsVisualizationRequired)
            {
                DelayTime = Int32Input.InputValue(DelayTimeInputMsg, AlgorithmDelayTimeValueRange);
            }
        }

        [MenuItem(Constants.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            ClearGraph();
            Interrupted?.Invoke(this, new ProcessEventArgs());
            Interrupted = null;
        }

        [MenuItem(Constants.ChooseEndPoints, MenuItemPriority.High)]
        public void ChooseExtremeVertex()
        {
            if (HasAnyVerticesToChooseAsEndPoints)
            {
                string inputMessage = "Input number of intermediate vertices: ";
                string intermediateInputMessage = "Choose intermediate vertex";
                int cursorLeft = Console.CursorLeft;
                int cursorRight = Console.CursorTop;
                int numberOfIntermediates = Int32Input.InputValue(inputMessage, NumberOfAvailableIntermediate);
                var messages = Enumerable.Repeat(intermediateInputMessage, numberOfIntermediates);
                endPoints.Reset();
                var chooseMessages = new[] { SourceVertexInputMessage, TargetVertexInputMessage }.Concat(messages);
                foreach (var message in chooseMessages)
                {
                    Console.SetCursorPosition(cursorLeft, cursorRight);
                    var vertex = ChooseVertex(message);
                    cursorLeft = Console.CursorLeft;
                    cursorRight = Console.CursorTop;
                    (vertex as Vertex)?.SetAsExtremeVertex();
                }
            }
            else
            {
                log.Warn("No vertices to choose as end points");
            }
        }

        [MenuItem(Constants.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph()
        {
            mainViewModel.ClearGraph();
        }

        [MenuItem(Constants.ClearColors, MenuItemPriority.Low)]
        public void ClearColors()
        {
            mainViewModel.ClearColors();
        }

        [MenuItem(Constants.ApplyVisualization, MenuItemPriority.Low)]
        public void ApplyVisualization()
        {
            var answer = AnswerInput.InputValue(VisualizationMsg, Constants.AnswerValueRange);
            IsVisualizationRequired = answer == Answer.Yes;
        }

        private IVertex ChooseVertex(string message)
        {
            if (mainViewModel.Graph is Graph2D graph2D)
            {
                Console.WriteLine(message);
                IVertex vertex;
                do
                {
                    vertex = Int32Input.InputVertex(graph2D);
                } while (!endPoints.CanBeEndPoint(vertex));

                return vertex;
            }
            return new NullVertex();
        }

        private void HookAlgorithmInterruptionKeystrokes()
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Escape:
                case ConsoleKey.End:
                    algorithm.Interrupt();
                    break;
            }
        }

        private int NumberOfAvailableIntermediate
            => mainModel.Graph.Size - mainModel.Graph.Vertices.Count(v => v.IsIsolated()) - 2;
        private bool HasAnyVerticesToChooseAsEndPoints => NumberOfAvailableIntermediate >= 0;

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        private readonly MainViewModel mainModel;
    }
}