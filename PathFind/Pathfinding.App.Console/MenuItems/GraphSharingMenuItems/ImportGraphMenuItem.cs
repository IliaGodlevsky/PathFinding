using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.DataAccess.ReadDto;
using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    internal abstract class ImportGraphMenuItem<TPath> : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly ISerializer<GraphsPathfindingHistory> serializer;
        protected readonly IDbContextService service;
        protected readonly ILog log;

        protected ImportGraphMenuItem(IMessenger messenger,
            IInput<TPath> input,
            IDbContextService service,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            ISerializer<GraphsPathfindingHistory> serializer, ILog log)
        {
            this.service = service;
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
                foreach (int id in importedHistory.Ids)
                {
                    var graph = importedHistory.GetGraph(id);
                    int graphId = service.AddGraph(graph);
                    foreach (var range in importedHistory.GetRange(id))
                    {
                        var vertex = graph.Get(range.Item2);
                        service.AddRangeVertex(vertex, range.Item1);
                    }
                    var history = importedHistory.GetHistory(id);
                    foreach (var algo in history.Algorithms)
                    {
                        var create = new AlgorithmCreateDto()
                        {
                            GraphId = graphId,
                            Costs = history.Costs[id],
                            Range = history.Ranges[id],
                            Obstacles = history.Obstacles[id],
                            Visited = history.Visited[id],
                            Statistics = history.Statistics[id],
                            Path = history.Paths[id],
                        };
                        service.AddAlgorithm(create);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected abstract TPath InputPath();

        protected abstract GraphsPathfindingHistory ImportGraph(TPath path);
    }
}
