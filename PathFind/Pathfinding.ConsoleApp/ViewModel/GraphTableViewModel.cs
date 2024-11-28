using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Infrastructure.Business.Builders;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphTableViewModel : BaseViewModel, IGraphTableViewModel
    {
        private readonly IRequestService<GraphVertexModel> service;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        public ReactiveCommand<Unit, Unit> LoadGraphsCommand { get; }

        public ReactiveCommand<int, Unit> ActivateGraphCommand { get; }

        public ReactiveCommand<int[], Unit> SelectGraphsCommand { get; }

        public ObservableCollection<GraphInfoModel> Graphs { get; } = new();

        private int ActivatedGraphId { get; set; } = 0;

        public GraphTableViewModel(
            IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.messenger = messenger;
            this.logger = logger;
            messenger.Register<GraphCreatedMessage>(this, async (r, msg) => await OnGraphCreated(r, msg));
            messenger.Register<GraphUpdatedMessage, int>(this, Tokens.GraphTable, async (r, msg) 
                => await OnGraphUpdated(r, msg));
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<ObstaclesCountChangedMessage>(this, OnObstaclesCountChanged);
            messenger.Register<GraphStateChangedMessage>(this, GraphStateChanged);
            LoadGraphsCommand = ReactiveCommand.CreateFromTask(LoadGraphs);
            ActivateGraphCommand = ReactiveCommand.CreateFromTask<int>(ActivatedGraph);
            SelectGraphsCommand = ReactiveCommand.Create<int[]>(SelectGraphs);
        }

        private void SelectGraphs(int[] selected)
        {
            var graphs = Graphs.Where(x => selected.Contains(x.Id)).ToArray();
            messenger.Send(new GraphSelectedMessage(graphs));
        }

        private async Task ActivatedGraph(int model)
        {
            await ExecuteSafe(async () =>
            {
                var graphModel = await service.ReadGraphAsync(model).ConfigureAwait(false);
                var layers = LayersBuilder.Take(graphModel).Build();
                await layers.OverlayAsync(graphModel.Graph).ConfigureAwait(false);
                ActivatedGraphId = graphModel.Id;
                messenger.Send(new GraphActivatedMessage(graphModel), Tokens.GraphField);
                messenger.Send(new GraphActivatedMessage(graphModel), Tokens.PathfindingRange);
                messenger.Send(new GraphActivatedMessage(graphModel));
            }, logger.Error).ConfigureAwait(false);
        }

        private async Task LoadGraphs()
        {
            await ExecuteSafe(async () =>
            {
                Graphs.Clear();
                var infos = (await service.ReadAllGraphInfoAsync().ConfigureAwait(false))
                    .Select(ToGraphInfo).ToList();
                Graphs.Add(infos);
            }, logger.Error).ConfigureAwait(false);
        }

        private void OnObstaclesCountChanged(object recipient, ObstaclesCountChangedMessage msg)
        {
            var graph = Graphs.FirstOrDefault(x => x.Id == msg.GraphId);
            if (graph != null)
            {
                graph.ObstaclesCount += msg.Delta;
            }
        }

        private void GraphStateChanged(object recipient, GraphStateChangedMessage msg)
        {
            var graph = Graphs.FirstOrDefault(x => x.Id == msg.Id);
            if (graph != null)
            {
                graph.Status = msg.Status;
            }
        }

        private async Task OnGraphUpdated(object recipient, GraphUpdatedMessage msg)
        {
            var model = Graphs.FirstOrDefault(x => x.Id == msg.Model.Id);
            if (model != null)
            {
                model.Name = msg.Model.Name;
                model.Neighborhood = msg.Model.Neighborhood;
                model.SmoothLevel = msg.Model.SmoothLevel;
                if (ActivatedGraphId == model.Id)
                {
                    await ActivatedGraph(ActivatedGraphId);
                }
            }
            msg.Signal();
        }

        private async Task OnGraphCreated(object recipient, GraphCreatedMessage msg)
        {
            if (msg.Models.Length > 0)
            {
                Graphs.Add(msg.Models);
                if (Graphs.Count == 1)
                {
                    await ActivatedGraph(Graphs.First().Id);
                }
            }
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            var graphs = Graphs
                .Where(x => msg.GraphIds.Contains(x.Id))
                .ToList();
            Graphs.Remove(graphs);
        }

        private GraphInfoModel ToGraphInfo(GraphInformationModel model)
        {
            return new GraphInfoModel()
            {
                Id = model.Id,
                Name = model.Name,
                Neighborhood = model.Neighborhood,
                SmoothLevel = model.SmoothLevel,
                Width = model.Dimensions.ElementAtOrDefault(0),
                Length = model.Dimensions.ElementAtOrDefault(1),
                ObstaclesCount = model.ObstaclesCount,
                Status = model.Status
            };
        }
    }
}
