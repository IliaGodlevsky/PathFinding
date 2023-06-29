using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System;
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
                var importedHistory = ImportGraph(path);
                importedHistory.ForEach(history.Add);
                if (history.Count == importedHistory.Count)
                {
                    var graph = importedHistory.Graphs.FirstOrDefault();
                    var costRange = graph.First().Cost.CostRange;
                    messenger.SendData(costRange, Tokens.AppLayout);
                    messenger.SendData(graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
                    var range = importedHistory.GetFor(graph).PathfindingRange;
                    rangeBuilder.Undo();
                    rangeBuilder.Include(range, graph);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract TPath InputPath();

        protected abstract PathfindingHistory ImportGraph(TPath path);
    }
}
