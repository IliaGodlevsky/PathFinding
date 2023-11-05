using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Factory.Interface;
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

        private void FindPath((IAlgorithmFactory<PathfindingProcess> Factory, Statistics Statistics) info)
        {
            using (Cursor.HideCursor())
            {
                var algorithm = info.Factory.Create(rangeBuilder.Range);
                try
                {
                    PrepareForPathfinding((algorithm, info.Statistics));
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
            messenger.SendData(string.Empty, Tokens.AppLayout);
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
                messenger.SendData((algorithm, path), 
                    Tokens.Statistics, Tokens.History);
                input.Input();
            }
        }

        private void SetGraph(IGraph<Vertex> graph)
        {
            this.graph = graph;
        }

        private void PrepareForPathfinding((PathfindingProcess Algorithm, Statistics Statistics) info)
        {
            messenger.SendData(info.Statistics.Name, Tokens.AppLayout);
            messenger.SendData(info.Algorithm, Tokens.Visualization, 
                Tokens.Statistics, Tokens.History);
            messenger.SendAlgorithmData(info.Algorithm, info.Statistics, Tokens.History);
            messenger.SendData(info.Statistics, Tokens.Statistics);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<(IAlgorithmFactory<PathfindingProcess>, Statistics)>(this, Tokens.Pathfinding, FindPath);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<ClearColorsMessage>(this, _ => ClearColors());
        }
    }
}