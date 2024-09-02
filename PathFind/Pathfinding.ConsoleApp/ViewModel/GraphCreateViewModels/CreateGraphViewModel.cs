using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Business.MeanCosts;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Random;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class CreateGraphViewModel : ReactiveObject
    {
        private static readonly InclusiveValueRange<int> CostRange = (9, 1);
        private readonly IRequestService<VertexViewModel> service;
        private readonly IGraphAssemble<VertexViewModel> graphAssemble;
        private readonly IMessenger messenger;

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

        private int smoothLevel;
        public int SmoothLevel 
        {
            get => smoothLevel;
            set => this.RaiseAndSetIfChanged(ref smoothLevel, value);
        }

        private INeighborhoodFactory neighborhoodFactory;
        public INeighborhoodFactory NeighborhoodFactory 
        {
            get => neighborhoodFactory;
            set => this.RaiseAndSetIfChanged(ref neighborhoodFactory, value); 
        }

        public ReactiveCommand<MouseEventArgs, Unit> CreateCommand { get; }

        public CreateGraphViewModel(IRequestService<VertexViewModel> service,
            IGraphAssemble<VertexViewModel> graphAssemble,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            this.service = service;
            this.messenger = messenger;
            this.graphAssemble = graphAssemble;
            CreateCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(CreateGraph, CanExecute());
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(
                x => x.Width,
                x => x.Length,
                x => x.Obstacles,
                x => x.Name,
                x => x.NeighborhoodFactory,
                (width, length, obstacles, name, factory) =>
                {
                    return width > 0 && length > 0 && obstacles >= 0 && factory != null
                        && !string.IsNullOrEmpty(name);
                });
        }

        public async Task CreateGraph(MouseEventArgs e)
        {
            var random = new CongruentialRandom();
            var costLayer = new VertexCostLayer(CostRange, range => random.NextInt(range));
            var obstacleLayer = new ObstacleLayer(random, Obstacles);
            var neighborhoodLayer = new NeighborhoodLayer(NeighborhoodFactory, ReturnOptions.Limit);
            var smoothLayer = Enumerable.Repeat(new SmoothLayer(new MeanCost()), SmoothLevel)
                .To(x => new Layers(x.ToArray()));
            var layers = new Layers(costLayer, obstacleLayer, neighborhoodLayer, smoothLayer);
            var graph = await graphAssemble.AssembleGraphAsync(layers, Width, Length);
            var request = new CreateGraphRequest<VertexViewModel>() { Graph = graph, Name = Name };
            var graphModel = await service.CreateGraphAsync(request);
            messenger.Send(new GraphCreatedMessage(graphModel));
        }
    }
}
