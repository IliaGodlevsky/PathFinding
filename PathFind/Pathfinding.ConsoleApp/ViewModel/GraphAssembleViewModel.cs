using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Random;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphAssembleViewModel : BaseViewModel,
        IGraphAssembleViewModel, 
        IRequireGraphNameViewModel, 
        IRequireGraphParametresViewModel,
        IRequireSmoothLevelViewModel, 
        IRequireNeighborhoodNameViewModel
    {
        private static readonly InclusiveValueRange<int> CostRange = (9, 1);

        private readonly IRequestService<GraphVertexModel> service;
        private readonly IGraphAssemble<GraphVertexModel> graphAssemble;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private int width;
        public int Width
        {
            get => width;
            set => this.RaiseAndSetIfChanged(ref width, value);
        }

        private int length;
        public int Length
        {
            get => length;
            set => this.RaiseAndSetIfChanged(ref length, value);
        }

        private int obstacles;
        public int Obstacles
        {
            get => obstacles;
            set => this.RaiseAndSetIfChanged(ref obstacles, value);
        }

        private string level;
        public string SmoothLevel
        {
            get => level;
            set => this.RaiseAndSetIfChanged(ref level, value);
        }

        private string neighborhood;
        public string Neighborhood
        {
            get => neighborhood;
            set => this.RaiseAndSetIfChanged(ref neighborhood, value);
        }

        public ReactiveCommand<Unit, Unit> CreateCommand { get; }

        public GraphAssembleViewModel(IRequestService<GraphVertexModel> service,
            IGraphAssemble<GraphVertexModel> graphAssemble,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.messenger = messenger;
            this.logger = logger;
            this.graphAssemble = graphAssemble;
            CreateCommand = ReactiveCommand.CreateFromTask(CreateGraph, CanExecute());
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(
                x => x.Width,
                x => x.Length,
                x => x.Obstacles,
                x => x.Name,
                x => x.Neighborhood,
                x => x.SmoothLevel,
                (width, length, obstacles, name, factory, smooth) =>
                {
                    return width > 0 && length > 0
                        && obstacles >= 0 
                        && !string.IsNullOrEmpty(factory)
                        && !string.IsNullOrEmpty(smooth)
                        && !string.IsNullOrEmpty(name);
                });
        }

        private async Task CreateGraph()
        {
            await ExecuteSafe(async () =>
            {
                var random = new CongruentialRandom();
                var costLayer = new VertexCostLayer(CostRange, range => new VertexCost(random.NextInt(range), range));
                var obstacleLayer = new ObstacleLayer(random, Obstacles);
                var layers = new Layers(costLayer, obstacleLayer);
                var graph = await graphAssemble.AssembleGraphAsync(layers, Width, Length)
                    .ConfigureAwait(false);
                var request = new CreateGraphRequest<GraphVertexModel>()
                {
                    Graph = graph,
                    Name = Name,
                    Neighborhood = Neighborhood,
                    SmoothLevel = SmoothLevel
                };
                var graphModel = await service.CreateGraphAsync(request).ConfigureAwait(false);
                var info = new GraphInfoModel()
                {
                    Id = graphModel.Id,
                    Name = Name,
                    SmoothLevel = SmoothLevel,
                    Neighborhood = Neighborhood,
                    Obstacles = graphModel.Graph.GetObstaclesCount(),
                    Width = Width,
                    Length = Length,
                    Status = GraphStatuses.Editable
                };
                messenger.Send(new GraphCreatedMessage(new[] { info }));
            }, logger.Error).ConfigureAwait(false);
        }
    }
}
