using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Visualizations.VisualizationUnits;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit, ICanRecieveMessage
    {
        private readonly IService service;
        private readonly IPathfindingRangeBuilder<Vertex> builder;
        private readonly List<(ICoordinate, IReadOnlyList<ICoordinate>)> visitedVertices = new();
        private readonly List<SubAlgorithmCreateDto> subAlgorithms = new();

        private RunStatisticsDto runStatistics = new();
        private GraphReadDto Graph = GraphReadDto.Empty;
        private PathfindingProcess algorithm = PathfindingProcess.Idle;

        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IPathfindingRangeBuilder<Vertex> builder,
            IService service) : base(menuItems)
        {
            this.service = service;
            this.builder = builder;
        }

        private void VisualizeHistory(AlgorithmKeyMessage msg)
        {
            var algorithm = service.GetRunInfo(msg.AlgorithmKey);
            var units = new VisualizationUnits(algorithm);
            units.Visualize(Graph.Graph);
        }

        private void SetIsApplied(IsAppliedMessage msg)
        {
            isHistoryApplied = msg.IsApplied;
        }

        private void SetGraph(GraphMessage msg)
        {
            Graph = msg.Graph;
        }

        private void OnVertexEnqueued(object sender, VerticesEnqueuedEventArgs e)
        {
            visitedVertices.Add((e.Current, e.Enqueued));
        }

        private void OnStarted(object sender, EventArgs args)
        {
            var graph = Graph.Graph;
        }

        private void PrepareForPathfinding(AlgorithmMessage msg)
        {
            subAlgorithms.Clear();
            visitedVertices.Clear();
            algorithm = msg.Algorithm;
            algorithm.Started += OnStarted;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.SubPathFound += OnSubPathFound;
        }

        private void OnSubPathFound(object sender, SubPathFoundEventArgs args)
        {
            var subAlgorithm = new SubAlgorithmCreateDto()
            {
                Path = args.SubPath,
                Visited = visitedVertices.ToList().AsReadOnly()
            };
            subAlgorithms.Add(subAlgorithm);
            visitedVertices.Clear();
        }

        private void SetStatistics(StatisticsMessage msg)
        {
            runStatistics = msg.Statistics;
        }

        private async void OnPathFound(PathFoundMessage msg)
        {
            AlgorithmRunHistoryCreateDto algorithmRunCreateDto = new()
            {
                Run = new() 
                { 
                    GraphId = Graph.Id, 
                    AlgorithmId = runStatistics.AlgorithmId 
                },
                GraphState = new()
                {
                    Obstacles = Graph.Graph.GetObstaclesCoordinates().ToReadOnly(),
                    Costs = Graph.Graph.GetCosts(),
                    Range = builder.Range.GetCoordinates().ToReadOnly()
                },
                SubAlgorithms = subAlgorithms.ToList(),
                Statistics = runStatistics
            };
            await Task.Run(() => service.AddRunHistory(algorithmRunCreateDto));
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.History.Bind(IsHistoryApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<PathfindingHistoryUnit, IsAppliedMessage>(this, Tokens.History, SetIsApplied);
            messenger.Register<PathfindingHistoryUnit, AlgorithmKeyMessage>(this, token, VisualizeHistory);
            messenger.Register<PathfindingHistoryUnit, AlgorithmMessage>(this, token, PrepareForPathfinding);
            messenger.Register<PathfindingHistoryUnit, PathFoundMessage>(this, token, OnPathFound);
            messenger.Register<PathfindingHistoryUnit, StatisticsMessage>(this, token, SetStatistics);
            //messenger.Register<PathfindingHistoryUnit, ClearHistoryMessage>(this, Tokens.History, ClearHistory);
        }
    }
}