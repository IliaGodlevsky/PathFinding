using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Model.PathfindingActions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Process.EventArguments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    [InstancePerLifetimeScope]
    internal sealed class PathfindingVisualizationViewModel : ViewModel, IRequireAnswerInput, IRequireTimeSpanInput
    {
        private readonly IMessenger messenger;
        private readonly ConsoleKeystrokesHook keyStrokeHook;

        private bool isVisualizationApplied = false;
        private PathfindingProcess algorithm = PathfindingProcess.Null;

        public IReadOnlyDictionary<ConsoleKey, IPathfindingAction> PathfindingActions { get; set; }

        public IReadOnlyDictionary<ConsoleKey, IAnimationSpeedAction> AnimationActions { get; set; }

        public IInput<TimeSpan> TimeSpanInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        private Graph2D<Vertex> Graph { get; }

        private InclusiveValueRange<TimeSpan> DelayRange => Constants.AlgorithmDelayTimeValueRange;        

        private TimeSpan AnimationDelay { get; set; } = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;

        public PathfindingVisualizationViewModel(ICache<Graph2D<Vertex>> graphCache, 
            ConsoleKeystrokesHook keyStrokeHook, IMessenger messenger)
        {
            this.keyStrokeHook = keyStrokeHook;
            this.messenger = messenger;
            Graph = graphCache.Cached;
            this.messenger.Register<SubscribeOnVisualizationMessage>(this, OnPathfindingPrepare);
        }

        [MenuItem(MenuItemsNames.ApplyVisualization, 0)]
        private void ApplyVisualization()
        {
            using (Cursor.CleanUpAfter())
            {
                isVisualizationApplied = AnswerInput.Input(MessagesTexts.ApplyVisualizationMsg, Answer.Range);
            }
        }

        [Condition(nameof(IsVisualizationApplied))]
        [MenuItem(MenuItemsNames.InputDelayTime, 1)]
        private void SetAnimationDelay()
        {
            using (Cursor.CleanUpAfter())
            {
                AnimationDelay = TimeSpanInput.Input(MessagesTexts.DelayTimeInputMsg, DelayRange);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            keyStrokeHook.KeyPressed -= OnConsoleKeyPressed;
            messenger.Unregister(this);
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            AnimationDelay.Wait();
            Graph.Get(e.Current).VisualizeAsVisited();
        }

        private void OnVertexEnqueued(object sender, PathfindingEventArgs e)
        {
            Graph.Get(e.Current).VisualizeAsEnqueued();
        }

        private void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            keyStrokeHook.KeyPressed += OnConsoleKeyPressed;
            Task.Run(keyStrokeHook.Start);
        }

        private void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            keyStrokeHook.Interrupt();
            keyStrokeHook.KeyPressed -= OnConsoleKeyPressed;
            algorithm = PathfindingProcess.Null;
        }

        private void OnPathfindingPrepare(SubscribeOnVisualizationMessage message)
        {
            if (IsVisualizationApplied())
            {
                algorithm = message.Algorithm;
                algorithm.VertexVisited += OnVertexVisited;
                algorithm.VertexEnqueued += OnVertexEnqueued;
                algorithm.Started += OnAlgorithmStarted;
                algorithm.Finished += OnAlgorithmFinished;
            }
        }

        [FailMessage(MessagesTexts.VisualizationIsNotAppliedMsg)]
        private bool IsVisualizationApplied() => isVisualizationApplied;

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            PathfindingActions
                .GetOrDefault(e.PressedKey, NullPathfindingAction.Interface)
                .Do(algorithm);

            AnimationDelay = AnimationActions
                .GetOrDefault(e.PressedKey, NullAnimationAction.Instance)
                .Do(AnimationDelay);
        }
    }
}