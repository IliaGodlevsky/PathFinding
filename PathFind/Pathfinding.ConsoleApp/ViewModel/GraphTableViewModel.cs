using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphTableViewModel : BaseViewModel, IGraphTableViewModel
    {
        private readonly IRequestService<GraphVertexModel> service;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        public ReactiveCommand<Unit, Unit> LoadGraphsCommand { get; }

        public ReactiveCommand<GraphInfoModel, Unit> ActivateGraphCommand { get; }

        public ReactiveCommand<GraphInfoModel[], Unit> SelectGraphsCommand { get; }

        public ObservableCollection<GraphInfoModel> Graphs { get; } = new();

        public GraphTableViewModel(
            IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.messenger = messenger;
            this.logger = logger;
            messenger.Register<GraphCreatedMessage>(this, async (r, msg) => await OnGraphCreated(r, msg));
            messenger.Register<GraphUpdatedMessage>(this, async (r, msg) => await OnGraphUpdated(r, msg));
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<ObstaclesCountChangedMessage>(this, OnObstaclesCountChanged);
            messenger.Register<GraphStateChangedMessage>(this, GraphBecameReadOnly);
            LoadGraphsCommand = ReactiveCommand.CreateFromTask(LoadGraphs);
            ActivateGraphCommand = ReactiveCommand.CreateFromTask<GraphInfoModel>(ActivatedGraph);
            SelectGraphsCommand = ReactiveCommand.Create<GraphInfoModel[]>(SelectGraphs);
        }

        private void SelectGraphs(GraphInfoModel[] selected)
        {
            messenger.Send(new GraphSelectedMessage(selected));
        }

        private async Task ActivatedGraph(GraphInfoModel model)
        {
            await ExecuteSafe(async () =>
            {
                var graphModel = await service.ReadGraphAsync(model.Id).ConfigureAwait(false);
                var layers = GetLayers(graphModel);
                await layers.OverlayAsync(graphModel.Graph).ConfigureAwait(false);
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
                var infos = await service.ReadAllGraphInfoAsync().ConfigureAwait(false);
                var graphs = infos
                    .Select(x => new GraphInfoModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Neighborhood = x.Neighborhood,
                        SmoothLevel = x.SmoothLevel,
                        Width = x.Dimensions.ElementAtOrDefault(0),
                        Length = x.Dimensions.ElementAtOrDefault(1),
                        Obstacles = x.ObstaclesCount,
                        Status = x.Status
                    })
                    .ToList();
                Graphs.Add(graphs);
            }, logger.Error).ConfigureAwait(false);
        }

        private void OnObstaclesCountChanged(object recipient, ObstaclesCountChangedMessage msg)
        {
            var graph = Graphs.FirstOrDefault(x => x.Id == msg.GraphId);
            if (graph != null)
            {
                graph.Obstacles += msg.Delta;
            }
        }

        private void GraphBecameReadOnly(object recipient, GraphStateChangedMessage msg)
        {
            var graph = Graphs.FirstOrDefault(x => x.Id == msg.Id);
            if (graph != null)
            {
                graph.Status = msg.Status;
            }
        }

        private async Task OnGraphUpdated(object recipient, GraphUpdatedMessage msg)
        {
            var info = Graphs.FirstOrDefault(x => x.Id == msg.Model.Id);
            if (info != null)
            {
                info.Name = msg.Model.Name;
                info.Neighborhood = msg.Model.Neighborhood;
                info.SmoothLevel = msg.Model.SmoothLevel;
                await ActivatedGraph(info);
            }
        }

        private async Task OnGraphCreated(object recipient, GraphCreatedMessage msg)
        {
            if (msg.Models.Length > 0)
            {
                Graphs.Add(msg.Models);
                if (Graphs.Count == 1)
                {
                    await ActivatedGraph(Graphs.First());
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

        private ILayer GetLayers(GraphModel<GraphVertexModel> model)
        {
            var layers = new Layers();
            switch (model.Neighborhood)
            {
                case Neighborhoods.Moore:
                    layers.Add(new MooreNeighborhoodLayer());
                    break;
                case Neighborhoods.VonNeumann:
                    layers.Add(new VonNeumannNeighborhoodLayer());
                    break;
            }
            int level = default;
            switch (model.SmoothLevel)
            {
                case SmoothLevels.Low: level = 1; break;
                case SmoothLevels.Medium: level = 2; break;
                case SmoothLevels.High: level = 4; break;
                case SmoothLevels.Extreme: level = 7; break;
            }
            layers.Add(new SmoothLayer(level));
            return layers;
        }
    }
}
