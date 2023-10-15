using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using Shared.Process.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingVisualizationUnit : Unit, ICanRecieveMessage
    {
        private readonly GraphsPathfindingHistory history;
        private readonly ConsoleKeystrokesHook keyStrokeHook = new();
        private readonly IReadOnlyCollection<(string, IPathfindingAction)> pathfindingActions;
        private readonly IReadOnlyCollection<(string, IAnimationSpeedAction)> animationActions;

        private PathfindingProcess algorithm = PathfindingProcess.Idle;
        private IGraph<Vertex> graph = Graph<Vertex>.Empty;
        private bool isVisualizationApplied = true;
        private TimeSpan animationDelay = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;

        public PathfindingVisualizationUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IReadOnlyCollection<(string, IPathfindingAction)> pathfindingActions,
            IReadOnlyCollection<(string, IAnimationSpeedAction)> animationActions,
            GraphsPathfindingHistory history)
            : base(menuItems, conditioned)
        {
            this.animationActions = animationActions;
            this.pathfindingActions = pathfindingActions;
            this.history = history;
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

        private void SetGraph(IGraph<Vertex> graph)
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
            algorithm = PathfindingProcess.Idle;
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
            GetOrDefault(e.PressedKey, pathfindingActions)?.Do(algorithm);
            var action = GetOrDefault(e.PressedKey, animationActions);
            animationDelay = action?.Do(animationDelay) ?? animationDelay;
        }

        private T GetOrDefault<T>(ConsoleKey key, IReadOnlyCollection<(string SourceName, T Action)> actions)
        {
            return actions
                .FirstOrDefault(action => Keys.Default[action.SourceName].Equals(key))
                .Action;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Visualization.Bind(IsVisualizationApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.RegisterData<TimeSpan>(this, token, SetAnimationDelay);
            messenger.RegisterData<bool>(this, Tokens.Visualization, SetIsApplied);
            messenger.RegisterData<PathfindingProcess>(this, token, SubscribeOnVisualization);
        }
    }
}