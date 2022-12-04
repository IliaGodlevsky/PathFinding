using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    [InstancePerLifetimeScope]
    internal sealed class PathfindingVisualizationViewModel : ViewModel, IRequireAnswerInput, IRequireTimeSpanInput
    {
        private static readonly TimeSpan Millisecond = TimeSpan.FromMilliseconds(1);

        private readonly IMessenger messenger;
        private readonly ConsoleKeystrokesHook keyStrokeHook;

        private bool isVisualizationApplied;

        private InclusiveValueRange<TimeSpan> DelayRange => Constants.AlgorithmDelayTimeValueRange;

        private Graph2D<Vertex> Graph { get; }

        public IInput<TimeSpan> TimeSpanInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        private TimeSpan AnimationDelay { get; set; } = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;

        public PathfindingVisualizationViewModel(ICache<Graph2D<Vertex>> graphCache, 
            ConsoleKeystrokesHook keyStrokeHook, IMessenger messenger)
        {
            this.keyStrokeHook = keyStrokeHook;
            this.messenger = messenger;
            Graph = graphCache.Cached;
            this.keyStrokeHook.KeyPressed += OnConsoleKeyPressed;
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
            Graph.Get(e.Current).VisualizeAsVisited();
        }

        private void OnVertexEnqueued(object sender, PathfindingEventArgs e)
        {
            Graph.Get(e.Current).VisualizeAsEnqueued();
        }

        private void OnPathfindingPrepare(SubscribeOnVisualizationMessage message)
        {
            if (IsVisualizationApplied())
            {
                message.Algorithm.VertexVisited += (s, e) => AnimationDelay.Wait();
                message.Algorithm.VertexVisited += OnVertexVisited;
                message.Algorithm.VertexEnqueued += OnVertexEnqueued;
            }
        }

        [FailMessage(MessagesTexts.VisualizationIsNotAppliedMsg)]
        private bool IsVisualizationApplied() => isVisualizationApplied;

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            switch (e.PressedKey)
            {
                case ConsoleKey.DownArrow:
                    AnimationDelay = DelayRange.ReturnInRange(AnimationDelay - Millisecond);
                    break;
                case ConsoleKey.UpArrow:
                    AnimationDelay = DelayRange.ReturnInRange(AnimationDelay + Millisecond);
                    break;
            }
        }
    }
}
