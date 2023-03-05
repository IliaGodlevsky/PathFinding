using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingProcessUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IInput<ConsoleKey> input;
        private readonly ILog log;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public PathfindingProcessUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IInput<ConsoleKey> input, IMessenger messenger, ILog log)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
            this.log = log;
            this.rangeBuilder = rangeBuilder;
            this.input = input;
        }

        private void FindPath(PathfindingAlgorithmChosenMessage message)
        {
            var factory = message.Algorithm;
            var range = rangeBuilder.Range;
            using var algorithm = factory.Create(range);
            using (Disposable.Use(ClearColors))
            {
                using (Cursor.HideCursor())
                {
                    try
                    {
                        FindPath(algorithm);
                    }
                    catch (PathfindingException ex)
                    {
                        log.Warn(ex.Message);
                    }
                }
            }
        }

        private void ClearColors()
        {
            graph.ForEach(v => v.RestoreDefaultVisualState());
            rangeBuilder.Range.RestoreVerticesVisualState();
            messenger.Send(PathfindingStatisticsMessage.Empty);
        }

        private void FindPath(PathfindingProcess algorithm)
        {
            var path = NullGraphPath.Interface;
            void Summarize()
            {
                messenger.Send(new PathFoundMessage(path, algorithm), MessageTokens.HistoryUnit);
                messenger.Send(new PathFoundMessage(path, algorithm), MessageTokens.StatisticsUnit);
            }
            using (Disposable.Use(Summarize))
            {
                PrepareForPathfinding(algorithm);
                path = algorithm.FindPath();
                var vertices = path.Select(graph.Get);
                vertices.ForEach(v => v.VisualizeAsPath());
            }
            input.Input();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        private void PrepareForPathfinding(PathfindingProcess algorithm)
        {
            messenger.Send(new PathfindingStatisticsMessage(algorithm.ToString()));
            messenger.Send(new SubscribeOnVisualizationMessage(algorithm), MessageTokens.VisualizationUnit);
            messenger.Send(new SubscribeOnHistoryMessage(algorithm), MessageTokens.HistoryUnit);
            var msg = new PathfindingRangeChosenMessage(rangeBuilder.Range, algorithm.Id);
            messenger.Send(msg, MessageTokens.HistoryUnit);
            messenger.Send(new SubscribeOnStatisticsMessage(algorithm), MessageTokens.StatisticsUnit);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<PathfindingAlgorithmChosenMessage>(this, FindPath);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<ClearColorsMessage>(this, _ => ClearColors());
        }
    }
}