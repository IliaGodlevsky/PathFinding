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
using System.Threading;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class CreateGraphViewModel
    {
        private static readonly InclusiveValueRange<int> CostRange = (9, 1);
        private readonly IRequestService<VertexViewModel> service;
        private readonly IGraphAssemble<VertexViewModel> graphAssemble;
        private readonly IMessenger messenger;

        private int Limit { get; set; }

        public ReactiveCommand<MouseEventArgs, Unit> CreateCommand { get; }

        public CreateGraphViewModel(IRequestService<VertexViewModel> service,
            IGraphAssemble<VertexViewModel> graphAssemble,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            this.service = service;
            this.messenger = messenger;
            this.graphAssemble = graphAssemble;
            CreateCommand = ReactiveCommand.Create<MouseEventArgs>(CreateGraph);
        }

        public async void CreateGraph(MouseEventArgs e)
        {
            var random = new CongruentialRandom();
            var msg = new GraphParametresRequest();
            messenger.Send(msg);
            var costLayer = new VertexCostLayer(CostRange, range => random.NextInt(range));
            var obstacleLayer = new ObstacleLayer(random, msg.Obstacles);
            var neighborhoodLayer = new NeighborhoodLayer(msg.NeighborhoodFactory, ReturnOptions.Limit);
            var smoothLayer = Enumerable
                .Repeat(new SmoothLayer(new MeanCost()), msg.SmoothLevel)
                .To(x => new Layers(x.ToArray()));
            var layers = new Layers(costLayer, obstacleLayer, neighborhoodLayer, smoothLayer);
            var graph = await graphAssemble.AssembleGraphAsync(layers, msg.Width, msg.Length);
            var request = new CreateGraphRequest<VertexViewModel>() { Graph = graph, Name = msg.Name };
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
            {
                var graphModel = await service.CreateGraphAsync(request, cts.Token);
                //messenger.Send(graphModel);
            }
        }
    }
}
