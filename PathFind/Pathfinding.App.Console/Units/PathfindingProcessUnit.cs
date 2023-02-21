using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
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

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingProcessUnit : Unit
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
            messenger.Register<PathfindingAlgorithmChosenMessage>(this, FindPath);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<ClearColorsMessage>(this, _ => ClearColors());
        }

        private void FindPath(PathfindingAlgorithmChosenMessage message)
        {
            var factory = message.Algorithm;
            using (var algorithm = factory.Create(rangeBuilder.Range))
            {
                using (Cursor.HideCursor())
                {
                    FindPath(algorithm);
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
            try
            {
                var path = NullGraphPath.Interface;
                void Summarize()
                {
                    messenger.Send(new PathFoundMessage(path, algorithm));
                }
                using (Disposable.Use(Summarize))
                {
                    PrepareForPathfinding(algorithm);
                    path = algorithm.FindPath();
                    graph.GetVertices(path).ForEach(v => v.VisualizeAsPath());
                }
                input.Input();
                ClearColors();
            }
            catch (PathfindingException ex)
            {
                log.Debug(ex.Message);
            }
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        private void PrepareForPathfinding(PathfindingProcess algorithm)
        {
            messenger.Send(new SubscribeOnVisualizationMessage(algorithm));
            messenger.Send(new SubscribeOnHistoryMessage(algorithm));
            messenger.Send(new PathfindingRangeChosenMessage(rangeBuilder.Range, algorithm.Id));
            messenger.Send(new SubscribeOnStatisticsMessage(algorithm));
        }
    }
}