using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingProcessUnit : Unit, ICanReceiveMessage
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IInput<ConsoleKey> input;
        private readonly ILog log;

        private GraphReadDto<Vertex> graph = GraphReadDto<Vertex>.Empty;

        public PathfindingProcessUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IInput<ConsoleKey> input,
            IMessenger messenger,
            ILog log) : base(menuItems)
        {
            this.messenger = messenger;
            this.log = log;
            this.rangeBuilder = rangeBuilder;
            this.input = input;
        }

        private void FindPath(AlgorithmStartInfoMessage msg)
        {
            using (Cursor.HideCursor())
            {
                var range = rangeBuilder.Range.ToReadOnly();
                var algorithm = msg.Factory.Create(range);
                var path = NullGraphPath.Interface;
                try
                {
                    PrepareForPathfinding(algorithm, msg.InitStatistics);
                    path = algorithm.FindPath();
                }
                catch (PathfindingException ex)
                {
                    log.Warn(ex.Message);
                }
                finally
                {
                    var pathFound = new PathFoundMessage(path);
                    messenger.SendMany(pathFound,
                        Tokens.Statistics, Tokens.History);
                    input.Input();
                    ClearColors(null, null);
                    algorithm.Dispose();
                }
            }
        }

        private void ClearColors(PathfindingProcessUnit unit, ClearColorsMessage m)
        {
            graph.Graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
            var msg = new StatisticsLineMessage(string.Empty);
            messenger.Send(msg, Tokens.AppLayout);
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        private void OnSubPathFound(object sender, SubPathFoundEventArgs e)
        {
            e.SubPath.Select(graph.Graph.Get).Reverse().VisualizeAsPath();
        }

        private void PrepareForPathfinding(PathfindingProcess algorithm,
            RunStatisticsDto statistics)
        {
            algorithm.SubPathFound += OnSubPathFound;
            var lineMsg = new StatisticsLineMessage(statistics.Name);
            messenger.Send(lineMsg, Tokens.AppLayout);
            var algorithmMsg = new AlgorithmMessage(algorithm);
            messenger.SendMany(algorithmMsg, Tokens.Visualization,
                Tokens.Statistics, Tokens.History);
            var algoStatMsg = new StatisticsMessage(statistics);
            messenger.Send(algoStatMsg, Tokens.History);
            var statMsg = new StatisticsMessage(statistics);
            messenger.Send(statMsg, Tokens.Statistics);
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.Register<PathfindingProcessUnit, AlgorithmStartInfoMessage>(this, Tokens.Pathfinding, FindPath);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<PathfindingProcessUnit, ClearColorsMessage>(this, ClearColors);
        }
    }
}