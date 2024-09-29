using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Update;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphFieldViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexModel> service;
        private readonly ILog logger;

        private int graphId;
        public int GraphId
        {
            get => graphId;
            set => this.RaiseAndSetIfChanged(ref graphId, value);
        }

        private IGraph<VertexModel> graph;
        public IGraph<VertexModel> Graph
        {
            get => graph;
            set => this.RaiseAndSetIfChanged(ref graph, value);
        }

        public ReactiveCommand<VertexModel, Unit> ReverseVertexCommand { get; }

        public ReactiveCommand<VertexModel, Unit> IncreaseVertexCostCommand { get; }

        public ReactiveCommand<VertexModel, Unit> DecreaseVertexCostCommand { get; }

        public GraphFieldViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<VertexModel> service, ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            var canExecute = this.WhenAnyValue(x => x.GraphId, x => x.Graph,
                (id, graph) => id > 0 && graph != null);
            ReverseVertexCommand = ReactiveCommand.CreateFromTask<VertexModel>(ReverseVertex, canExecute);
            IncreaseVertexCostCommand = ReactiveCommand.CreateFromTask<VertexModel>(IncreaseVertexCost, canExecute);
            DecreaseVertexCostCommand = ReactiveCommand.CreateFromTask<VertexModel>(DecreaseVertexCost, canExecute);
        }

        private async Task ReverseVertex(VertexModel vertex)
        {
            var inRangeRquest = new IsVertexInRangeRequest(vertex);
            messenger.Send(inRangeRquest);
            if (!inRangeRquest.IsInRange)
            {
                vertex.IsObstacle = !vertex.IsObstacle;
                messenger.Send(new ObstaclesCountChangedMessage(graphId, vertex.IsObstacle ? 1 : -1));
                var request = new UpdateVerticesRequest<VertexModel>(graphId,
                    vertex.Enumerate().ToList());
                await ExecuteSafe(async () =>
                {
                    await service.UpdateVerticesAsync(request);
                    var obstacles = Graph.GetObstaclesCount();
                    var graphRequest = new UpdateGraphInfoRequest(graphId, obstacles);
                    await service.UpdateObstaclesCountAsync(graphRequest);
                }, logger.Error);
            }
        }

        private async Task IncreaseVertexCost(VertexModel vertex)
        {
            await ChangeVertexCost(vertex, 1);
        }

        private async Task DecreaseVertexCost(VertexModel vertex)
        {
            await ChangeVertexCost(vertex, -1);
        }

        private async Task ChangeVertexCost(VertexModel vertex, int delta)
        {
            var cost = vertex.Cost.CurrentCost;
            cost += delta;
            cost = vertex.Cost.CostRange.ReturnInRange(cost);
            vertex.Cost = new VertexCost(cost, vertex.Cost.CostRange);
            var request = new UpdateVerticesRequest<VertexModel>(GraphId,
                vertex.Enumerate().ToList());
            await ExecuteSafe(async () => await service.UpdateVerticesAsync(request),
                logger.Error);
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph;
            GraphId = msg.GraphId;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(GraphId))
            {
                GraphId = 0;
                Graph = Graph<VertexModel>.Empty;
            }
        }
    }
}
