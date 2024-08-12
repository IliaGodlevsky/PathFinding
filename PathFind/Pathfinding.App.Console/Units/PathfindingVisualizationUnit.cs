using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Service.Interface.Models.Read;
using Shared.Extensions;
using Shared.Process.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingVisualizationUnit : Unit, ICanReceiveMessage
    {
        private readonly IMessenger messenger;
        private readonly ConsoleKeystrokesHook keyStrokeHook = new();
        private readonly IReadOnlyCollection<(string, IPathfindingAction)> pathfindingActions;
        private readonly IReadOnlyCollection<(string, IAnimationSpeedAction)> animationActions;

        private PathfindingProcess algorithm = PathfindingProcess.Idle;
        private GraphModel<Vertex> graph = null;
        private bool isVisualizationApplied = true;
        private TimeSpan animationDelay = TimeSpan.FromMilliseconds(2);

        public PathfindingVisualizationUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<(string, IPathfindingAction)> pathfindingActions,
            IReadOnlyCollection<(string, IAnimationSpeedAction)> animationActions,
            IMessenger messenger)
            : base(menuItems)
        {
            this.messenger = messenger;
            this.animationActions = animationActions;
            this.pathfindingActions = pathfindingActions;
        }

        private void SetAnimationDelay(AlgorithmDelayMessage msg)
        {
            animationDelay = msg.Delay;
        }

        private bool IsVisualizationApplied() => isVisualizationApplied;

        private void SetIsApplied(IsAppliedMessage msg)
        {
            isVisualizationApplied = msg.IsApplied;
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            animationDelay.Wait();
            graph.Graph.Get(e.Current).VisualizeAsVisited();
        }

        private void OnVertexEnqueued(object sender, VerticesEnqueuedEventArgs e)
        {
            foreach (var coordinate in e.Enqueued)
            {
                graph.Graph.Get(coordinate).VisualizeAsEnqueued();
            }
        }

        private void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            messenger.Send(new AlgorithmDelayMessage(animationDelay), Tokens.Statistics);
            keyStrokeHook.KeyPressed += OnConsoleKeyPressed;
            Task.Run(keyStrokeHook.Start);
        }

        private void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            keyStrokeHook.Interrupt();
            keyStrokeHook.KeyPressed -= OnConsoleKeyPressed;
        }

        private void SubscribeOnVisualization(AlgorithmMessage msg)
        {
            algorithm = msg.Algorithm;
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

        public void RegisterHandlers(IMessenger messenger)
        {
            var token = Tokens.Visualization.Bind(IsVisualizationApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<PathfindingVisualizationUnit, AlgorithmDelayMessage>(this, token, SetAnimationDelay);
            messenger.Register<PathfindingVisualizationUnit, IsAppliedMessage>(this, Tokens.Visualization, SetIsApplied);
            messenger.Register<PathfindingVisualizationUnit, AlgorithmMessage>(this, token, SubscribeOnVisualization);
        }
    }
}