using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Serialization;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal abstract class ExportGraphMenuItem<TPath>
        : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<TPath> input;
        protected readonly ISerializer<SerializationInfo> graphSerializer;
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly IUnitOfWork history;
        protected readonly ILog log;

        private GraphModel graph = new();

        protected ExportGraphMenuItem(IMessenger messenger, 
            IInput<TPath> input, 
            IUnitOfWork history,
            ISerializer<SerializationInfo> graphSerializer,
            IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            ILog log)
        {
            this.messenger = messenger;
            this.input = input;
            this.graphSerializer = graphSerializer;
            this.log = log;
            this.history = history;
            this.rangeBuilder = rangeBuilder;
        }

        public virtual bool CanBeExecuted() => graph.Graph != Graph2D<Vertex>.Empty;

        public virtual async void Execute()
        {
            try
            {
                var savePath = input.Input();
                var info = new SerializationInfo
                {
                    Graph = graph.Graph,
                    GraphInformation = graph.Graph.ToString(),
                    Range = rangeBuilder.Range.GetCoordinates().ToArray()
                };
                var algorithms = history.AlgorithmRepository
                    .GetAll(a => a.GraphId == graph.Id)
                    .ToArray();
                foreach (var algorithm in algorithms)
                {
                    info.Algorithms.Add(algorithm.Name);
                    var visited = history.VisitedRepository
                        .GetBy(c => c.AlgorithmId == algorithm.Id)
                        .Coordinates;
                    info.Visited.Add(visited);
                    var obstacles = history.ObstaclesRepository
                        .GetBy(c => c.AlgorithmId == algorithm.Id)
                        .Coordinates;
                    info.Obstacles.Add(obstacles);
                    var range = history.RangesRepository
                        .GetBy(c => c.AlgorithmId == algorithm.Id)
                        .Coordinates;
                    info.Ranges.Add(range);
                    var path = history.PathsRepository
                        .GetBy(c => c.AlgorithmId == algorithm.Id)
                        .Coordinates;
                    info.Paths.Add(path);
                    var costs = history.CostsRepository
                        .GetBy(c => c.AlgorithmId == algorithm.Id)
                        .Costs;
                    info.Costs.Add(costs);
                    var statistics = history.StatisticsRepository
                        .GetBy(s => s.AlgorithmId == algorithm.Id);
                    info.Statistics.Add(statistics);
                }
                
                await ExportAsync(info, savePath);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public virtual void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<GraphModel>(this, Tokens.Common, OnGraphCreated);
        }

        protected abstract Task ExportAsync(SerializationInfo graph, TPath path);

        private void OnGraphCreated(GraphModel graph)
        {
            this.graph = graph;
        }
    }
}
