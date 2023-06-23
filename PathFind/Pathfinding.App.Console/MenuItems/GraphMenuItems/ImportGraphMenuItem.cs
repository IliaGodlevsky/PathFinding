using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
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
        protected readonly ISerializer<PathfindingHistory> serializer;
        protected readonly PathfindingHistory history;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger, 
            IInput<TPath> input,
            PathfindingHistory history,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<PathfindingHistory> serializer, ILog log)
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
                var history = ImportGraph(path);
                foreach (var note in history.History)
                {
                    this.history.History.Add(note.Key, note.Value);
                }
                if (this.history.History.Count == history.History.Count)
                {
                    var graph = history.History.Keys.LastOrDefault();
                    var costRange = graph.First().Cost.CostRange;
                    messenger.SendData(costRange, Tokens.AppLayout);
                    messenger.SendData(graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
                    var range = history.History[graph].PathfindingRange;
                    SetRange(range, graph);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void SetRange(IEnumerable<ICoordinate> range, Graph2D<Vertex> graph)
        {
            var pathfindingRange = range.ToList();
            if (pathfindingRange.Count > 2)
            {
                var target = pathfindingRange[pathfindingRange.Count - 1];
                pathfindingRange.RemoveAt(pathfindingRange.Count - 1);
                pathfindingRange.Insert(1, target);
            }
            rangeBuilder.Undo();
            rangeBuilder.Include(pathfindingRange, graph);
        }

        protected abstract TPath InputPath();

        protected abstract PathfindingHistory ImportGraph(TPath path);
    }
}
