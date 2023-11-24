using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
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

        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

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
                var algorithm = msg.Factory.Create(rangeBuilder.Range);
                try
                {
                    PrepareForPathfinding(algorithm, msg.InitStatistics);
                    FindPath(algorithm);
                }
                catch (PathfindingException ex)
                {
                    log.Warn(ex.Message);
                }
                finally
                {
                    ClearColors();
                    algorithm.Dispose();
                }
            }
        }

        private void ClearColors()
        {
            graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
            var msg = new StatisticsLineMessage(string.Empty);
            messenger.Send(msg, Tokens.AppLayout);
        }

        private void FindPath(PathfindingProcess algorithm)
        {
            var path = NullGraphPath.Interface;
            try
            {
                path = algorithm.FindPath();
                path.Select(graph.Get).VisualizeAsPath();
            }
            finally
            {
                var msg = new PathFoundMessage(path);
                messenger.SendMany(msg, Tokens.Statistics, Tokens.History);
                input.Input();
            }
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        private void PrepareForPathfinding(PathfindingProcess algorithm, 
            Statistics statistics)
        {
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

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<AlgorithmStartInfoMessage>(this, Tokens.Pathfinding, FindPath);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<ClearColorsMessage>(this, _ => ClearColors());
        }
    }
}