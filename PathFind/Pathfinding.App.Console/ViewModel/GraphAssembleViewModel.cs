using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;

namespace Pathfinding.App.Console.ViewModel
{
    public class GraphAssembleViewModel : ReactiveObject
    {
        private readonly IMessenger messenger;
        private readonly IMeanCost meanCost;
        private readonly IRandom random;
        private readonly IRequestService<VertexViewModel> requestService;

        public string Name { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public int SmoothLevel { get; set; } = 0;

        public ReturnOptions ReturnOption { get; set; } = ReturnOptions.Limit;

        public INeighborhoodFactory NeighborhoodFactory { get; set; }

        public IGraphAssemble<VertexViewModel> Assembler { get; set; }

        public ReactiveCommand<Unit, Unit> AssembleCommand { get; }

        public GraphAssembleViewModel(IEnumerable<Pair<string, INeighborhoodFactory>> neighbourhoodFactoires,
            IEnumerable<Pair<string, IGraphAssemble<VertexViewModel>>> assemblers,
            IEnumerable<Pair<string, int>> smoothLevels,
            IRequestService<VertexViewModel> requestService,
            IMessenger messenger,
            IMeanCost meanCost,
            IRandom random)
        {
            var canAssemble = this.WhenAnyValue(
                x => x.Name,
                x => x.Length,
                x => x.Width,
                x => x.ObstaclePercent,
                x => x.NeighborhoodFactory,
                x => x.Assembler,
                (name, length, width, obstacle, neighborhood, assembler) =>
                {
                    return !string.IsNullOrEmpty(name)
                        && Constants.GraphWidthValueRange.Contains(width)
                        && Constants.GraphLengthValueRange.Contains(length)
                        && Constants.ObstaclesPercentValueRange.Contains(obstacle)
                        && assembler is not null
                        && neighborhood is not null;
                });
            this.messenger = messenger;
            this.meanCost = meanCost;
            this.random = random;
            this.requestService = requestService;
            AssembleCommand = ReactiveCommand.Create(Assemble, canAssemble);
        }

        private async void Assemble()
        {
            var obstacleLayer = new ObstacleLayer(random, ObstaclePercent);
            var neighborhoodLayer = new NeighborhoodLayer(NeighborhoodFactory, ReturnOption);
            var costLayer = new VertexCostLayer(Constants.VerticesCostRange,
                range => random.NextInt(range));
            var smoothLayer = new SmoothLayer(meanCost);
            var smoothLayers = Enumerable.Repeat(smoothLayer, SmoothLevel).To(x => new Layers(x));
            var layers = new Layers(obstacleLayer, neighborhoodLayer, costLayer, smoothLayers);
            var graph = await Assembler.AssembleGraphAsync(layers, Width, Length);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(45));
            var request = new CreateGraphRequest<VertexViewModel>() { Graph = graph, Name = Name };
            var model = await requestService.CreateGraphAsync(request, cts.Token);
            // messenger.Send
        }
    }
}
