using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using Shared.Process.EventArguments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingVisualizationUnit : Unit, ICanRecieveMessage
    {
        private readonly ConsoleKeystrokesHook keyStrokeHook = new();
        private readonly IReadOnlyDictionary<ConsoleKey, IPathfindingAction> pathfindingActions;
        private readonly IReadOnlyDictionary<ConsoleKey, IAnimationSpeedAction> animationActions;

        private PathfindingProcess algorithm = PathfindingProcess.Null;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isVisualizationApplied = true;
        private TimeSpan animationDelay = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;

        public PathfindingVisualizationUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IReadOnlyDictionary<ConsoleKey, IPathfindingAction> pathfindingActions,
            IReadOnlyDictionary<ConsoleKey, IAnimationSpeedAction> animationActions)
            : base(menuItems, conditioned)
        {
            this.animationActions = animationActions;
            this.pathfindingActions = pathfindingActions;
        }

        private void SetAnimationDelay(TimeSpan delay)
        {
            animationDelay = delay;
        }

        private bool IsVisualizationApplied() => isVisualizationApplied;

        private void SetIsApplied(bool isApplied)
        {
            isVisualizationApplied = isApplied;
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            animationDelay.Wait();
            graph.Get(e.Current).VisualizeAsVisited();
        }

        private void OnVertexEnqueued(object sender, PathfindingEventArgs e)
        {
            graph.Get(e.Current).VisualizeAsEnqueued();
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

        private void SubscribeOnVisualization(PathfindingProcess process)
        {
            algorithm = process;
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.Started += OnAlgorithmStarted;
            algorithm.Finished += OnAlgorithmFinished;
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            pathfindingActions.GetOrDefault(e.PressedKey)?.Do(algorithm);
            animationDelay = animationActions.GetOrDefault(e.PressedKey)?.Do(animationDelay) ?? animationDelay;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = ConditionToken.Create(IsVisualizationApplied, Tokens.Visualization);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.RegisterData<TimeSpan>(this, token, SetAnimationDelay);
            messenger.RegisterData<bool>(this, Tokens.Visualization, SetIsApplied);
            messenger.RegisterData<PathfindingProcess>(this, token, SubscribeOnVisualization);
        }
    }
}