using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Extensions;
using Common.ValueRanges;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using static ConsoleVersion.Constants;

using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel,
        IModel, IInterruptable, IRequireInt32Input, IRequireAnswerInput
    {
        public event ProcessEventHandler Interrupted;

        public string AlgorithmKeyInputMessage { private get; set; }

        public string TargetVertexInputMessage { private get; set; }

        public IValueInput<int> Int32Input { get; set; }
        public IValueInput<Answer> AnswerInput { get; set; }

        public string SourceVertexInputMessage { private get; set; }

        public PathFindingViewModel(ILog log, MainViewModel model, BaseEndPoints endPoints)
            : base(log, model.Graph, endPoints)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Length, 1);
            //keyStrokesHook = new ConsoleKeystrokesHook(ConsoleKey.Escape, ConsoleKey.End);            
            this.mainModel = model;
        }

        [MenuItem(Constants.FindPath, MenuItemPriority.Highest)]
        public override void FindPath()
        {
            if (!endPoints.HasIsolators())
            {
                try
                {
                    Console.CursorVisible = false;
                    base.FindPath();
                    //keyStrokesHook.KeystrokeHooked += algorithm.Interrupt;
                    //keyStrokesHook.StartHookingConsoleKeystrokes();
                    Console.CursorVisible = true;
                    //keyStrokesHook.KeystrokeHooked -= algorithm.Interrupt;
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
            string statistics = !path.IsNull() ? Statistics : CouldntFindPath;
            mainModel.Statistics = Statistics;
            visitedVerticesCount = 0;
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Pause(DelayTime).Cancel();
            base.OnVertexVisited(sender, e);
            mainModel.Statistics = Statistics;
        }

        [MenuItem(Constants.ChooseAlgorithm, MenuItemPriority.High)]
        public void ChooseAlgorithm()
        {
            int algorithmKeyIndex = Int32Input.InputValue(AlgorithmKeyInputMessage,
                algorithmKeysValueRange) - 1;
            Algorithm = Algorithms[algorithmKeyIndex].Item2;
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
                var selection = new EndPointsSelection(endPoints, graph, NumberOfAvailableIntermediate)
                {
                    SourceVertexInputMsg = SourceVertexInputMessage,
                    TargetVertexInputMsg = TargetVertexInputMessage,
                    Int32Input = Int32Input
                };
                selection.ChooseEndPoints();
            }
            else
            {
                log.Warn("No vertices to choose as end points");
            }
        }

        [MenuItem(Constants.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph()
        {
            mainModel.ClearGraph();
        }

        [MenuItem(Constants.ClearColors, MenuItemPriority.Low)]
        public void ClearColors()
        {
            mainModel.ClearColors();
        }

        [MenuItem(Constants.ApplyVisualization, MenuItemPriority.Low)]
        public void ApplyVisualization()
        {
            var answer = AnswerInput.InputValue(VisualizationMsg, Constants.AnswerValueRange);
            IsVisualizationRequired = answer == Answer.Yes;
        }

        protected override void SubscribeOnAlgorithmEvents(IAlgorithm algorithm)
        {
            base.SubscribeOnAlgorithmEvents(algorithm);
            //algorithm.Finished += keyStrokesHook.CancelKeyStrokeHooking;
        }

        private string Statistics
        {
            get
            {
                string timerInfo = timer.ToFormattedString();
                string description = Algorithms.FirstOrDefault(item => item.Item2 == Algorithm).Item1;
                string pathfindingInfo = string.Format(Format, PathfindingInfo);
                return string.Join("\t", description, timerInfo, pathfindingInfo);
            }
        }

        private object[] PathfindingInfo => new object[] { path.PathLength, path.PathCost, visitedVerticesCount };

        private readonly string Format = "Steps: {0}  Path cost: {1}  Visited: {2}";
        private readonly string CouldntFindPath = "Could't fing path";

        private int NumberOfAvailableIntermediate => graph.Size - graph.Vertices.Count(v => v.IsIsolated()) - 2;
        private bool HasAnyVerticesToChooseAsEndPoints => NumberOfAvailableIntermediate >= 0;

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        //private readonly ConsoleKeystrokesHook keyStrokesHook;
        private readonly MainViewModel mainModel;
    }
}