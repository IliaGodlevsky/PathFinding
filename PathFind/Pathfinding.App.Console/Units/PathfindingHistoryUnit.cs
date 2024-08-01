using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Visualizations.VisualizationUnits;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit, ICanReceiveMessage
    {
        private readonly IRequestService<Vertex> service;
        private readonly IPathfindingRangeBuilder<Vertex> builder;
        private readonly List<(ICoordinate, IReadOnlyList<ICoordinate>)> visitedVertices = new();
        private readonly List<CreateSubAlgorithmRequest> subAlgorithms = new();

        private RunStatisticsModel runStatistics = new();
        private GraphModel<Vertex> Graph = null;
        private PathfindingProcess algorithm = PathfindingProcess.Idle;

        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IPathfindingRangeBuilder<Vertex> builder,
            IRequestService<Vertex> service) : base(menuItems)
        {
            this.service = service;
            this.builder = builder;
        }

        private async void VisualizeHistory(AlgorithmKeyMessage msg)
        {
            var algorithm = await service.ReadRunInfoAsync(msg.AlgorithmKey);
            var layers = new VisualizationLayers(algorithm);
            layers.Overlay(Graph.Graph);
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
            algorithm = msg.Algorithm;
            algorithm.Started += OnStarted;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.SubPathFound += OnSubPathFound;
        }

        private void OnSubPathFound(object sender, SubPathFoundEventArgs args)
        {
            var subAlgorithm = new CreateSubAlgorithmRequest()
            {
                Path = args.SubPath,
                Visited = visitedVertices.ToReadOnly(),
                Order = subAlgorithms.Count
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
            AlgorithmRunHistoryModel algorithmRunCreateDto = new()
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
                SubAlgorithms = subAlgorithms.ToReadOnly(),
                Statistics = runStatistics
            };
            subAlgorithms.Clear();
            visitedVertices.Clear();
            runStatistics = new();
            await Task.Run(() => service.AddRunHistory(algorithmRunCreateDto));
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        public void RegisterHandlers(IMessenger messenger)
        {
            var token = Tokens.History.Bind(IsHistoryApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.Register<PathfindingHistoryUnit, IsAppliedMessage>(this, Tokens.History, SetIsApplied);
            messenger.Register<PathfindingHistoryUnit, AlgorithmKeyMessage>(this, token, VisualizeHistory);
            messenger.Register<PathfindingHistoryUnit, AlgorithmMessage>(this, token, PrepareForPathfinding);
            messenger.Register<PathfindingHistoryUnit, PathFoundMessage>(this, token, OnPathFound);
            messenger.Register<PathfindingHistoryUnit, StatisticsMessage>(this, token, SetStatistics);
        }
    }
}