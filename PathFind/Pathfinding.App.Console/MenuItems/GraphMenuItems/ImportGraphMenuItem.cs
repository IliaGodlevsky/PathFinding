using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Serialization;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal abstract class ImportGraphMenuItem<TPath> : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly ISerializer<SerializationInfo> serializer;
        protected readonly IPathfindingHistory history;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger, 
            IInput<TPath> input,
            IPathfindingHistory history,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<SerializationInfo> serializer, ILog log)
        {
            this.history = history;
            this.rangeBuilder = rangeBuilder;
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public virtual void Execute()
        {
            try
            {
                var path = InputPath();
                var info = ImportGraph(path);
                SetGraph(info.Graph);
                SetRange(info.Range, info.Graph);
                SetUnitOfWork(info.History);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            var costRange = graph.First().Cost.CostRange;
            messenger.SendData(costRange, Tokens.AppLayout);
            messenger.SendData(graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
        }

        private void SetRange(IEnumerable<ICoordinate> range, Graph2D<Vertex> graph)
        {
            var pathfindingRange = range.ToList();
            var target = pathfindingRange[pathfindingRange.Count - 1];
            pathfindingRange.RemoveAt(pathfindingRange.Count - 1);
            pathfindingRange.Insert(1, target);
            rangeBuilder.Undo();
            rangeBuilder.Include(pathfindingRange, graph);
        }

        private void SetUnitOfWork(IPathfindingHistory h)
        {
            foreach (var key in h.Algorithms)
            {
                history.Algorithms.Add(key);
                history.VisitedVertices.Add(key, h.VisitedVertices.Get(key));
                history.ObstacleVertices.Add(key, h.ObstacleVertices.Get(key));
                history.PathVertices.Add(key, h.PathVertices.Get(key));
                history.RangeVertices.Add(key, h.RangeVertices.Get(key));
                history.Costs.Add(key, h.Costs.Get(key));
                history.Statistics.Add(key, h.Statistics.Get(key));
            }
        }

        protected abstract TPath InputPath();

        protected abstract SerializationInfo ImportGraph(TPath path);
    }
}
